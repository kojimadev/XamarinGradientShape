using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinGradientShape.Utilities;

namespace XamarinGradientShape.Controls
{
	/// <summary>
	/// グラデーションの矩形を描画するクラス
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GradientRect : ContentView
	{
		#region 依存関係プロパティ

		/// <summary>
		/// BackGradientColor 依存関係プロパティ
		/// </summary>
		public static readonly BindableProperty BackGradientColorProperty = BindableProperty.Create(
			"BackGradientColor", // プロパティ名
			typeof(GradientColor), // プロパティの型
			typeof(GradientRect), // プロパティを持つ View の型
			GradientColor.Transparent, // 初期値
			BindingMode.TwoWay, // バインド方向
			null, // バリデーションメソッド
			BackGradientColorPropertyChanged, // 変更後イベントハンドラ
			null, // 変更時イベントハンドラ
			null);

		#endregion

		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GradientRect()
		{
			InitializeComponent();
		}

		#endregion

		#region 公開プロパティ

		/// <summary>
		/// 背景のグラデーションのタイプ
		/// </summary>
		public GradientColor BackGradientColor
		{
			get => (GradientColor)GetValue(BackGradientColorProperty);
			set => SetValue(BackGradientColorProperty, value);
		}

		/// <summary>
		/// Borderの色
		/// </summary>
		public Color BorderColor { get; set; }

		/// <summary>
		/// Borderの線の太さ
		/// </summary>
		public float StrokeWidth { get; set; }

		/// <summary>
		/// 矩形を描画する場合の角丸にする円の半径
		/// </summary>
		public int CornerRadius { get; set; }

		/// <summary>
		/// 初期描画済みか
		/// </summary>
		public bool Initialized { get; private set; } = false;

		#endregion

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
			canvas.Clear();

			// 描画範囲をコントロール全体として指定
			SKRect rect = new SKRect(0, 0, info.Width, info.Height);

			// グラデーション設定を取得
			GradientModel gradientModel = GradientModelFactory.Instance.CreateGradientModel(BackGradientColor);
			if (gradientModel == null)
			{
				return;
			}

			// 塗りつぶしのグラデーション用のSKPaintを作成
			SKPaint paint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Fill,
				Shader = SKShader.CreateLinearGradient(new SKPoint(rect.Left, rect.Top), new SKPoint(rect.Left, rect.Bottom),
					gradientModel.Colors, gradientModel.ColorPos, SKShaderTileMode.Clamp)
			};
			// 背景を描画
			canvas.DrawRoundRect(rect, CornerRadius, CornerRadius, paint);

			// 枠線用にSKPaintを変更
			paint.Style = SKPaintStyle.Stroke;
			paint.Shader = null;
			paint.Color = BorderColor.ToSKColor();
			paint.StrokeWidth = StrokeWidth;
			// 枠線の描画
			canvas.DrawRoundRect(rect, CornerRadius, CornerRadius, paint);

			// 初期描画済み
			Initialized = true;
		}

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
			GradientRect gradientRect = (GradientRect)bindable;

			// 初期描画前であれば何もしない
			if (gradientRect.Initialized == false)
			{
				return;
			}

			// 再描画する
			gradientRect._SkCanvasView.InvalidateSurface();
		}
	}
}