using SkiaSharp;

namespace XamarinGradientShape.Controls
{
	/// <summary>
	/// グラデーションを実現する矩形
	/// </summary>
	public class GradientRect : GradientShape
	{
		/// <summary>
		/// 背景を描画する
		/// </summary>
		/// <param name="canvas">描画対象のキャンバス</param>
		/// <param name="paint">描画する色の設定</param>
		/// <param name="rect">描画サイズ</param>
		protected override void DrawFill(SKCanvas canvas, SKPaint paint, SKRect rect)
		{
			canvas.DrawRoundRect(rect, CornerRadius, CornerRadius, paint);
		}

		/// <summary>
		/// 枠線を描画する
		/// </summary>
		/// <param name="canvas">描画対象のキャンバス</param>
		/// <param name="paint">描画する色の設定</param>
		/// <param name="rect">描画サイズ</param>
		protected override void DrawStroke(SKCanvas canvas, SKPaint paint, SKRect rect)
		{
			canvas.DrawRoundRect(rect, CornerRadius, CornerRadius, paint);
		}
	}
}
