
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	public sealed partial class Console : MonoBehaviour
	{
		void Awake()
		{
			OnEntryDefaultCommands();
			contentLayout.OnElementSize = OnElementSize;
			contentLayout.OnEnableElements = OnEnableElements;
			inputField?.onEndEdit.AddListener( OnEndEdit);
			Application.logMessageReceivedThreaded += OnLogMessageReceived;
		}
		void Start()
		{
			if( inputField != null && inputFieldFont != null)
			{
				if( inputField.placeholder is Text placeholder)
				{
					placeholder.font = inputFieldFont;
				}
				if( inputField.textComponent != null)
				{
					inputField.textComponent.font = inputFieldFont;
				}
			}
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
				if( logs.Count != contentLayout.ItemCount)
				{
					contentLayout.ChangeItemCount( logs.Count);
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
					color = logTextSettings.normalColor;
					break;
				}
				case LogType.Warning:
				{
					color = logTextSettings.warningColor;
					break;
				}
				case LogType.Error:
				{
					color = logTextSettings.errorColor;
					break;
				}
				case LogType.Assert:
				{
					color = logTextSettings.assertColor;
					break;
				}
				case LogType.Exception:
				{
					color = logTextSettings.exceptionColor;
					break;
				}
				default:
				{
					color = logTextSettings.normalColor;
					break;
				}
			}
			Append( text, color);
		}
		void Append( string text, Color color)
		{
			if( string.IsNullOrEmpty( text) == false)
			{
				if( logTextSettings.lineSplit == false)
				{
					lock( logs)
					{
						logs.Add( new Log( text, color));
					}
				}
				else
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
		}
		Vector2 OnElementSize( int index)
		{
			var generationSettings = logTextSettings.GetGenerationSettings( 
				new Vector2( contentLayout.ContentTransform.rect.width, 4096));
			var ret = new Vector2( 0, 0);
			
			lock( logs)
			{
				ret = logs[ index].CalculateSize( generationSettings);
			}
			return ret;
		}
		void OnEnableElements( RecycleElement elements, System.Action callback)
		{
			Log log;
			
			lock( logs)
			{
				log = logs[ elements.Index];
			}
			if( elements.GetComponentInChildren<Text>() is Text component)
			{
				string text = log.text;
				
				if( text.Length > 14000)
				{
					text = text.Substring( 0, 14000);
					text += "\n<message truncated>"; 
				}
				logTextSettings.ApplyComponent( component, text, log.color);
			}
			callback();
		}
		public void OnEndEdit( string text)
		{
			if( string.IsNullOrEmpty( text) == false)
			{
				var color = Color.white;
				
				if( text[ 0] == kCommandPrefix)
				{
					System.Func<string, string[], string> func;
					
					string[] args = text.Split( ' ');
					
					if( commands.TryGetValue( args[ 0], out func) != false)
					{
						try
						{
							text = func( ">" + text, args);
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
	#if UNITY_EDITOR
		[UnityEditor.MenuItem("GameObject/UI/Debug Console/Screen Space - Overlay", false, 10000)]
		static void CreateWithOverlay()
		{
			Create( "3e5bdfe0eb6d38c4bb8c7ed1da393c63");
		}
		[UnityEditor.MenuItem("GameObject/UI/Debug Console/Screen Space - Camera", false, 10000)]
		static void CreateWithCamera()
		{
			Create( "f103627363cd792419ae83183ad47cba");
		}
		static void Create( string guid)
		{
			string path = UnityEditor.AssetDatabase.GUIDToAssetPath( guid);
			if( string.IsNullOrEmpty( path) == false)
			{
				if( UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>( path) is GameObject prefab)
				{
					GameObject newGameObject = GameObject.Instantiate( prefab);
					newGameObject.name = "DebugConsole";
					
					if( newGameObject.GetComponent<Console>() is Console console)
					{
						Font font = Resources.GetBuiltinResource<Font>( "Arial.ttf");
						console.logTextSettings.font = font;
						console.inputFieldFont = font;
					}
				}
			}
		}
		void Reset()
		{
			contentLayout = GetComponentInChildren<RecycleLayoutGroup>();
			inputField = GetComponentInChildren<InputField>();
		}
	#endif
		[SerializeField]
		RecycleLayoutGroup contentLayout = default;
		[SerializeField]
		InputField inputField = default;
		[SerializeField]
		Font inputFieldFont = default;
		[SerializeField]
		LogTextSettings logTextSettings = default;
		
		List<Log> logs = new List<Log>();
		Dictionary<string, System.Func<string, string[], string>> commands = 
			new Dictionary<string, System.Func<string, string[], string>>();
	}
}
