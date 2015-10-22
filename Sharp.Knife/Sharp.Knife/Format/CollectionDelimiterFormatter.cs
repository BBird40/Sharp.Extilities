using System;
using System.Collections;
using System.Text;

namespace Sharp.Knife.Format
{
    /// <summary>
    /// Formats IEnumerable of objects with the abstract FormatObject method.
    /// </summary>
    public abstract class CollectionDelimiterFormatter : IFormatProvider, ICustomFormatter
    {
        protected string Delimiter;

        /// <summary>
        /// The Collection Formatter with a delimiter.
        /// </summary>
        /// <param name="delimiter">The string to use to delimit the list.</param>
        protected CollectionDelimiterFormatter(string delimiter)
        {
            Delimiter = delimiter;
        }

        /// <summary>
        /// String.Format calls this method to get an instance of an ICustomerFormatter to handle the formatting.
        /// </summary>
        /// <param name="formatType">Custom Format Object.</param>
        /// <returns>Return the custom format object if it was passed.</returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        /// <summary>
        /// Formats the IEnumerable with the given delimiter.
        /// </summary>
        /// <param name="format">The string formation to use with the arg.</param>
        /// <param name="arg">The IEnumerable to be used with the format.</param>
        /// <param name="formatProvider">FormatPovider to use in method.</param>
        public virtual string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var objects = arg as IEnumerable;
            if (objects == null) return string.Empty;

            var stringBuilder = new StringBuilder();

            foreach (var element in objects)
                stringBuilder.Append(FormatObject(element)).Append(Delimiter);

            if (stringBuilder.Length <= 0) return string.Empty;

            stringBuilder.Length = stringBuilder.Length - Delimiter.Length;
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Formats the objects in the IEnumerable
        /// </summary>
        /// <param name="aObject">The object to format.</param>
        /// <returns>The returned formation of aObject.</returns>
        public abstract string FormatObject(object aObject);
    }
}