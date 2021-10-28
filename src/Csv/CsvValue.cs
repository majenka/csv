using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majenka.Csv
{
    public class CsvValue
    {
        readonly string value;

        public CsvValue(string value)
        {
            this.value = value;
        }

        public static implicit operator string(CsvValue csvValue)
        {
            return csvValue.value;
        }

        public static implicit operator CsvValue(string csvValue)
        {
            return new CsvValue(csvValue);
        }

        public override string ToString()
        {
            return value;
        }

        public bool? ToBoolean()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToBoolean(value);
        }
                
        public char? ToChar()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToChar(value);
        }

        public DateTime? ToDateTime()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToDateTime(value);
        }

        public float? ToFloat()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToSingle(value);
        }

        public decimal? ToDecimal()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToDecimal(value);
        }

        public double? ToDouble()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToDouble(value);
        }

        public byte? ToByte()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToByte(value);
        }

        public short? ToShort()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToInt16(value);
        }

        public int? ToInt()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToInt32(value);
        }

        public long? ToLong()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToInt64(value);
        }

        public sbyte? ToSByte()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToSByte(value);
        }

        public ushort? ToSingle()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToUInt16(value);
        }

        public uint? ToUInt()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToUInt32(value);
        }

        public ulong? ToULong()
        {
            return string.IsNullOrEmpty(value) ? null : Convert.ToUInt64(value);
        }
    }
}