// KExtensions
using System;
using System.Windows;

namespace KExtensions.Core.DP
{
    /// <summary>
    /// Add an extension property to a control allowing setting the visibility with a boolean.
    /// </summary>
    /// <seealso href="http://www.rudyhuyn.com/blog/2015/03/26/how-to-add-isvisible-property-to-all-winrt-ui-elements/"/>
    public static class KControl
    {
        /// <summary>
        /// A dependency property which provides the ability to set the visibility with a boolean.
        /// </summary>
        private static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached(
                "IsVisible",
                typeof(bool),
                typeof(KControl),
                new PropertyMetadata(true, IsVisibleCallback));

        /// <summary>
        /// Gets the IsVisible property of the element.
        /// </summary>
        /// <param name="element">The element from which the property is recovered.</param>
        /// <returns>The IsVisible property of the element.</returns>
        public static bool GetIsVisible(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return (bool)element.GetValue(IsVisibleProperty);
        }

        /// <summary>
        /// Set the IsVisible property of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The new value.</param>
        public static void SetIsVisible(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.SetValue(IsVisibleProperty, value);
        }

        /// <summary>
        /// Represents the callback that is invoked when the effective property value of <see cref="IsVisibleProperty"/> changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void IsVisibleCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UIElement)d).Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}