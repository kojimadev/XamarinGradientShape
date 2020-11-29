using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinGradientShape.Utilities;

namespace XamarinGradientShape.Controls
{
	/// <summary>
	/// 背景をグラデーション描画するコントロール
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public abstract partial class GradientShape : ContentView
	{
		#region 依存関係プロパティ

		/// <summary>
		/// BackGradientColor プロパティ
		/// </summary>
		public static readonly BindableProperty BackGradientColorProperty = BindableProperty.Create(
			"BackGradientColor", // プロパティ名
			typeof(GradientColor), // プロパティの型
			typeof(GradientShape), // プロパティを持つ View の型
			GradientColor.Transparent, // 初期値
			BindingMode.TwoWay, // バインド方向
			null, // バリデーションメソッド
			BackGradientColorPropertyChanged, // 変更後イベントハンドラ
			null, // 変更時イベントハンドラ
			null);

		/// <summary>
		/// BackGradientColor変更後ハンドラ
		/// </summary>
		/// <param name="bindable"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private static void BackGradientColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (bindable == null || newValue == null ||
			    newValue.GetType() != BackGradientColorProperty.ReturnType)
			{
				return;
			}
			GradientShape gradientShape = (GradientShape) bindable;

			// 初期描画前は何もしない
			if (gradientShape.Initialized == false)
			{
				return;
			}

			// 再描画する
			gradientShape._SkCanvasView.InvalidateSurface();
		}

		/// <summary>
		/// BorderColor プロパティ
		/// </summary>
		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
			"BorderColor", // プロパティ名
			typeof(Color), // プロパティの型
			typeof(GradientShape), // プロパティを持つ View の型
			Color.Gray, // 初期値
			BindingMode.TwoWay, // バインド方向
			null, // バリデーションメソッド
			null, // 変更後イベントハンドラ
			null, // 変更時イベントハンドラ
			null);

		/// <summary>
		/// StrokeWidth プロパティ
		/// </summary>
		public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create(
			"StrokeWidth", // プロパティ名
			typeof(float), // プロパティの型
			typeof(GradientShape), // プロパティを持つ View の型
			1f, // 初期値
			BindingMode.TwoWay, // バインド方向
			null, // バリデーションメソッド
			null, // 変更後イベントハンドラ
			null, // 変更時イベントハンドラ
			null);

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		protected GradientShape()
		{
			InitializeComponent();
		}

		#endregion

		#region 公開プロパティ

		/// <summary>
		/// 初期描画済みか
		/// </summary>
		public bool Initialized { get; private set; } = false;

		/// <summary>
		/// 背景のグラデーションのタイプ(BackgroundColorよりも優先する)
		/// </summary>
		public GradientColor BackGradientColor
		{
			get => (GradientColor)GetValue(BackGradientColorProperty);
			set => SetValue(BackGradientColorProperty, value);
		}

		/// <summary>
		/// Borderの色
		/// </summary>
		public Color BorderColor
		{
			get => (Color)GetValue(BorderColorProperty);
			set => SetValue(BorderColorProperty, value);
		}

		/// <summary>
		/// Borderの線の太さ
		/// </summary>
		public float StrokeWidth
		{
			get => (float)GetValue(StrokeWidthProperty);
			set => SetValue(StrokeWidthProperty, value);
		}

		/// <summary>
		/// 矩形を描画する場合の角丸にする円の半径
		/// </summary>
		public int CornerRadius { get; set; } = 0;

		#endregion

		#region イベントハンドラ

		/// <summary>
		/// 描画するハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			SKImageInfo info = e.Info;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;
			PaintSurface(canvas, info);
		}

		#endregion

		#region 内部処理

		/// <summary>
		/// 対象キャンバスを描画する
		/// </summary>
		/// <param name="canvas">描画対象のキャンバス</param>
		/// <param name="info">描画対象領域のサイズなどの情報</param>
		protected virtual void PaintSurface(SKCanvas canvas, SKImageInfo info)
		{
			// 初期描画済みとする
			Initialized = true;

			// canvas上にすでに何かを描画済みであればいったんクリアする(透明色にする)
			canvas.Clear();

			// 描画範囲をコントロール全体として指定
			SKRect rect = new SKRect(0, 0, info.Width, info.Height);

			// グラデーション設定を取得
			GradientModel gradientModel = GradientModelFactory.Instance.CreateGradientModel(BackGradientColor);
			if (gradientModel == null && BackgroundColor == Color.Transparent)
			{
				// グラデーション設定もグラデーション無しの背景色も、どちらも未設定であれば透明とする
				return;
			}

			// 塗りつぶしのグラデーション用のSKPaintを作成
			using (SKPaint paint = CreateSKPaint())
			{
				if (gradientModel != null)
				{
					// 背景グラデーション用のShaderの設定
					paint.Shader = CreateShader(gradientModel, rect);
				}
				else
				{
					// グラデーション設定がない場合はBackgroundColorから取得
					paint.Color = BackgroundColor.ToSKColor();
				}

				// 背景を描画
				DrawFill(canvas, paint, rect);
			}

			// 枠線の太さが0に近ければ枠線は描画しない
			if (StrokeWidth < 0.01) return;

			// 枠線用のSKPaintを作成
			using (SKPaint paint = CreateSKPaint(BorderColor.ToSKColor(), SKPaintStyle.Stroke, StrokeWidth))
			{
				// 枠線を描画
				DrawStroke(canvas, paint, rect);
			}
		}

		/// <summary>
		/// 背景を描画する
		/// </summary>
		/// <param name="canvas">描画対象のキャンバス</param>
		/// <param name="paint">描画する色の設定</param>
		/// <param name="rect">描画サイズ</param>
		protected abstract void DrawFill(SKCanvas canvas, SKPaint paint, SKRect rect);

		/// <summary>
		/// 枠線を描画する
		/// </summary>
		/// <param name="canvas">描画対象のキャンバス</param>
		/// <param name="paint">描画する色の設定</param>
		/// <param name="rect">描画サイズ</param>
		protected abstract void DrawStroke(SKCanvas canvas, SKPaint paint, SKRect rect);

		/// <summary>
		/// 矩形から円の半径を算出する
		/// </summary>
		/// <param name="rect">矩形</param>
		/// <returns>半径</returns>
		protected static float GetRadius(SKRect rect)
		{
			// 円から絶対にはみ出ないように半径は1小さくする
			return (float)Math.Min(rect.Width, rect.Height) / 2 - 1;
		}

		/// <summary>
		/// SKPaintを作成する
		/// </summary>
		/// <param name="style">塗りつぶしか枠線か</param>
		/// <param name="strokeWidth">枠線の場合の線の太さ</param>
		/// <returns>作成したSKPaint</returns>
		protected static SKPaint CreateSKPaint(SKPaintStyle style = SKPaintStyle.Fill, float strokeWidth = 0)
		{
			return CreateSKPaint(SKColors.Black, style, strokeWidth);
		}

		/// <summary>
		/// SKPaintを作成する
		/// </summary>
		/// <param name="color">色</param>
		/// <param name="style">塗りつぶしか枠線か</param>
		/// <param name="strokeWidth">枠線の場合の線の太さ</param>
		/// <returns>作成したSKPaint</returns>
		protected static SKPaint CreateSKPaint(SKColor color, SKPaintStyle style = SKPaintStyle.Fill, float strokeWidth = 0)
		{
			return new SKPaint()
			{
				IsAntialias = true,
				Color = color,
				Style = style,
				StrokeWidth = strokeWidth,
			};
		}

		/// <summary>
		/// グラデーション用のShaderを作成する
		/// </summary>
		/// <param name="gradientModel">グラデーション設定</param>
		/// <param name="rect">対象の矩形範囲</param>
		/// <returns>作成したShader</returns>
		protected static SKShader CreateShader(GradientModel gradientModel, SKRect rect)
		{
			switch (gradientModel.ShaderType)
			{
				case ShaderType.Liner:
					// 矩形の場合
					return SKShader.CreateLinearGradient(
						new SKPoint(rect.Left, rect.Top),
						new SKPoint(rect.Left, rect.Bottom),
						gradientModel.Colors,
						gradientModel.ColorPos,
						SKShaderTileMode.Clamp);
				case ShaderType.Radial:
					// 円の場合
					// 矩形のサイズから円の半径を算出する
					float radius = GetRadius(rect);
					// グラデーションの始点位置を中心から左上にずらし、そのずれた分だけ半径を大きくする
					return SKShader.CreateRadialGradient(
						new SKPoint(rect.MidX - radius * 0.5f, rect.MidY - radius * 0.5f),
						radius + radius * 0.5f,
						gradientModel.Colors,
						gradientModel.ColorPos,
						SKShaderTileMode.Clamp);
				default:
					return null;
			}
		}

		#endregion
	}
}