using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace TestETL.Application.Converts
{
    public class ZeroIfEmptyInt32Converter : Int32Converter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }
            else
            {
                if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
                {
                    return result;
                }
                else
                {
                    return base.ConvertFromString(text, row, memberMapData);
                }
            }
        }
    }
}
