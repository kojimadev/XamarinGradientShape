﻿using System;
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
				case GradientColor.LinerDarkRed:
					return new GradientModel()
					{
						ShaderType = ShaderType.Liner,
						Colors = new SKColor[] { SKColors.Red, SKColors.DarkRed },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.LinerLightBlue:
					return new GradientModel()
					{
						ShaderType = ShaderType.Liner,
						Colors = new SKColor[] { SKColors.AliceBlue, SKColors.LightBlue },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.LinerDarkYellow:
					return new GradientModel()
					{
						ShaderType = ShaderType.Liner,
						Colors = new SKColor[] { Color.FromHex("#FFFFA500").ToSKColor(), Color.FromHex("#FF90B000").ToSKColor() },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.LinerBlack:
					return new GradientModel()
					{
						// 3色のグラデーションの場合はColorsとColorPosに3つを指定
						ShaderType = ShaderType.Liner,
						Colors = new SKColor[] { SKColors.White, SKColors.LightGray, SKColors.Black },
						ColorPos = new float[] { 0, 0.5f, 1 }
					};
				case GradientColor.LinerGray:
					return new GradientModel()
					{
						ShaderType = ShaderType.Liner,
						Colors = new SKColor[] { SKColors.LightGray, SKColors.Gray },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.RadialRedBrush:
					return new GradientModel()
					{
						// 放射状グラデーション
						ShaderType = ShaderType.Radial,
						Colors = new SKColor[] { SKColors.White, Color.FromHex("#FFFF2020").ToSKColor() },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.RadialBlueBrush:
					return new GradientModel()
					{
						// 放射状グラデーション
						ShaderType = ShaderType.Radial,
						Colors = new SKColor[] { SKColors.White, Color.FromHex("#FF2020FF").ToSKColor() },
						ColorPos = new float[] { 0, 1 }
					};
				case GradientColor.RadialYellowBrush:
					return new GradientModel()
					{
						// 放射状グラデーション
						ShaderType = ShaderType.Radial,
						Colors = new SKColor[] { SKColors.White, SKColors.Yellow },
						ColorPos = new float[] { 0, 1 }
					};
				default:
					throw new Exception("Invalid GradientColor");
			}
		}
	}
}
