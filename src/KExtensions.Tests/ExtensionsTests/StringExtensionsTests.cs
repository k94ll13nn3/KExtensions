// KExtensions
using KExtensions.Core.Extensions;
using NUnit.Framework;

namespace KExtensions.Tests.ExtensionsTests
{
    [TestFixture]
    internal class StringExtensionsTests
    {
        [Test]
        public void In_StringIsInArray_True()
        {
            const string value = "Keuvain";
            var values = new[] { "KEu", "Vai", "Keuvain" };

            Assert.That(value.In(values), Is.True);
        }

        [Test]
        public void In_StringIsNotInArray_False()
        {
            const string value = "K3uvain";
            var values = new[] { "KEu", "Vai", "Keuvain" };

            Assert.That(value.In(values), Is.False);
        }

        [Test]
        public void Slugify_StringWithUpperCaseLetters_StringWithNoUpperCaseLetters()
        {
            const string value = "KAJeeaDF";

            Assert.That(value.Slugify(), Is.EqualTo("kajeeadf"));
        }

        [Test]
        public void Slugify_StringWithAccents_StringWithNoAccents()
        {
            const string value = "йщавпо";

            Assert.That(value.Slugify(), Is.EqualTo("euaaii"));
        }

        [Test]
        public void Slugify_StringWithSpaces_StringWithDashes()
        {
            const string value = "ah  gj kfiv    dhjc  c";

            Assert.That(value.Slugify(), Is.EqualTo("ah-gj-kfiv-dhjc-c"));
        }

        [Test]
        public void Slugify_StringWithMultiplesDashes_StringWithUniqueDashes()
        {
            const string value = "a--a--b----b";

            Assert.That(value.Slugify(), Is.EqualTo("a-a-b-b"));
        }

        [Test]
        public void Slugify_StringWithInvalidChars_StringWithNoInvalidChars()
        {
            const string value = "amd$%*d=gl";

            Assert.That(value.Slugify(), Is.EqualTo("amddgl"));
        }

        [Test]
        public void Slugify_String_SlugifiedString()
        {
            const string value = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam tempor mi pretium molestie sagittis. In et augue a eros scelerisque facilisis et eget tellus.";

            Assert.That(value.Slugify(), Is.EqualTo("lorem-ipsum-dolor-sit-amet-consectetur-adipiscing-elit-aliquam-tempor-mi-pretium-molestie-sagittis-in-et-augue-a-eros-scelerisque-facilisis-et-eget-tellus"));
        }

        [Test]
        public void ThrowIfNullOrWhiteSpace_NullString_ThrowsArgumentNullException()
        {
            const string value = null;

            Assert.That(() => value.ThrowIfNullOrWhiteSpace(value), Throws.ArgumentNullException);
        }

        [Test]
        public void ThrowIfNullOrWhiteSpace_EmptyString_ThrowsArgumentNullException()
        {
            var value = string.Empty;

            Assert.That(() => value.ThrowIfNullOrWhiteSpace(value), Throws.ArgumentException.With.Message.StartsWith("Argument must not be the empty string."));
        }

        [Test]
        public void ThrowIfNullOrWhiteSpace_StringWithOnlyWhiteSpaces_ThrowsArgumentNullException()
        {
            const string value = "    ";

            Assert.That(() => value.ThrowIfNullOrWhiteSpace(value), Throws.ArgumentException.With.Message.StartsWith("Argument must not be only composed of whitespace characters."));
        }

        [Test]
        public void ThrowIfNullOrWhiteSpace_ValidString_ThrowsNothing()
        {
            const string value = "Value";

            Assert.That(() => value.ThrowIfNullOrWhiteSpace(value), Throws.Nothing);
        }

        [Test]
        public void ThrowIfInvalidPath_InvalidPath_ThrowsArgumentNullException()
        {
            const string value = @"C:\Users""";

            Assert.That(() => value.ThrowIfInvalidPath(value), Throws.ArgumentException.With.Message.StartsWith("Argument contains invalid path chars (see Path.GetInvalidPathChars())"));
        }

        [Test]
        public void ThrowIfInvalidPath_ValidPath_DoesNotThrow()
        {
            const string value = @"C:\Users";

            Assert.That(() => value.ThrowIfInvalidPath(value), Throws.Nothing);
        }
    }
}