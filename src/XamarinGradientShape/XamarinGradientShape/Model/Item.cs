using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinGradientShape.Model
{
	public class Item : INotifyPropertyChanged
	{
		private Status _Status;
		public Status Status
		{
			get => _Status;
			set
			{
				_Status = value;
				OnPropertyChanged();
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
