//////////////////////////////////////////////////////////////////////
// DEPENDENCIES
//////////////////////////////////////////////////////////////////////

#tool GitVersion.CommandLine&version=4.0.0-beta0012
#tool OpenCover&version=4.6.519
#tool coveralls.io&version=1.4.2

#addin Cake.Coveralls&version=0.8.0
#addin Octokit&version=0.29.0
#addin Cake.FileHelpers&version=3.0.0

//////////////////////////////////////////////////////////////////////
// USINGS
//////////////////////////////////////////////////////////////////////

using Octokit;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var solutionDir = Directory("./src/") + Directory("KExtensions/");
var buildDir = solutionDir + Directory("bin/");
var publishDir = Directory("./artifacts");
var coverageDir = Directory("./coverage");

// Define script variables
var releaseNotesPath = new FilePath("releaseNotes.md");
var coverageResultPath = new FilePath("./coverage.xml");
var framework = "netstandard1.0";
var versionSuffix = "";
var nugetVersion = "";
var currentBranch = EnvironmentVariable("APPVEYOR_REPO_BRANCH");
var isOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isOnMaster =  currentBranch == "master";
var isPullRequest = isOnAppVeyor ? AppVeyor.Environment.PullRequest.IsPullRequest : false;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
	CleanDirectory(publishDir);
	CleanDirectory(coverageDir);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => DotNetCoreRestore());

Task("Set-Environment")
    .IsDependentOn("Restore")
    .Does(() =>
{
    Information("Current branch:      " + currentBranch);
    Information("Master branch:       " + isOnMaster.ToString());
    Information("Pull Request:        " + isPullRequest.ToString());
    Information("Running on AppVeyor: " + isOnAppVeyor.ToString());

    var version = GitVersion(new GitVersionSettings 
    {
        UpdateAssemblyInfo = true, 
        WorkingDirectory = solutionDir 
    });
    nugetVersion = version.NuGetVersion;
    if(version.CommitsSinceVersionSource.HasValue && version.CommitsSinceVersionSource.Value != 0)
    {
        versionSuffix = "ci" + version.CommitsSinceVersionSource?.ToString()?.PadLeft(4, '0');
    }    

    Information("AssembyVersion       " + version.AssemblySemVer);
    Information("FileVersion          " + version.AssemblySemFileVer);
    Information("InformationalVersion " + version.InformationalVersion);
    if (isOnAppVeyor)
    {
        Information("Build version:       " + nugetVersion + " (" + EnvironmentVariable("APPVEYOR_BUILD_NUMBER") + ")");
        AppVeyor.UpdateBuildVersion(nugetVersion + " (" + EnvironmentVariable("APPVEYOR_BUILD_NUMBER") + ")");
    }
});

Task("Build")
    .IsDependentOn("Set-Environment")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        VersionSuffix = versionSuffix
    };

    DotNetCoreBuild(solutionDir, settings);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    EnsureDirectoryExists(coverageDir);
    var settings = new DotNetCoreTestSettings
    {
        Configuration = "Coverage"
    };

    var coverageSettings = new OpenCoverSettings().WithFilter("+[KExtensions*]*").WithFilter("-[KExtensions.Tests]*");
    coverageSettings.ReturnTargetCodeOffset = 1000; // Offset in order to have Cake fail if a test is a failure
    coverageSettings.Register = "user";
    coverageSettings.MergeOutput = true;
    coverageSettings.OldStyle = true;
    coverageSettings.SkipAutoProps = true;

    OpenCover(tool => {
            tool.DotNetCoreTest("./test/KExtensions.Tests/KExtensions.Tests.csproj", settings);
        },
        coverageDir + coverageResultPath,
        coverageSettings
    );
});

Task("Nuget-Pack")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    EnsureDirectoryExists(publishDir);
    var settings = new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory = publishDir,
        VersionSuffix = versionSuffix
    };

    DotNetCorePack(solutionDir, settings);
});

Task("Upload-Artifact")
    .WithCriteria(() => isOnAppVeyor && isOnMaster && !isPullRequest)
    .IsDependentOn("Nuget-Pack")
    .Does(() =>
{
    AppVeyor.UploadArtifact(publishDir + new FilePath("KExtensions." + nugetVersion +".nupkg"));
});
