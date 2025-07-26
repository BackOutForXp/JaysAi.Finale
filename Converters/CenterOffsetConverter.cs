// CenterOffsetConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;

public class CenterOffsetConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double screenDim && parameter is string sizeStr && double.TryParse(sizeStr, out double elementSize))
            return (screenDim / 2) - (elementSize / 2);
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
