// KExtensions
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KExtensions.Core
{
    /// <summary>
    /// A converter that convert a <see cref="bool"/> into any object depending of its value.
    /// </summary>
    public class BoolToValueConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the object returned when the <see cref="bool"/> is false.
        /// </summary>
        public object FalseValue { get; set; }

        /// <summary>
        /// Gets or sets the object returned when the <see cref="bool"/> is true.
        /// </summary>
        public object TrueValue { get; set; }

        /// <summary>
        /// Converts the boolean.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TypeDescriptor.GetConverter(targetType).ConvertFrom((bool)value ? this.TrueValue : this.FalseValue);
        }

        /// <summary>
        /// The <see cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/> is not implemented.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns><see cref="DependencyProperty.UnsetValue"/></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
    }
}