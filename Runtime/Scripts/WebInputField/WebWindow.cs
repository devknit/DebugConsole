
using AOT;
using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace WebSupport
{
	public static class WebWindow
	{
		public static bool Focus { get; private set; }
        public static event Action OnFocusEvent = () => {};
        public static event Action OnBlurEvent = () => {};
        
        [MonoPInvokeCallback( typeof( Action))]
        static void OnWindowFocus()
        {
            Focus = true;
            OnFocusEvent();
        }
        [MonoPInvokeCallback( typeof( Action))]
        static void OnWindowBlur()
        {
            Focus = false;
            OnBlurEvent();
        }
        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitializeOnLoadMethod()
        {
			Focus = true;
            WebWindowPlugin_OnFocus( OnWindowFocus);
            WebWindowPlugin_OnBlur( OnWindowBlur);
        }
	#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        static extern void WebWindowPlugin_OnFocus( Action cb);
        [DllImport("__Internal")]
        static extern void WebWindowPlugin_OnBlur( Action cb);
	#else
        static void WebWindowPlugin_OnFocus( Action cb){}
        static void WebWindowPlugin_OnBlur( Action cb){}
	#endif
	}
}
