// KExtensions
using System.Windows;
using KExtensions.Core;
using NUnit.Framework;

namespace KExtensions.Tests
{
    [TestFixture]
    internal class HelpersTests
    {
        [Test]
        public void ParseValue_OnlyOneStar_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(1, GridUnitType.Star);

            Assert.That(Helpers.ParseValue("*"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_OneStarAndAnInt_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(2, GridUnitType.Star);

            Assert.That(Helpers.ParseValue("2*"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_OneStarAndADouble_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(2.5, GridUnitType.Star);

            Assert.That(Helpers.ParseValue("2.5*"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_Auto_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(1, GridUnitType.Auto);

            Assert.That(Helpers.ParseValue("auto"), Is.EqualTo(expectedValue));
            Assert.That(Helpers.ParseValue("aUto"), Is.EqualTo(expectedValue));
            Assert.That(Helpers.ParseValue("Auto"), Is.EqualTo(expectedValue));
            Assert.That(Helpers.ParseValue("AUto"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_Int_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20, GridUnitType.Pixel);

            Assert.That(Helpers.ParseValue("20"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_Double_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(42.9, GridUnitType.Pixel);

            Assert.That(Helpers.ParseValue("42.9"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_ValueInPixel_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5, GridUnitType.Pixel);

            Assert.That(Helpers.ParseValue("20.5px"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_ValueInCentimeter_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5 * 96.0 / 2.54, GridUnitType.Pixel);

            Assert.That(Helpers.ParseValue("20.5cm").Value, Is.EqualTo(expectedValue.Value).Within(.0005));
            Assert.That(Helpers.ParseValue("20.5cm").GridUnitType, Is.EqualTo(expectedValue.GridUnitType));
        }

        [Test]
        public void ParseValue_ValueInInches_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5 * 96.0, GridUnitType.Pixel);

            Assert.That(Helpers.ParseValue("20.5in"), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ParseValue_ValueInPoints_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5 * 96.0 / 72.0, GridUnitType.Pixel);

            Assert.That(Helpers.ParseValue("20.5pt").Value, Is.EqualTo(expectedValue.Value).Within(.0005));
            Assert.That(Helpers.ParseValue("20.5pt").GridUnitType, Is.EqualTo(expectedValue.GridUnitType));
        }

        [Test]
        public void IsValidGridLengthValue_OnlyOneStar_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("*"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_OneStarAndAValue_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("2.5*"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_OneStarAndAString_False()
        {
            Assert.That(Helpers.IsValidGridLengthValue("d*"), Is.False);
        }

        [Test]
        public void IsValidGridLengthValue_Auto_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("auto"), Is.True);
            Assert.That(Helpers.IsValidGridLengthValue("aUto"), Is.True);
            Assert.That(Helpers.IsValidGridLengthValue("Auto"), Is.True);
            Assert.That(Helpers.IsValidGridLengthValue("AUto"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_RandomString_False()
        {
            Assert.That(Helpers.IsValidGridLengthValue("False"), Is.False);
            Assert.That(Helpers.IsValidGridLengthValue("false"), Is.False);
            Assert.That(Helpers.IsValidGridLengthValue("faLse"), Is.False);
        }

        [Test]
        public void IsValidGridLengthValue_Int_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("6"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_Double_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("6.5"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_ValueInPixel_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("20.1px"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_ValueInCentimeter_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("20.2cm"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_ValueInInches_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("20.3in"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_ValueInPoints_True()
        {
            Assert.That(Helpers.IsValidGridLengthValue("20.4cm"), Is.True);
        }

        [Test]
        public void IsValidGridLengthValue_ValueInKilo_False()
        {
            Assert.That(Helpers.IsValidGridLengthValue("20.5kg"), Is.False);
        }
    }
}