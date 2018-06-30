using System.Windows;
using FluentAssertions;
using KExtensions.Core.Helpers;
using Xunit;

namespace KExtensions.Tests.HelpersTests
{
    public class KGridHelpersTests
    {
        [Fact]
        public void IsValidGridLengthValue_Auto_True()
        {
            KGridHelpers.IsValidGridLengthValue("auto").Should().BeTrue();
            KGridHelpers.IsValidGridLengthValue("aUto").Should().BeTrue();
            KGridHelpers.IsValidGridLengthValue("Auto").Should().BeTrue();
            KGridHelpers.IsValidGridLengthValue("AUto").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_Double_True()
        {
            KGridHelpers.IsValidGridLengthValue("6.5").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_ExclamationMark_True()
        {
            KGridHelpers.IsValidGridLengthValue("!").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_Int_True()
        {
            KGridHelpers.IsValidGridLengthValue("6").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_OneStarAndAString_False()
        {
            KGridHelpers.IsValidGridLengthValue("d*").Should().BeFalse();
        }

        [Fact]
        public void IsValidGridLengthValue_OneStarAndAValue_True()
        {
            KGridHelpers.IsValidGridLengthValue("2.5*").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_OnlyOneStar_True()
        {
            KGridHelpers.IsValidGridLengthValue("*").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_OnlyQuestionMark_False()
        {
            KGridHelpers.IsValidGridLengthValue("?").Should().BeFalse();
        }

        [Fact]
        public void IsValidGridLengthValue_RandomString_False()
        {
            KGridHelpers.IsValidGridLengthValue("False").Should().BeFalse();
            KGridHelpers.IsValidGridLengthValue("false").Should().BeFalse();
            KGridHelpers.IsValidGridLengthValue("faLse").Should().BeFalse();
        }

        [Fact]
        public void IsValidGridLengthValue_ValueInCentimeter_True()
        {
            KGridHelpers.IsValidGridLengthValue("20.2cm").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_ValueInInches_True()
        {
            KGridHelpers.IsValidGridLengthValue("20.3in").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_ValueInKilo_False()
        {
            KGridHelpers.IsValidGridLengthValue("20.5kg").Should().BeFalse();
        }

        [Fact]
        public void IsValidGridLengthValue_ValueInPixel_True()
        {
            KGridHelpers.IsValidGridLengthValue("20.1px").Should().BeTrue();
        }

        [Fact]
        public void IsValidGridLengthValue_ValueInPoints_True()
        {
            KGridHelpers.IsValidGridLengthValue("20.4cm").Should().BeTrue();
        }

        [Fact]
        public void ParseValue_Auto_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(1, GridUnitType.Auto);

            KGridHelpers.ParseValue("auto").Should().Be(expectedValue);
            KGridHelpers.ParseValue("aUto").Should().Be(expectedValue);
            KGridHelpers.ParseValue("Auto").Should().Be(expectedValue);
            KGridHelpers.ParseValue("AUto").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_Double_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(42.9, GridUnitType.Pixel);

            KGridHelpers.ParseValue("42.9").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_ExclamationMark_ReturnsAutoGridUnitType()
        {
            var expectedValue = new GridLength(1, GridUnitType.Auto);

            KGridHelpers.ParseValue("!").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_Int_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20, GridUnitType.Pixel);

            KGridHelpers.ParseValue("20").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_OneStarAndADouble_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(2.5, GridUnitType.Star);

            KGridHelpers.ParseValue("2.5*").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_OneStarAndAnInt_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(2, GridUnitType.Star);

            KGridHelpers.ParseValue("2*").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_OnlyOneStar_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(1, GridUnitType.Star);

            KGridHelpers.ParseValue("*").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_ValueInCentimeter_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5 * 96.0 / 2.54, GridUnitType.Pixel);

            KGridHelpers.ParseValue("20.5cm").Value.Should().BeInRange(expectedValue.Value - .0005, expectedValue.Value + .0005);
            KGridHelpers.ParseValue("20.5cm").GridUnitType.Should().Be(expectedValue.GridUnitType);
        }

        [Fact]
        public void ParseValue_ValueInInches_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5 * 96.0, GridUnitType.Pixel);

            KGridHelpers.ParseValue("20.5in").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_ValueInPixel_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5, GridUnitType.Pixel);

            KGridHelpers.ParseValue("20.5px").Should().Be(expectedValue);
        }

        [Fact]
        public void ParseValue_ValueInPoints_ReturnsExpectedValue()
        {
            var expectedValue = new GridLength(20.5 * 96.0 / 72.0, GridUnitType.Pixel);

            KGridHelpers.ParseValue("20.5pt").Value.Should().BeInRange(expectedValue.Value - .0005, expectedValue.Value + .0005);
            KGridHelpers.ParseValue("20.5pt").GridUnitType.Should().Be(expectedValue.GridUnitType);
        }
    }
}
