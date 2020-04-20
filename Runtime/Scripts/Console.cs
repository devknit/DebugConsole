#if !UNITY_EDITOR
	#if UNITY_IOS
		#define WITH_IOS
	#endif
	#if UNITY_ANDROID
		#define WITH_ANDROID
	#endif
#endif

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	public sealed partial class Console : MonoBehaviour
	{
		public string GetLogString( string newLine=null)
		{
			var builder = new System.Text.StringBuilder();
			
			if( string.IsNullOrEmpty( newLine) != false)
			{
				newLine = System.Environment.NewLine;
			}
			lock( logs)
			{
				foreach( var log in logs)
				{
					builder.AppendFormat( $"{log.text}{newLine}");
				}
			}
			return builder.ToString();
		}
		public int ResizeLogFont( int fontSize)
		{
			if( fontSize < 8)
			{
				fontSize = 8;
			}
			if( fontSize > 300)
			{
				fontSize = 300;
			}
			if( logTextSettings.fontSize != fontSize)
			{
				logTextSettings.fontSize = fontSize;
				contentLayout.CalculateContentSize();
				contentLayout.Flush();
			}
			return fontSize;
		}
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
			LogSetting logSetting = logTextSettings.GetLogSetting( type);
			
			if( logSetting.stackTrace != false)
			{
				text += System.Environment.NewLine + stackTrace;
			}
			Append( text, logSetting.color);
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
				var color = logTextSettings.normal.color;
				
				Append( text, color);
				OnCommands( text, color);
				
				inputField.text = string.Empty;
			#if !WITH_IOS && !WITH_ANDROID
				inputField.ActivateInputField();
			#endif
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
			canvas = GetComponentInChildren<Canvas>();
//			canvasScaler = GetComponentInChildren<CanvasScaler>();
			
			if( GetComponentInChildren<CanvasRenderer>() is CanvasRenderer canvasRenderer)
			{
				if( canvasRenderer.transform is RectTransform rectTransform)
				{
					windowTransform = rectTransform;
				}
			}
			contentLayout = GetComponentInChildren<RecycleLayoutGroup>();
			inputField = GetComponentInChildren<InputField>();
		}
	#endif
		[SerializeField]
		Canvas canvas = default;
//		[SerializeField]
//		CanvasScaler canvasScaler = default;
		[SerializeField]
		RectTransform windowTransform = default;
		[SerializeField]
		RecycleLayoutGroup contentLayout = default;
		[SerializeField]
		InputField inputField = default;
		[SerializeField]
		Font inputFieldFont = default;
		[SerializeField]
		LogTextSettings logTextSettings = default;
		
		List<Log> logs = new List<Log>();
	}
}
