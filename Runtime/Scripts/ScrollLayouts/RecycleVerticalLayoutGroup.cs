
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DebugConsole
{
	public sealed class RecycleVerticalLayoutGroup : RecycleLayoutGroup
	{
		/**
		 * \brief 項目数を変更する
		 * \param count [in] 項目数
		 */
		public override void ChangeItemCount( int count)
		{
			if( count < ItemCount)
			{
				int removeCount = ItemCount - count;
				sizes.RemoveRange( count, removeCount);
				positions.RemoveRange( count, removeCount);
			}
			else if( count > ItemCount)
			{
				var values = new float[ count - ItemCount];
				sizes.AddRange( values);
				positions.AddRange( values);
			}
			base.ChangeItemCount( count);
		}
		/**
		 * \brief すべての要素を無効化して項目数を 0 にする。
		 */
		public override void Clear()
		{
			positions.Clear();
			sizes.Clear();
			base.Clear();
		}
		/**
		 * \brief Content のサイズを求めます。
		 * \return Conetnt のサイズが返ります。
		 */
		protected override Vector2 OnCalculateContentSize()
		{
			var ret = Vector2.zero;
			
			if( OnElementSize != null)
			{
				float size;
				
				for( int i0 = 0; i0 < ItemCount; ++i0)
				{
					size = OnElementSize( i0).y;
					
					if( i0 > 0)
					{
						ret.y += ElementSpace.y;
					}
					positions[ i0] = ret.y;
					sizes[ i0] = size;
					ret.y += size;
				}
				UpdateEnableElements();
			}
			return ret;
		}
		/**
		 * \brief スクロール情報が変化した時に呼び出されます。
		 * \param scrollPosition スクロール位置
		 */
		protected override void OnMoveScrollEvent( Vector2 scrollPosition)
		{
			float p = contentTransform.localPosition.y;// * -1;
			float q = p + viewportTransform.rect.height;
			ToDisableVerticalOutsideElements( p, q);
			ToEnableVerticalInsideElements( p, q);
		}
		/**
		 * \brief 要素のトランスフォーム情報を更新します。
		 * \param index [in] 項目のインデックス
		 * \param elements [in] トランスフォーム情報を更新する要素
		 * \param bEnabled [in] 既に有効な状態だった場合 true が渡されます。
		 */
		protected override void OnUpdateElementTransform( int index, RecycleElement elements, bool bEnabled)
		{
			float x = GetColumnsPositionByIndex( index);
			float y = GetRowsPositionByIndex( index);
			
			var rectTransform = elements.transform as RectTransform;
			rectTransform.localPosition = new Vector3( x, y, 0.0f);
			rectTransform.sizeDelta = new Vector2( viewportSize.x, sizes[ index]);
			
			if( bEnabled == false)
			{
				rectTransform.localScale = Vector3.one;
				elements.Relocation = true;
			}
		}
		/**
		 * \brief 項目のインデックスから x座標を取得します。
		 * \param index [in] 項目のインデックス
		 * \return x座標が返ります。
		 */
		float GetColumnsPositionByIndex( int index)
		{
			return 0.0f;
		}
		/**
		 * \brief 項目のインデックスから y座標を取得します。
		 * \param index [in] 項目のインデックス
		 * \return y座標が返ります。
		 */
		float GetRowsPositionByIndex( int index)
		{
			return positions[ index] * -1;
		}
		void ToDisableVerticalOutsideElements( float viewTop, float viewBottom)
		{
			foreach( RectTransform child in contentTransform)
			{
				if( child.gameObject.activeSelf != false)
				{
					var elements = child.GetComponent<RecycleElement>();
					float elementTop = positions[ elements.Index];
					float elementBottom = elementTop + sizes[ elements.Index] + ElementSpace.y;
					
					if( elementTop > viewBottom || elementBottom < viewTop)
					{
						ToDisableElements( elements);
					}
				}
			}
		}
		void ToEnableVerticalInsideElements( float viewTop, float viewBottom)
		{
			for( int i0 = 0; i0 < ItemCount; i0++)
			{
				float elementTop = positions[ i0];
				float elementBottom = elementTop + sizes[ i0] + ElementSpace.y;
				
				if( elementTop <= viewBottom && elementBottom >= viewTop)
				{
					ToEnableElements( i0);
				}
			}
		}
	#if true
		void LateUpdate()
		{
			bool bResizing = false;
			
			if( DetectViewportResizing() != false)
			{
				bResizing = true;
			}
			if( bResizing != false)
			{
				CalculateContentSize();
				bUpdateTransform = true;
			}
			if( bUpdateLocation != false || bUpdateTransform != false)
			{
				OnMoveScrollEvent( scrollPosition);
				bUpdateLocation = false;
				bUpdateTransform = false;
			}
		}
	#endif
		
		List<float> positions = new List<float>();
		List<float> sizes = new List<float>();
	}
}
