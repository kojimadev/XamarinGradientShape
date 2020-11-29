using System;
using Xamarin.Forms;
using XamarinGradientShape.Model;

namespace XamarinGradientShape
{
	public partial class MainPage : ContentPage
	{
		private readonly Item _Item = new Item() {Status = Status.ToDo};
		
		public MainPage()
		{
			InitializeComponent();

			this.BindingContext = _Item;
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			switch (_Item.Status)
			{
				case Status.ToDo:
					_Item.Status = Status.Doing;
					break;
				case Status.Doing:
					_Item.Status = Status.Done;
					break;
				case Status.Done:
					_Item.Status = Status.ToDo;
					break;
			}
		}
	}
}
