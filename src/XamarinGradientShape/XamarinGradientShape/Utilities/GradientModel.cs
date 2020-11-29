using SkiaSharp;

namespace XamarinGradientShape.Utilities
{
	/// <summary>
	/// グラデーション設定
	/// </summary>
	public class GradientModel
	{
		/// <summary>
		/// グラデーションで使用する色
		/// </summary>
		public SKColor[] Colors { get; set; }

		/// <summary>
		/// グラデーションのポジション
		/// </summary>
		public float[] ColorPos { get; set; }
	}
}
