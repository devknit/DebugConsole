
using UnityEngine;
using System.Runtime.InteropServices;

namespace WebSupport
{
	public sealed partial class WebInputField : MonoBehaviour, System.IComparable<WebInputField>
	{
	#if UNITY_WEBGL && !UNITY_EDITOR
		[DllImport("__Internal")]
        public static extern int WebInputFieldPlugin_Create( string canvasId, int x, int y, int width, int height, int fontsize, string text, bool isMultiLine, bool isPassword);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_EnterSubmit( int id, bool flag);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_Tab( int id, System.Action<int, int> cb);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_Focus( int id);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_OnFocus( int id, System.Action<int> cb);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_OnBlur( int id, System.Action<int> cb);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_OnValueChange( int id, System.Action<int, string> cb);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_OnEditEnd( int id, System.Action<int, string> cb);
        [DllImport("__Internal")]
        public static extern int WebInputFieldPlugin_SelectionStart( int id);
        [DllImport("__Internal")]
        public static extern int WebInputFieldPlugin_SelectionEnd( int id);
        [DllImport("__Internal")]
        public static extern int WebInputFieldPlugin_SelectionDirection( int id);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_SetSelectionRange( int id, int start, int end);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_MaxLength( int id, int maxlength);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_Text( int id, string text);
        [DllImport("__Internal")]
        public static extern bool WebInputFieldPlugin_IsFocus( int id);
        [DllImport("__Internal")]
        public static extern void WebInputFieldPlugin_Delete( int id);
	#else
		public static int WebInputFieldPlugin_Create( string canvasId, int x, int y, int width, int height, int fontsize, string text, bool isMultiLine, bool isPassword){ return 0; }
        public static void WebInputFieldPlugin_EnterSubmit( int id, bool flag){}
        public static void WebInputFieldPlugin_Tab( int id, System.Action<int, int> cb){}
        public static void WebInputFieldPlugin_Focus( int id){}
        public static void WebInputFieldPlugin_OnFocus( int id, System.Action<int> cb){}
        public static void WebInputFieldPlugin_OnBlur( int id, System.Action<int> cb){}
        public static void WebInputFieldPlugin_OnValueChange( int id, System.Action<int, string> cb){}
        public static void WebInputFieldPlugin_OnEditEnd( int id, System.Action<int, string> cb){}
        public static int WebInputFieldPlugin_SelectionStart( int id){ return 0; }
        public static int WebInputFieldPlugin_SelectionEnd( int id){ return 0; }
        public static int WebInputFieldPlugin_SelectionDirection( int id){ return 0; }
        public static void WebInputFieldPlugin_SetSelectionRange( int id, int start, int end){}
        public static void WebInputFieldPlugin_MaxLength( int id, int maxlength){}
        public static void WebInputFieldPlugin_Text( int id, string text){}
        public static bool WebInputFieldPlugin_IsFocus( int id){ return false; }
        public static void WebInputFieldPlugin_Delete( int id){}
	#endif
	}
}
