
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	public sealed partial class GUI : MonoBehaviour
	{
		void Awake()
		{
			OnEntryDefaultCommands();
			layout.OnEnableElements = OnEnableElements;
			inputField?.onEndEdit.AddListener( OnEndEdit);
			Application.logMessageReceivedThreaded += OnLogMessageReceived;
		}
		void OnDestroy()
		{
			inputField?.onEndEdit.RemoveListener( OnEndEdit);
			Application.logMessageReceivedThreaded -= OnLogMessageReceived;
		}
		void Update()
		{
			lock( logs)
			{
				if( logs.Count != layout.ItemCount)
				{
					layout.ChangeItemCount( logs.Count);
					forceDownScroll = true;
					
				}
			}
		}
		bool forceDownScroll;
		
		void OnGUI()
		{
			if( Event.current.type == EventType.Repaint)
			{
				if( forceDownScroll != false)
				{
					layout.VerticalNormalizedPosition = 0.0f;
					forceDownScroll = false;
				}
			}
		}
		void OnLogMessageReceived( string condition, string stackTrace, LogType type)
		{
			string text = string.Format( "[{0}] {1}", System.DateTime.Now.ToLongTimeString(), condition);
			var color = Color.white;
			
			if( type != LogType.Log)
			{
				text += System.Environment.NewLine + stackTrace;
			}
			switch( type)
			{
				case LogType.Log:
				{
					color = logColor;
					break;
				}
				case LogType.Warning:
				{
					color = warningColor;
					break;
				}
				case LogType.Error:
				{
					color = errorColor;
					break;
				}
				case LogType.Assert:
				{
					color = assertColor;
					break;
				}
				case LogType.Exception:
				{
					color = exceptionColor;
					break;
				}
				default:
				{
					color = logColor;
					break;
				}
			}
			Append( text, color);
		}
		void Append( string text, Color color)
		{
			if( string.IsNullOrEmpty( text) == false)
			{
				string[] lines = text.Split( new[]{ "\r\n", "\n", "\r"}, System.StringSplitOptions.None);
				
				lock( logs)
				{
					for( int i0 = 0; i0 < lines.Length; ++i0)
					{
						logs.Add( new Log( lines[ i0], color));
					}
				}
			}
		}
		void OnEnableElements( RecycleElement elements, System.Action callback)
		{
			Log log;
			
			lock( logs)
			{
				log = logs[ elements.Index];
			}
			if( elements.GetComponent<Text>() is Text component)
			{
				string text = log.text;
				
				if( text.Length > 14000)
				{
					text = text.Substring( 0, 14000);
					text += "\n<message truncated>"; 
				}
				component.text = text;
				component.color = log.color;
			}
			callback();
		}
		public void OnEndEdit( string text)
		{
			if( string.IsNullOrEmpty( text) == false)
			{
				var color = Color.white;
				
				if( text[ 0] == '/')
				{
					System.Func<string, string[], string> func;
					
					string[] split = text.Split( ' ');
					
					if( commands.TryGetValue( split[ 0], out func) != false)
					{
						try
						{
							text = func( ">" + text, split);
						}
						catch( System.Exception e)
						{
							text = e.ToString();
						}
						ColorUtility.TryParseHtmlString( "#c8c8c8", out color);
					}
					else
					{
						text = ">" + text + "\n command not found.";
					}
				}
				Append( text, color);
				inputField.text = string.Empty;
				inputField.ActivateInputField();
			}
		}
		
	//	[SerializeField]
	//	CanvasScaler canvasScaler = default;
		[SerializeField]
		RecycleLayoutGroup layout = default;
		[SerializeField]
		InputField inputField = default;
		
		[SerializeField]
		Color logColor = new Color32( 200, 200, 200, 255);
		[SerializeField]
		Color warningColor = new Color32( 255, 204, 102, 255);
		[SerializeField]
		Color errorColor = new Color32( 255, 102, 102, 255);
		[SerializeField]
		Color assertColor = new Color32( 255, 102, 102, 255);
		[SerializeField]
		Color exceptionColor = new Color32( 255, 102, 102, 255);
		
		List<Log> logs = new List<Log>();
		Dictionary<string, System.Func<string, string[], string>> commands = 
			new Dictionary<string, System.Func<string, string[], string>>();
	}
	public class Log
	{
		public Log( string text, Color color)
		{
			this.text = text;
			this.color = color;
		}
		public string text;
		public Color color;
	}
}
