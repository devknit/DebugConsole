
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	[System.Serializable]
	public sealed class TextSettings
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
		[SerializeField]
		internal Font font = default;
		[SerializeField]
		internal FontStyle fontStyle = FontStyle.Normal;
		[SerializeField]
		internal int fontSize = 24;
		[SerializeField]
		internal float lineSpacing = 1.0f;
		[SerializeField]
		internal bool richText = true;
		[SerializeField]
		internal TextAnchor textAnchor = TextAnchor.UpperLeft;
		[SerializeField]
		internal bool alignByGeometry = false;
		[SerializeField]
		internal HorizontalWrapMode horizontalOverflow = HorizontalWrapMode.Wrap;
		[SerializeField]
		internal VerticalWrapMode verticalOverflow = VerticalWrapMode.Truncate;
		[SerializeField]
		internal bool resizeTextForBestFit = false;
		[SerializeField]
		internal int resizeTextMinSize = 1;
		[SerializeField]
		internal int resizeTextMaxSize = 40;
		[SerializeField]
		internal Vector2 pivot = Vector2.up;
		[SerializeField]
		internal float scaleFactor = 1.0f;
		[SerializeField]
		internal bool generateOutOfBounds = false;
		[SerializeField]
		internal bool updateBounds = false;
	}
}
