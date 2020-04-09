
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace WebSupport
{
	public sealed partial class WebInputField : MonoBehaviour, System.IComparable<WebInputField>
	{
		void Awake()
		{
			InputField inputField = GetComponent<InputField>();
			if( inputField != null)
			{
				input = new WrappedInputField( inputField);
			}
		#if !UNITY_WEBGL || UNITY_EDITOR
			enabled = false;
		#endif
		}
		void OnEnable()
        {
            TabFocus.Add( this);
        }
        void OnDisable()
        {
            TabFocus.Remove( this);
        }
		void Update()
		{
			if( input == null || input.isFocused == false)
			{
				return;
			}
			if( instances.ContainsKey( id) == false)
			{
				OnSelect();
			}
			else if( WebInputFieldPlugin_IsFocus( id) == false)
			{
				WebInputFieldPlugin_Focus( id);
			}
			
			var start = WebInputFieldPlugin_SelectionStart( id);
            var end = WebInputFieldPlugin_SelectionEnd( id);
            
            if( WebInputFieldPlugin_SelectionDirection( id) == -1)
            {
                input.selectionFocusPosition = start;
                input.selectionAnchorPosition = end;
            }
            else
            {
                input.selectionFocusPosition = end;
                input.selectionAnchorPosition = start;
            }
            input.Rebuild();
		}
		void OnSelect()
		{
			var rect = GetScreenCoordinates( input.RectTransform());

            var x = (int)rect.x;
            var y = (int)(Screen.height - rect.y);
            id = WebInputFieldPlugin_Create( 
            #if UNITY_2019_1_OR_NEWER
	            "unityContainer",
			#else
	            "gameContainer",
			#endif
            	x, y, (int)rect.width, 
            	1, input.fontSize, input.text, 
            	input.lineType != LineType.SingleLine, 
            	input.contentType == ContentType.Password);

            instances[ id] = this;
            WebInputFieldPlugin_EnterSubmit( id, input.lineType != LineType.MultiLineNewline);
            WebInputFieldPlugin_OnFocus( id, OnFocus);
            WebInputFieldPlugin_OnBlur( id, OnBlur);
            WebInputFieldPlugin_OnValueChange( id, OnValueChange);
            WebInputFieldPlugin_OnEditEnd( id, OnEditEnd);
            WebInputFieldPlugin_Tab( id, OnTab);
            /* default value : https://www.w3schools.com/tags/att_input_maxlength.asp */
            WebInputFieldPlugin_MaxLength( id, (input.characterLimit > 0) ? input.characterLimit : 524288);
            WebInputFieldPlugin_Focus( id);

            if( input.OnFocusSelectAll != false)
            {
                WebInputFieldPlugin_SetSelectionRange( id, 0, input.text.Length);
            }
            WebWindow.OnBlurEvent += OnWindowBlur;
		}
		void OnWindowBlur()
        {
            blueBlock = true;
        }
		void DeactivateInputField()
        {
            WebInputFieldPlugin_Delete(id);
            input.DeactivateInputField();
            instances.Remove( id);
            id = -1;
            WebWindow.OnBlurEvent -= OnWindowBlur;
        }
		/**
		 * \brief 入力範囲をスクリーン座標で取得する
		 * \param rectTransform [in] 入力範囲のトランスフォーム
		 * \return スクリーン座標系の範囲が返ります。
		 */
		Rect GetScreenCoordinates( RectTransform rectTransform)
        {
            var worldCorners = new Vector3[ 4];
            rectTransform.GetWorldCorners( worldCorners);

            var canvas = rectTransform.GetComponentInParent<Canvas>();
            if( canvas != null)
            {
				if( canvas.renderMode != RenderMode.ScreenSpaceOverlay && canvas.worldCamera != null)
				{
	                for( int i0 = 0; i0 < worldCorners.Length; ++i0)
	                {
	                    worldCorners[ i0] = canvas.worldCamera.WorldToScreenPoint( worldCorners[ i0]);
	                }
	            }
            }
            var min = new Vector3( float.MaxValue, float.MaxValue);
            var max = new Vector3( float.MinValue, float.MinValue);
            
            for( int i0 = 0; i0 < worldCorners.Length; ++i0)
            {
                min.x = Mathf.Min( min.x, worldCorners[ i0].x);
                min.y = Mathf.Min( min.y, worldCorners[ i0].y);
                max.x = Mathf.Max( max.x, worldCorners[ i0].x);
                max.y = Mathf.Max( max.y, worldCorners[ i0].y);
            }
            return new Rect( min.x, min.y, max.x - min.x, max.y - min.y);
        }
        public int CompareTo( WebInputField other)
        {
            var a = GetScreenCoordinates( input.RectTransform());
            var b = GetScreenCoordinates( other.input.RectTransform());
            var res = b.y.CompareTo( a.y);
            if( res == 0)
            {
				res = a.x.CompareTo( b.x);
			}
            return res;
        }
        static IEnumerator Blue( int id)
        {
            yield return null;
            
            if( instances.ContainsKey( id) == false)
            {
				yield break;
			}

            bool block = instances[ id].blueBlock;
            instances[ id].blueBlock = false;
            if( block != false)
            {
				yield break;
			}
            instances[ id].DeactivateInputField();
        }
		static Dictionary<int, WebInputField> instances = new Dictionary<int, WebInputField>();
		
		bool blueBlock = false;
		IInputField input;
		int id;
	}
}
