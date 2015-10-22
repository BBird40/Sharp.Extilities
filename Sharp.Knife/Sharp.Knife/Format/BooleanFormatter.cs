using System;

namespace Sharp.Knife.Format
{
    /// <summary>
    /// Changes how true and false flags will be string formatted.
    /// </summary>
    public class BooleanFormatter : IFormatProvider, ICustomFormatter
    {
        private readonly string _trueFormat;
        private readonly string _falseFormat;

        /// <summary>
        /// Format Boolean True/False values to another format.
        /// </summary>
        /// <param name="trueFormat">string representation for true.</param>
        /// <param name="falseFormat">string representation for false.</param>
        public BooleanFormatter(string trueFormat, string falseFormat)
        {
            _trueFormat = trueFormat;
            _falseFormat = falseFormat;
        }

        /// <summary>
        /// String.Format calls this method to get an instance of an ICustomerFormatter to handle the formatting
        /// </summary>
        /// <param name="formatType">Custom Format Object.</param>
        /// <returns>Return the custom format object if it was passed.</returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        /// <summary>
        /// Formats a boolean value to the format specified in the constructor.
        /// </summary>
        /// <param name="format">The string formation to use with the arg.</param>
        /// <param name="arg">The argument(s) to be used with the format.</param>
        /// <param name="formatProvider">FormatPovider to use in method.</param>
        /// <returns>the boolean value of the argument converted.</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            try
            {
                var booleanValue = Convert.ToBoolean(arg);
                return booleanValue ? _trueFormat : _falseFormat;
            }
            catch (Exception)
            {
                // TODO: Log the exception.
                return string.Empty;
            }
        }
    }
}