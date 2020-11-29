using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinGradientShape.Model;
using XamarinGradientShape.Utilities;

namespace XamarinGradientShape.Converter
{
	public class StatusToGradientColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var status = (Status)value;
			switch (status)
			{
				case Status.ToDo:
					return GradientColor.DarkRed;
				case Status.Doing:
					return GradientColor.DarkYellow;
				case Status.Done:
					return GradientColor.LightBlue;
				default:
					throw new Exception("Invalid status");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
