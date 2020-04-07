
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DebugConsole
{
	public sealed class RecycleVerticalLayoutGridGroup : RecycleLayoutGroup
	{
		/**
		 * \brief 1行(列)内の要素数を求める
		 * return 要素数が返ります。
		 */
		protected override int OnCalculateLineCount()
		{
			return (int)((float)viewportTransform.rect.width / ElementAdvance.x);
		}
		/**
		 * \brief Content のサイズを求めます。
		 * \return Conetnt のサイズが返ります。
		 */
		protected override Vector2 OnCalculateContentSize()
		{
			int count = ItemCount / LineElementCount;
			var contentSize = Vector2.zero;
			
			if( ItemCount % LineElementCount > 0)
			{
				count += 1;
			}
			contentSize = new Vector2( 0, ElementAdvance.y);
			contentSize.y *= count;
			return contentSize;
		}
		/**
		 * \brief スクロール情報が変化した時に呼び出されます。
		 * \param scrollPosition スクロール位置
		 */
		protected override void OnMoveScrollEvent( Vector2 scrollPosition)
		{
			float p = rectTransform.localPosition.y * -1;
			float q = p - viewportTransform.rect.height;
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
			rectTransform.localScale = Vector3.one;
			rectTransform.sizeDelta = ElementSize;
		}
		/**
		 * \brief 項目のインデックスから x座標を取得します。
		 * \param index [in] 項目のインデックス
		 * \return x座標が返ります。
		 */
		float GetColumnsPositionByIndex( int index)
		{
			return ElementAdvance.x * (index % LineElementCount);
		}
		/**
		 * \brief 項目のインデックスから y座標を取得します。
		 * \param index [in] 項目のインデックス
		 * \return y座標が返ります。
		 */
		float GetRowsPositionByIndex( int index)
		{
			return ElementAdvance.y * (index / LineElementCount) * -1;
		}
		void ToDisableVerticalOutsideElements( float viewTop, float viewBottom)
		{
			foreach( RectTransform child in rectTransform)
			{
				if( child.gameObject.activeSelf != false)
				{
					float elementTop = child.localPosition.y;
					float elementBottom = elementTop - ElementSize.y - ElementSpace.y;
					
					if( elementTop < viewBottom || elementBottom > viewTop)
					{
						ToDisableElements( child.GetComponent<RecycleElement>());
					}
				}
			}
		}
		void ToEnableVerticalInsideElements( float viewTop, float viewBottom)
		{
			int index = (int)(Mathf.Abs( viewTop) / ElementAdvance.y) * LineElementCount - 1;
			do
			{
				index++;
				
				if( index >= ItemCount || GetRowsPositionByIndex( index) < viewBottom)
				{
					break;
				}
				ToEnableElements( index);
			}
			while( true);
		}
		void LateUpdate()
		{
			if( bInitialized != false)
			{
				if( DetectViewportResizing() != false)
				{
					CalculateLineCount();
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
		}
	}
}
