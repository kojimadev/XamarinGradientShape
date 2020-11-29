using SkiaSharp;

namespace XamarinGradientShape.Controls
{
	/// <summary>
	/// グラデーションを実現する円
	/// </summary>
	public class GradientCircle : GradientShape
    {
	    /// <summary>
	    /// 背景を描画する
	    /// </summary>
	    /// <param name="canvas">描画対象のキャンバス</param>
	    /// <param name="paint">描画する色の設定</param>
	    /// <param name="rect">描画サイズ</param>
	    protected override void DrawFill(SKCanvas canvas, SKPaint paint, SKRect rect)
	    {
			float radius = GetRadius(rect);
			canvas.DrawCircle((float)rect.MidX, (float)rect.MidY, radius, paint);
		}

	    /// <summary>
	    /// 枠線を描画する
	    /// </summary>
	    /// <param name="canvas">描画対象のキャンバス</param>
	    /// <param name="paint">描画する色の設定</param>
	    /// <param name="rect">描画サイズ</param>
	    protected override void DrawStroke(SKCanvas canvas, SKPaint paint, SKRect rect)
	    {
		    float radius = GetRadius(rect);
		    canvas.DrawCircle(rect.MidX, rect.MidY, radius, paint);
	    }
	}
}
