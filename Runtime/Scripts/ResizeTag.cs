
using UnityEngine;
using UnityEngine.EventSystems;

namespace DebugConsole
{
	public sealed class ResizeTag : MonoBehaviour, IDragHandler, IBeginDragHandler
	{
		public void OnBeginDrag( PointerEventData e)
		{
			if( window != null)
			{
				TryScreenPointToLocalPoint( e.pressEventCamera, e.pressPosition, out pressPosition);
				beginWidth = window.rect.width;
				beginHeight = window.rect.height;
			}
		}
		public void OnDrag( PointerEventData e)
		{
			if( window != null)
			{
				Vector2 position = Vector2.zero;
				
				if( TryScreenPointToLocalPoint( e.pressEventCamera, e.position, out position) != false)
				{
					float width = beginWidth + (position.x - pressPosition.x);
					float height = beginHeight + (position.y - pressPosition.y);
					
					if( width < minimum.x)
					{
						width = minimum.x;
					}
					if( height < minimum.y)
					{
						height = minimum.y;
					}
					if( minimum.x < maximum.x)
					{
						if( width > maximum.x)
						{
							width = maximum.x;
						}
					}
					if( minimum.y < maximum.y)
					{
						if( height > maximum.y)
						{
							height = maximum.y;
						}
					}
					window.sizeDelta = new Vector2( width, height);
				}
			}
		}
		bool TryScreenPointToLocalPoint( Camera camera, Vector2 screen, out Vector2 local)
		{
			if( canvas == null || window == null)
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
						local = window.InverseTransformPoint( screen);
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
							RectTransformUtility.ScreenPointToLocalPointInRectangle( window, screen, camera, out local);
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
		
		[SerializeField]
		Canvas canvas = default;
		[SerializeField]
		RectTransform window = default;
		[SerializeField]
		Vector2 minimum = Vector2.zero;
		[SerializeField]
		Vector2 maximum = Vector2.zero;
		
		Vector2 pressPosition;
		float beginWidth;
		float beginHeight;
	}
}
