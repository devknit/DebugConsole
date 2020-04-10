
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace DebugConsole
{
	public sealed partial class Console : MonoBehaviour
	{
		public Vector2 ResizeLogWindow( Vector2 windowSize)
		{
			if( windowSize.x <= 0.0f)
			{
				windowSize.x = windowTransform.sizeDelta.x;
			}
			if( windowSize.y <= 0.0f)
			{
				windowSize.y = windowTransform.sizeDelta.y;
			}
			var minimum = new Vector2( 128, 64);
			var maximum = new Vector2( 4096, 4096);
			
			if( canvas.transform is RectTransform canvasTransform)
			{
				maximum = new Vector2( 
					canvasTransform.sizeDelta.x - (windowTransform.offsetMin.x * 2.0f),
					canvasTransform.sizeDelta.y - (windowTransform.offsetMin.y * 2.0f));
			}
			if( windowSize.x < minimum.x)
			{
				windowSize.x = minimum.x;
			}
			if( windowSize.y < minimum.y)
			{
				windowSize.y = minimum.y;
			}
			if( minimum.x < maximum.x && windowSize.x > maximum.x)
			{
				windowSize.x = maximum.x;
			}
			if( minimum.y < maximum.y && windowSize.y > maximum.y)
			{
				windowSize.y = maximum.y;
			}
			windowSize.x = Mathf.Round( windowSize.x);
			windowSize.y = Mathf.Round( windowSize.y);
			windowSize.x -= windowSize.x % 2.0f;
			windowSize.y -= windowSize.y % 2.0f;
			windowTransform.sizeDelta = windowSize;
			return windowSize;
		}
		public void OnBeginDrag( BaseEventData eventData)
		{
			if( eventData is PointerEventData e)
			{
				if( windowTransform != null)
				{
					TryScreenPointToLocalPoint( e.pressEventCamera, e.pressPosition, out pressPosition);
					beginWidth = windowTransform.rect.width;
					beginHeight = windowTransform.rect.height;
				}
			}
		}
		public void OnDrag( BaseEventData eventData)
		{
			if( eventData is PointerEventData e)
			{
				if( windowTransform != null)
				{
					Vector2 position = Vector2.zero;
					
					if( TryScreenPointToLocalPoint( e.pressEventCamera, e.position, out position) != false)
					{
						var windowSize = new Vector2(
							beginWidth + (position.x - pressPosition.x),
							beginHeight + (position.y - pressPosition.y));
						ResizeLogWindow( windowSize);
					}
				}
			}
		}
		bool TryScreenPointToLocalPoint( Camera camera, Vector2 screen, out Vector2 local)
		{
			if( canvas == null || windowTransform == null)
			{
				local = Vector2.zero;
				return false;
			}
			else
			{
				switch( canvas.renderMode)
				{
					case RenderMode.ScreenSpaceOverlay:
					{
						local = windowTransform.InverseTransformPoint( screen);
						break;
					}
					case RenderMode.ScreenSpaceCamera:
					case RenderMode.WorldSpace:
					{
						var rectTransform = transform as RectTransform;
						
						if( camera == null || rectTransform == null)
						{
							local = Vector2.zero;
							return false;
						}
						else
						{
							RectTransformUtility.ScreenPointToLocalPointInRectangle( windowTransform, screen, camera, out local);
						}
						break;
					}
					default:
					{
						local = Vector2.zero;
						return false;
					}
				}
			}
			return true;
		}
		
		Vector2 pressPosition;
		float beginWidth;
		float beginHeight;
	}
}
