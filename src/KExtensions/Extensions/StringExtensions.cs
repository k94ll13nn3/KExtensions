// KExtensions
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KExtensions.Core.Extensions
{
    /// <summary>
    /// Extensions for string objects.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a string to a slug version.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>The converted string.</returns>
        /// <seealso href="https://gist.github.com/joancaron/8436664"/>
        public static string Slugify(this string value)
        {
            value = value.ToLowerInvariant();
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);
            value = Regex.Replace(value, @"[^\w\s\p{Pd}]", string.Empty, RegexOptions.Compiled);
            value = value.Trim('-', '_');
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }

        /// <summary>
        ///     Checks if a string a valid path.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="name">The name of the string.</param>
        /// <exception cref="ArgumentNullException">If the string is null.</exception>
        /// <exception cref="ArgumentException">If the string is empty or contains one or more whitespace characters or invalid path chars.</exception>
        public static void ThrowIfInvalidPath(this string value, string name)
        {
            ThrowIfNullOrWhiteSpace(value, name);

            if (value.Any(Path.GetInvalidPathChars().Contains))
            {
                throw new ArgumentException("Argument contains invalid path chars (see Path.GetInvalidPathChars())", name);
            }
        }

        /// <summary>
        ///     Checks if a string is either null, empty or contains one or more whitespace characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="name">The name of the string.</param>
        /// <exception cref="ArgumentNullException">If the string is null.</exception>
        /// <exception cref="ArgumentException">If the string is empty or contains one or more whitespace characters.</exception>
        public static void ThrowIfNullOrWhiteSpace(this string value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Argument must not be the empty string.", name);
            }

            if (value.All(char.IsWhiteSpace))
            {
                throw new ArgumentException("Argument must not be only composed of whitespace characters.", name);
            }
        }

        /// <summary>
        /// Tests if a string is in an array of values.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="values">The array with possible values.</param>
        /// <returns>A boolean indicating whether the string is contained in the array.</returns>
        public static bool In(this string value, string[] values) => values.Contains(value);
    }
}
