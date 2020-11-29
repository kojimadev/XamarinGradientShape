using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinGradientShape.Utilities
{
	/// <summary>
	/// グラデーションの設定を作成するクラス
	/// </summary>
	public class GradientModelFactory
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		private GradientModelFactory() { }

		/// <summary>
		/// インスタンス
		/// </summary>
		public static GradientModelFactory Instance => new GradientModelFactory();

		/// <summary>
		/// グラデーションの設定を作成して返す
		/// </summary>
		/// <param name="gradientColor">グラデーションのタイプ</param>
		/// <returns>グラデーションの設定</returns>
		public GradientModel CreateGradientModel(GradientColor gradientColor)
		{
			switch (gradientColor)
			{
				case GradientColor.Transparent:
					// 透明色の場合はnullを返す
					return null;
				case GradientColor.DarkRed:
					return new GradientModel()
					{
						Colors = new SKColor[] { SKColors.Red, SKColors.DarkRed },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.LightBlue:
					return new GradientModel()
					{
						Colors = new SKColor[] { SKColors.AliceBlue, SKColors.LightBlue },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.DarkYellow:
					return new GradientModel()
					{
						Colors = new SKColor[] { Color.FromHex("#FFFFA500").ToSKColor(), Color.FromHex("#FF90B000").ToSKColor() },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.Black:
					return new GradientModel()
					{
						// 3色のグラデーションの場合はColorsとColorPosに3つを指定
						Colors = new SKColor[] { SKColors.White, SKColors.LightGray, SKColors.Black },
						ColorPos = new float[] { 0, 0.5f, 1 }
					};
				case GradientColor.Gray:
					return new GradientModel()
					{
						Colors = new SKColor[] { SKColors.LightGray, SKColors.Gray },
						ColorPos = new float[] { 0, 1 }
					};
				default:
					throw new Exception("Invalid GradientColor");
			}
		}
	}
}
