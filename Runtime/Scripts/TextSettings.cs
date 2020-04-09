
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	[System.Serializable]
	internal sealed class LogTextSettings : TextSettings
	{
		[HeaderAttribute("Log Text Settings")]
		[SerializeField]
		internal bool lineSplit = true;
		[SerializeField]
		internal Color normalColor = new Color32( 200, 200, 200, 255);
		[SerializeField]
		internal Color warningColor = new Color32( 255, 204, 102, 255);
		[SerializeField]
		internal Color errorColor = new Color32( 255, 102, 102, 255);
		[SerializeField]
		internal Color assertColor = new Color32( 255, 0, 0, 255);
		[SerializeField]
		internal Color exceptionColor = new Color32( 255, 0, 255, 255);
	}
	[System.Serializable]
	public class TextSettings
	{
		public void Apply( TextGenerationSettings settings)
		{
			font = settings.font;
			fontStyle = settings.fontStyle;
			fontSize = settings.fontSize;
			lineSpacing = settings.lineSpacing;
			richText = settings.richText;
			textAnchor = settings.textAnchor;
			alignByGeometry = settings.alignByGeometry;
			horizontalOverflow = settings.horizontalOverflow;
			verticalOverflow = settings.verticalOverflow;
			resizeTextForBestFit = settings.resizeTextForBestFit;
			resizeTextMinSize = settings.resizeTextMinSize;
			resizeTextMaxSize = settings.resizeTextMaxSize;
			pivot = settings.pivot;
			scaleFactor = settings.scaleFactor;
			generateOutOfBounds = settings.generateOutOfBounds;
			updateBounds = settings.updateBounds;
		}
		public void ApplyComponent( Text component, string text, Color color)
		{
			component.text = text;
			component.color = color;
			component.font = font;
			component.fontStyle = fontStyle;
			component.fontSize = fontSize;
			component.lineSpacing = lineSpacing;
			component.supportRichText = richText;
			component.alignment = textAnchor;
			component.alignByGeometry = alignByGeometry;
			component.horizontalOverflow = horizontalOverflow;
			component.verticalOverflow = verticalOverflow;
			component.resizeTextForBestFit = resizeTextForBestFit;
			component.resizeTextMinSize = resizeTextMinSize;
			component.resizeTextMaxSize = resizeTextMaxSize;
		}
		public TextGenerationSettings GetGenerationSettings( Vector2 extents)
		{
			TextGenerationSettings settings;
			settings.color = Color.white;
			settings.font = font;
			settings.fontStyle = fontStyle;
			settings.fontSize = fontSize;
			settings.lineSpacing = lineSpacing;
			settings.richText = richText;
			settings.textAnchor = textAnchor;
			settings.alignByGeometry = alignByGeometry;
			settings.horizontalOverflow = horizontalOverflow;
			settings.verticalOverflow = verticalOverflow;
			settings.resizeTextForBestFit = resizeTextForBestFit;
			settings.resizeTextMinSize = resizeTextMinSize;
			settings.resizeTextMaxSize = resizeTextMaxSize;
			settings.pivot = pivot;
			settings.scaleFactor = scaleFactor;
			settings.generateOutOfBounds = generateOutOfBounds;
			settings.generationExtents = extents;
			settings.updateBounds = updateBounds;
			return settings;
		}
		[HeaderAttribute("Text Generation Settings")]
		[SerializeField]
		public Font font = default;
		[SerializeField]
		public FontStyle fontStyle = FontStyle.Normal;
		[SerializeField]
		public int fontSize = 24;
		[SerializeField]
		public float lineSpacing = 1.0f;
		[SerializeField]
		public bool richText = true;
		[SerializeField]
		public TextAnchor textAnchor = TextAnchor.UpperLeft;
		[SerializeField]
		public bool alignByGeometry = false;
		[SerializeField]
		public HorizontalWrapMode horizontalOverflow = HorizontalWrapMode.Wrap;
		[SerializeField]
		public VerticalWrapMode verticalOverflow = VerticalWrapMode.Truncate;
		[SerializeField]
		public bool resizeTextForBestFit = false;
		[SerializeField]
		public int resizeTextMinSize = 1;
		[SerializeField]
		public int resizeTextMaxSize = 40;
		[SerializeField]
		public Vector2 pivot = Vector2.up;
		[SerializeField]
		public float scaleFactor = 1.0f;
		[SerializeField]
		public bool generateOutOfBounds = false;
		[SerializeField]
		public bool updateBounds = false;
	}
}
