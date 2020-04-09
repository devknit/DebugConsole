
using UnityEngine;
using UnityEngine.UI;
using AOT;

namespace WebSupport
{
	public sealed partial class WebInputField : MonoBehaviour, System.IComparable<WebInputField>
	{
		[MonoPInvokeCallback( typeof( System.Action<int>))]
        static void OnFocus( int id)
        {
		#if UNITY_WEBGL && !UNITY_EDITOR
            UnityEngine.WebGLInput.captureAllKeyboardInput = false;
		#endif
        }
        [MonoPInvokeCallback( typeof( System.Action<int>))]
        static void OnBlur(int id)
        {
		#if UNITY_WEBGL && !UNITY_EDITOR
            UnityEngine.WebGLInput.captureAllKeyboardInput = true;
		#endif
            instances[ id].StartCoroutine( Blue( id));
        }
		[MonoPInvokeCallback( typeof( System.Action<int, string>))]
        static void OnValueChange( int id, string value)
        {
			WebInputField instance;
			
			if( instances.TryGetValue( id, out instance) != false)
			{
				var index = instance.input.caretPosition;
	            if( instance.input.ReadOnly == false)
	            {
	                instance.input.text = value;
	            }
	            /* InputField.ContentType.Name が Name の場合、先頭文字が強制的大文字になるため小文字にして比べる */
	            if( instance.input.contentType == ContentType.Name)
	            {
	                if( string.Compare( instance.input.text, value, true) == 0)
	                {
	                    value = instance.input.text;
	                }
	            }
	            /* InputField の ContentType による整形したテキストを HTML の input に再設定します */
	            if( value != instance.input.text)
	            {
	                WebInputFieldPlugin_Text( id, instance.input.text);
	                WebInputFieldPlugin_SetSelectionRange( id, index, index);
	            }
			}
        }
        [MonoPInvokeCallback( typeof( System.Action<int, string>))]
        static void OnEditEnd( int id, string value)
        {
            if( instances[ id].input.ReadOnly == false)
            {
                instances[ id].input.text = value;
            }
        }
        [MonoPInvokeCallback( typeof( System.Action<int, int>))]
        static void OnTab( int id, int value)
        {
            TabFocus.OnTab( instances[ id], value);
        }
	}
}
