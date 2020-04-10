
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	[System.Serializable]
	internal sealed class LogSetting
	{
		public LogSetting( Color color, bool stackTrace)
		{
			this.color = color;
			this.stackTrace = stackTrace;
		}
		[SerializeField]
		internal Color color;
		[SerializeField]
		internal bool stackTrace;
	}
	[System.Serializable]
	internal sealed class LogTextSettings : TextSettings
	{
		internal LogSetting GetLogSetting( LogType type)
		{
			LogSetting logSetting;
			
			switch( type)
			{
				case LogType.Log:
				{
					logSetting = normal;
					break;
				}
				case LogType.Warning:
				{
					logSetting = warning;
					break;
				}
				case LogType.Error:
				{
					logSetting = error;
					break;
				}
				case LogType.Assert:
				{
					logSetting = assert;
					break;
				}
				case LogType.Exception:
				{
					logSetting = exception;
					break;
				}
				default:
				{
					logSetting = normal;
					break;
				}
			}
			return logSetting;
		}
		[HeaderAttribute("Log Text Settings")]
		[SerializeField]
		internal LogSetting normal = new LogSetting( new Color32( 200, 200, 200, 255), false);
		[SerializeField]
		internal LogSetting warning = new LogSetting( new Color32( 255, 204, 102, 255), true);
		[SerializeField]
		internal LogSetting error = new LogSetting( new Color32( 255, 102, 102, 255), true);
		[SerializeField]
		internal LogSetting assert = new LogSetting( new Color32( 255, 0, 0, 255), true);
		[SerializeField]
		internal LogSetting exception = new LogSetting( new Color32( 255, 0, 255, 255), true);
		[SerializeField]
		internal bool lineSplit = true;
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
		public int fontSize = 14;
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
