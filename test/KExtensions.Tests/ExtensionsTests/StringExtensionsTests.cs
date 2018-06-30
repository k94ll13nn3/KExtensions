using System;
using FluentAssertions;
using KExtensions.Core.Extensions;
using Xunit;

namespace KExtensions.Tests.ExtensionsTests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void In_StringIsInArray_True()
        {
            const string value = "Keuvain";
            var values = new[] { "KEu", "Vai", "Keuvain" };

            value.In(values).Should().BeTrue();
        }

        [Fact]
        public void In_StringIsNotInArray_False()
        {
            const string value = "K3uvain";
            var values = new[] { "KEu", "Vai", "Keuvain" };

            value.In(values).Should().BeFalse();
        }

        [Fact]
        public void Slugify_StringWithUpperCaseLetters_StringWithNoUpperCaseLetters()
        {
            const string value = "KAJeeaDF";

            value.Slugify().Should().Be("kajeeadf");
        }

        [Fact]
        public void Slugify_StringWithAccents_StringWithNoAccents()
        {
            const string value = "éùàâïî";

            value.Slugify().Should().Be("euaaii");
        }

        [Fact]
        public void Slugify_StringWithSpaces_StringWithDashes()
        {
            const string value = "ah  gj kfiv    dhjc  c";

            value.Slugify().Should().Be("ah-gj-kfiv-dhjc-c");
        }

        [Fact]
        public void Slugify_StringWithMultiplesDashes_StringWithUniqueDashes()
        {
            const string value = "a--a--b----b";

            value.Slugify().Should().Be("a-a-b-b");
        }

        [Fact]
        public void Slugify_StringWithInvalidChars_StringWithNoInvalidChars()
        {
            const string value = "amd$%*d=gl";

            value.Slugify().Should().Be("amddgl");
        }

        [Fact]
        public void Slugify_String_SlugifiedString()
        {
            const string value = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam tempor mi pretium molestie sagittis. In et augue a eros scelerisque facilisis et eget tellus.";

            value.Slugify().Should().Be("lorem-ipsum-dolor-sit-amet-consectetur-adipiscing-elit-aliquam-tempor-mi-pretium-molestie-sagittis-in-et-augue-a-eros-scelerisque-facilisis-et-eget-tellus");
        }

        [Fact]
        public void ThrowIfNullOrWhiteSpace_NullString_ThrowsArgumentNullException()
        {
            const string value = null;

            Action act = () => value.ThrowIfNullOrWhiteSpace(value);
            act.Should().Throw<ArgumentNullException>().Where(e => e.ParamName == "name");
        }

        [Fact]
        public void ThrowIfNullOrWhiteSpace_EmptyString_ThrowsArgumentNullException()
        {
            var value = string.Empty;

            Action act = () => value.ThrowIfNullOrWhiteSpace(value);
            act.Should().Throw<ArgumentException>().Where(e => e.Message == "Argument must not be the empty string.");
        }

        [Fact]
        public void ThrowIfNullOrWhiteSpace_StringWithOnlyWhiteSpaces_ThrowsArgumentNullException()
        {
            const string value = "    ";

            Action act = () => value.ThrowIfNullOrWhiteSpace(value);
            act.Should().Throw<ArgumentException>().Where(e => e.Message.StartsWith("Argument must not be only composed of whitespace characters."));
        }

        [Fact]
        public void ThrowIfNullOrWhiteSpace_ValidString_ThrowsNothing()
        {
            const string value = "Value";

            Action act = () => value.ThrowIfNullOrWhiteSpace(value);
            act.Should().NotThrow();
        }

        [Fact]
        public void ThrowIfInvalidPath_InvalidPath_ThrowsArgumentNullException()
        {
            const string value = @"C:\Users""";

            Action act = () => value.ThrowIfInvalidPath(value);
            act.Should().Throw<ArgumentException>().Where(e => e.Message.StartsWith("Argument contains invalid path chars (see Path.GetInvalidPathChars())"));
        }

        [Fact]
        public void ThrowIfInvalidPath_ValidPath_DoesNotThrow()
        {
            const string value = @"C:\Users";

            Action act = () => value.ThrowIfInvalidPath(value);
            act.Should().NotThrow();
        }
    }
}
