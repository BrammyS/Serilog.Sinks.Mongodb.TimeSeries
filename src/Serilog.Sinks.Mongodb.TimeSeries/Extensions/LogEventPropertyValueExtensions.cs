using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Serilog.Events;

namespace Serilog.Sinks.Mongodb.TimeSeries.Extensions
{
    public static class LogEventPropertyValueExtensions
    {
        /// <summary>
        ///     Converts the <see cref="LogEventPropertyValue"/> to a <see cref="BsonValue"/>
        /// </summary>
        /// <param name="propertyValue">The value to convert (possibly null).</param>
        /// <param name="format">A format string applied to the value, or null.</param>
        /// <param name="formatProvider">A format provider to apply to the value, or null to use the default.</param>
        /// <returns>
        ///     The converted <see cref="BsonValue"/>.
        /// </returns>
        public static BsonValue ToBsonValue(this LogEventPropertyValue propertyValue, string? format = null, IFormatProvider? formatProvider = null)
        {
            try
            {
                if (propertyValue is ScalarValue scalar)
                {
                    return ConvertScalar(scalar.Value);
                }

                if (propertyValue is DictionaryValue dict)
                {
                    var bsonDict = new Dictionary<BsonValue, BsonValue>();
                    foreach (var (key, value) in dict.Elements)
                    {
                        bsonDict.Add(ConvertScalar(key), value.ToBsonValue(format, formatProvider));
                    }
                
                    return BsonValue.Create(bsonDict);
                }

                if (propertyValue is SequenceValue seq)
                {
                    var bsonList = new List<BsonValue>();
                    foreach (var value in seq.Elements)
                    {
                        bsonList.Add(value.ToBsonValue(format, formatProvider));
                    }
                
                    return BsonValue.Create(bsonList);
                }

                if (propertyValue is StructureValue str)
                {
                    return BsonValue.Create(str.ToString(format, formatProvider));
                }

                return BsonValue.Create(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static BsonValue ConvertScalar(object? value)
        {
            if (value is null) return BsonValue.Create(null);

            var valueType = value.GetType();

            if (valueType == typeof(string)) return BsonValue.Create((string)value);
            if (valueType == typeof(byte[])) return BsonValue.Create((byte[])value);
            if (valueType == typeof(int)) return BsonValue.Create((int)value);
            if (valueType == typeof(long)) return BsonValue.Create((long)value);
            if (valueType == typeof(bool)) return BsonValue.Create((bool)value);
            if (valueType == typeof(double)) return BsonValue.Create((double)value);
            if (valueType == typeof(DateTimeOffset)) return BsonValue.Create(((DateTimeOffset)value).Date);
            if (valueType == typeof(DateTime)) return BsonValue.Create((DateTime)value);

            return BsonValue.Create(value.ToString());
        }
    }
}