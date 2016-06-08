// KExtensions
using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace KExtensions.Core.Helpers
{
    /// <summary>
    /// Helper methods for the KGrid class. Should be able to validate and parse all possible values for the
    /// height and width of rows and columns.
    /// </summary>
    /// <seealso href="https://msdn.microsoft.com/fr-fr/library/system.windows.gridlength(v=vs.110).aspx"/>
    internal static class KGridHelpers
    {
        /// <summary>
        /// Test whether the value is a valid <see cref="string"/> that can be parsed into a <see cref="GridLength"/> object.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to test.</param>
        /// <returns>True if the value is valid.</returns>
        internal static bool IsValidGridLengthValue(string value)
        {
            double parsedValue;
            Predicate<string> isQualifiedDouble = s =>
                s.Length >= 3
                && new[] { "px", "in", "pt", "cm" }.Contains(value.Substring(value.Length - 2, 2))
                && double.TryParse(value.Substring(0, value.Length - 2), NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValue);

            if (isQualifiedDouble(value))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(value)
                || !((value.Length >= 2 && value[value.Length - 1] == '*'
                    && double.TryParse(value.Substring(0, value.Length - 1), NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValue))
                || (value == "*" || value == "!" || value.ToLowerInvariant() == "auto"
                || double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValue))))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Parse a <see cref="string"/> and returns the corresponding <see cref="GridLength"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to parse.</param>
        /// <returns>The corresponding <see cref="GridLength"/>.</returns>
        internal static GridLength ParseValue(string value)
        {
            var parsedGridLength = default(GridLength);
            const double inchesAsPixels = 96.0;
            double parsedValue;

            Predicate<string> isQualifiedDouble = s =>
                s.Length >= 3
                && new[] { "px", "in", "pt", "cm" }.Contains(value.Substring(value.Length - 2, 2))
                && double.TryParse(value.Substring(0, value.Length - 2), NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValue);

            if (isQualifiedDouble(value))
            {
                parsedValue = double.Parse(value.Substring(0, value.Length - 2), NumberStyles.Number, CultureInfo.InvariantCulture);
                switch (value.Substring(value.Length - 2, 2))
                {
                    case "in":
                        parsedValue *= inchesAsPixels;
                        break;

                    case "cm":
                        parsedValue *= inchesAsPixels / 2.54;
                        break;

                    case "pt":
                        parsedValue *= inchesAsPixels / 72.0;
                        break;
                }

                parsedGridLength = new GridLength(parsedValue, GridUnitType.Pixel);
            }
            else if (value.Length >= 1 && value[value.Length - 1] == '*')
            {
                if (!double.TryParse(value.Substring(0, value.Length - 1), NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValue))
                {
                    parsedValue = 1;
                }

                parsedGridLength = new GridLength(parsedValue, GridUnitType.Star);
            }
            else if (value.ToLowerInvariant() == "auto" || value == "!")
            {
                parsedGridLength = new GridLength(0, GridUnitType.Auto);
            }
            else if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValue))
            {
                parsedGridLength = new GridLength(parsedValue, GridUnitType.Pixel);
            }
            else
            {
                throw new ArgumentException("The passed value is not valid.", nameof(value));
            }

            return parsedGridLength;
        }
    }
}