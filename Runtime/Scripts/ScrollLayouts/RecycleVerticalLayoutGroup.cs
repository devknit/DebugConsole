
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
				int addCount = count - ItemCount;
				
				for( int i0 = 0; i0 < addCount; i0++)
				{
					positions.Add( sizes.Sum() + sizes.Count * ElementSpace.y);
					sizes.Add( ElementSize.y);
				}
				bForceScrollbarButtonUpdateCount = 2;
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
		 * \brief 1行(列)内の要素数を求める
		 * return 要素数が返ります。
		 */
		protected override int OnCalculateLineCount()
		{
			return 1;
		}
		/**
		 * \brief Content のサイズを求めます。
		 * \return Conetnt のサイズが返ります。
		 */
		protected override Vector2 OnCalculateContentSize()
		{
			return new Vector2( 0, 
				sizes.Sum() + (ItemCount - 1) * ElementSpace.y);
		}
		/**
		 * \brief スクロール情報が変化した時に呼び出されます。
		 * \param scrollPosition スクロール位置
		 */
		protected override void OnMoveScrollEvent( Vector2 scrollPosition)
		{
			float p = rectTransform.localPosition.y;// * -1;
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
			rectTransform.sizeDelta = new Vector2( ViewportSize.x, sizes[ index]);
			
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
			foreach( RectTransform child in rectTransform)
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
		void LateUpdate()
		{
			if( bInitialized == false)
			{
				return;
			}
			
			bool bResizing = false;
			bool bRealign = false;
			float offset = -1.0f;
			float size;
			int index = 0;
			int i0;
			
			if( DetectViewportResizing() != false)
			{
				bResizing = true;
			}
			foreach( var elements in enableElements)
			{
				size = (elements.Value.transform as RectTransform).rect.height;
				index = elements.Key;
				
				if( bResizing != false
				||	sizes[ index] != size
				||	elements.Value.Relocation != false)
				{
					elements.Value.Relocation = false;
					if( offset < 0.0f)
					{
						for( i0 = 0, offset = 0.0f; i0 < index; i0++)
						{
							offset += sizes[ i0] + ElementSpace.y;
						}
					}
					sizes[ index] = size;
					bResizing = false;
					bRealign = true;
				}
				if( bRealign != false)
				{
					positions[ index] = offset;
					offset += sizes[ index] + ElementSpace.y;
				}
			}
			if( bRealign != false)
			{
				for( i0 = index + 1; i0 < ItemCount; i0++)
				{
					positions[ i0] = offset;
					sizes[ i0] = ElementSize.y;
					offset += sizes[ i0] + ElementSpace.y;
				}
				CalculateContentSize();
				bUpdateTransform = true;
			}
			if( bUpdateLocation != false || bUpdateTransform != false)
			{
				OnMoveScrollEvent( scrollPosition);
				bUpdateLocation = false;
				bUpdateTransform = false;
				
				if( bForceScrollbarButtonUpdateCount > 0)
				{
					if( defaultScrollbarBottom != false)
					{
						if( scrollRect != null)
						{
							scrollRect.verticalNormalizedPosition = 0.0f;
						}
					}
					bForceScrollbarButtonUpdateCount--;
				}
			}
		}
		
		[SerializeField]
		bool defaultScrollbarBottom = false;
		
		List<float> positions = new List<float>();
		List<float> sizes = new List<float>();
		int bForceScrollbarButtonUpdateCount;
	}
}
