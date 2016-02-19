﻿// KExtensions
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KExtensions.Core
{
    /// <summary>
    /// Attached properties for the Grid element.
    /// </summary>
    public static class KGrid
    {
        /// <summary>
        /// An attached dependency property which provides a short way of defining columns.
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached(
                "Columns",
                typeof(string),
                typeof(KGrid),
                new FrameworkPropertyMetadata("*", Colums_PropertyChanged),
                new ValidateValueCallback(IsStringValueValid));

        /// <summary>
        /// An attached dependency property which provides a short way of defining columns.
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached(
                "Rows",
                typeof(string),
                typeof(KGrid),
                new FrameworkPropertyMetadata("*", Rows_PropertyChanged),
                new ValidateValueCallback(IsStringValueValid));

        /// <summary>
        /// Gets the value of <see cref="ColumnsProperty"/> for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">The given <see cref="DependencyObject"/>.</param>
        /// <returns>The value of <see cref="ColumnsProperty"/>.</returns>
        public static string GetColumns(DependencyObject obj) => (string)obj.GetValue(ColumnsProperty);

        /// <summary>
        /// Gets the value of <see cref="RowsProperty"/> for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">The given <see cref="DependencyObject"/>.</param>
        /// <returns>The value of <see cref="RowsProperty"/>.</returns>
        public static string GetRows(DependencyObject obj) => (string)obj.GetValue(RowsProperty);

        /// <summary>
        /// Sets the value of <see cref="ColumnsProperty"/> for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">The given <see cref="DependencyObject"/>.</param>
        /// <param name="value">The new value of <see cref="ColumnsProperty"/>.</param>
        public static void SetColumns(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        /// <summary>
        /// Sets the value of <see cref="RowsProperty"/> for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">The given <see cref="DependencyObject"/>.</param>
        /// <param name="value">The new value of <see cref="RowsProperty"/>.</param>
        public static void SetRows(DependencyObject obj, string value)
        {
            obj.SetValue(RowsProperty, value);
        }

        /// <summary>
        /// Represents the callback that is invoked when the effective property value of <see cref="ColumnsProperty"/> changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void Colums_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            if (grid != null)
            {
                grid.ColumnDefinitions.Clear();

                var inputValue = (string)e.NewValue;
                var values = inputValue.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var value in values)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = Helpers.ParseValue(value) });
                }
            }
        }

        /// <summary>
        /// Indicates whether the value of the property is valid.
        /// </summary>
        /// <param name="passedValue">The value passed to <see cref="ColumnsProperty"/> or <see cref="RowsProperty"/>.</param>
        /// <returns>True if the string is valid.</returns>
        private static bool IsStringValueValid(object passedValue)
        {
            var inputValue = (string)passedValue;
            var values = inputValue.Split(new[] { ' ', ',' });

            return values.All(Helpers.IsValidGridLengthValue);
        }

        /// <summary>
        /// Represents the callback that is invoked when the effective property value of <see cref="RowsProperty"/> changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void Rows_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            if (grid != null)
            {
                grid.RowDefinitions.Clear();

                var inputValue = (string)e.NewValue;
                var values = inputValue.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var value in values)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = Helpers.ParseValue(value) });
                }
            }
        }
    }
}