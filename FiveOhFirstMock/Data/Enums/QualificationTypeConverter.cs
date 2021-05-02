using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Data.Enums
{
    public class QualificationTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value is string raw)
            {
                var parts = raw.Split(",", StringSplitOptions.RemoveEmptyEntries);
                long qual = 0x0;
                foreach(var p in parts)
                {
                    if(long.TryParse(p, out var val))
                    {
                        qual |= val;
                    }
                }

                return (Qualifications)qual;
            }
            
            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            List<string> output = new();
            if(value is Qualifications quals)
            {
                foreach(Qualifications q in Enum.GetValues(typeof(Qualifications)))
                {
                    if ((quals & q) == q)
                        output.Add(q.ToString());
                }

                return string.Join(",", output);
            }
            else
            {
                throw new Exception("Cannot convert Qualification enum to anything other than a string.");
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
