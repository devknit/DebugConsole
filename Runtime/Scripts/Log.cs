
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	public sealed class Log
	{
		public Log( string text, Color color)
		{
			this.text = text;
			this.color = color;
			generator = new TextGenerator();
		}
		public Vector2 CalculateSize( TextGenerationSettings settings)
		{
			size.x = generator.GetPreferredWidth( text, settings);
			size.y = generator.GetPreferredHeight( text, settings);
			return size;
		}
		public string text;
		public Color color;
		public Vector2 size;
		TextGenerator generator;
	}
}
