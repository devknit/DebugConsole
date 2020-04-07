
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DebugConsole
{
	public abstract class RecycleLayoutGroup : UIBehaviour
	{
		/**
		 * \brief 項目数
		 */
		public int ItemCount
		{
			get{ return itemCount; }
		}
		/**
		 * \brief 1行(列)内の要素数
		 */
		public int LineElementCount
		{
			get{ return lineElementCount; }
		}
		/**
		 * \brief 要素のサイズ
		 */
		public Vector2 ElementSize
		{
			get{ return elementSize; }
		}
		/**
		 * \brief 要素同士の間隔
		 */
		public Vector2 ElementSpace
		{
			get{ return elementSpace; }
		}
		/**
		 * \brief 次の要素までの距離
		 */
		public Vector2 ElementAdvance
		{
			get{ return elementAdvance; }
		}
		/**
		 * \brief Viewport のサイズ
		 */
		public Vector2 ViewportSize
		{
			get{ return viewportSize; }
		}
		/**
		 * \brief 垂直方向のスクロール位置を 0 から 1 の間で表したもの。0 は下端を表します。
		 */
		public float VerticalNormalizedPosition
		{
			get{ return scrollRect.verticalNormalizedPosition; }
			set{ scrollRect.verticalNormalizedPosition = value; }
		}
		/**
		 * \brief 要素が無効化された時に呼び出されるコールバック
		 * \param RecycleElement [in] 無効化された要素
		 */
		public Action<RecycleElement> OnDisableElements
		{
			get{ return onDisableElements; }
			set{ onDisableElements = value; }
		}
		/**
		 * \brief 要素が有効化されたときに呼び出されるコールバック
		 * \param RecycleElement [in] 有効化された要素
		 * \param Action [in] 要素の有効化が完了したら呼び出してください。
		 */
		public Action<RecycleElement, Action> OnEnableElements
		{
			get{ return onEnableElements; }
			set{ onEnableElements = value; }
		}
		/**
		 * \brief 項目数を変更する
		 * \param count [in] 項目数
		 */
		public virtual void ChangeItemCount( int count)
		{
			if( itemCount != count)
			{
				itemCount = count;
				
				if( bInitialized != false)
				{
					bUpdateLocation = true;
				}
				CalculateContentSize();
			}
		}
		/**
		 * \brief すべての要素を無効化して項目数を 0 にする。
		 */
		public virtual void Clear()
		{
			RecycleElement[] enables = enableElements.Values.ToArray();
			
			for( int i0 = 0; i0 < enables.Length; i0++)
			{
				ToDisableElements( enables[ i0]);
			}
			itemCount = 0;
			CalculateContentSize();
			
			scrollPosition = new Vector2( 1.0f, 0.0f);
			OnScrollRectValueChanged( scrollPosition);
		}
		/**
		 * \brief すべての要素を更新する。
		 */
		public void Flush()
		{
			RecycleElement[] enables = enableElements.Values.ToArray();
			
			for( int i0 = 0; i0 < enables.Length; i0++)
			{
				ToDisableElements( enables[ i0]);
			}
			if( bInitialized != false && itemCount > 0)
			{
				OnScrollRectValueChanged( scrollPosition);
			}
		}
		
		/* please inherit and implement methods ---------------------------- */
		/**
		 * \brief 1行(列)内の要素数を求める
		 * return 要素数が返ります。
		 */
		protected abstract int OnCalculateLineCount();
		/**
		 * \brief Content のサイズを求めます。
		 * \return Conetnt のサイズが返ります。
		 */
		protected abstract Vector2 OnCalculateContentSize();
		/**
		 * \brief Content のサイズが変更された時に呼び出されます。
		 */
		protected virtual void OnContentResize(){}
		/**
		 * \brief スクロール情報が変化した時に呼び出されます。
		 * \param scrollPosition スクロール位置
		 */
		protected abstract void OnMoveScrollEvent( Vector2 scrollPosition);
		/**
		 * \brief 要素のトランスフォーム情報を更新します。
		 * \param index [in] 項目のインデックス
		 * \param elements [in] トランスフォーム情報を更新する要素
		 * \param bEnabled [in] 既に有効な状態だった場合 true が渡されます。
		 */
		protected abstract void OnUpdateElementTransform( int index, RecycleElement elements, bool bEnabled);
		
		/* protected methods ----------------------------------------------- */
		/**
		 * \brief 1行(列)内の要素数を求める
		 */
		protected void CalculateLineCount()
		{
			lineElementCount = OnCalculateLineCount();
			
			if( lineElementCount < 1)
			{
				lineElementCount = 1;
			}
		}
		/**
		 * \brief Content のサイズを求めます。
		 */
		protected void CalculateContentSize()
		{
			rectTransform.sizeDelta = OnCalculateContentSize();
		}
		/**
		 * \brief Viewport のサイズ変更を検知する。
		 */
		protected bool DetectViewportResizing()
		{
			float width = viewportTransform.rect.width;
			float height = viewportTransform.rect.height;
			
			if( viewportSize.x != width
			||	viewportSize.y != height)
			{
				viewportSize.x = width;
				viewportSize.y = height;
				return true;
			}
			return false;
		}
		/**
		 * \brief 要素を無効にする。
		 * \param elements [in] 無効化する要素
		 */
		protected void ToDisableElements( RecycleElement elements)
		{
			enableElements.Remove( elements.Index);
			elements.gameObject.SetActive( false);
			elements.transform.SetParent( recycleTransform);
			elements.transform.localPosition = Vector3.zero;
			elements.Enabled = false;
			elements.Recycle = true;
			disableElements.Add( elements);
			
			if( onDisableElements != null)
			{
				onDisableElements( elements);
			}
		}
		/**
		 * \brief 要素を有効にする。
		 * \param index [in] 有効化する項目のインデックス
		 * \return 有効化した要素が返ります。
		 */
		protected RecycleElement ToEnableElements( int index)
		{
			RecycleElement elements = null;
			
			if( index < itemCount)
			{
				if( enableElements.TryGetValue( index, out elements) != false)
				{
					if( bUpdateTransform != false)
					{
						OnUpdateElementTransform( index, elements, true);
					}
				}
				else
				{
					/* 未使用要素からリサイクル可能なモノを検索 */
					foreach( RecycleElement e in disableElements)
					{
						if( e.Recycle != false)
						{
							elements = e;
							break;
						}
					}
					/* リサイクル可能な要素が見つかった場合は再使用する */
					if( elements != null)
					{
						elements.transform.SetParent( transform);
						elements.gameObject.SetActive( true);
						disableElements.Remove( elements);
					}
					/* リサイクル可能な要素が見つからなかった場合は新規作成する */
					else
					{
						var newObject = Instantiate( elementPrefab);
						elements = newObject.GetComponent<RecycleElement>();
						elements.transform.SetParent( transform);
						if( elements.transform is RectTransform rc)
						{
							rc.pivot = new Vector2( 0.0f, 1.0f);
						}
					}
					
				#if UNITY_EDITOR
					elements.name = index.ToString();
				#endif
					elements.Index = index;
					elements.Recycle = false;
					
					OnUpdateElementTransform( index, elements, false);
					enableElements.Add( index, elements);
					
					if( onEnableElements == null)
					{
						elements.Enabled = true;
					}
					else
					{
						onEnableElements( elements, () =>
						{
							elements.Enabled = true;
						});
					}
				}
			}
			return elements;
		}
		protected override void Awake()
		{
			base.Awake();
			rectTransform = transform as RectTransform;
			viewportTransform = transform.parent as RectTransform;
		}
		protected override void Start()
		{
			base.Start();
			StartCoroutine( Initialize());
		}
		protected override void OnEnable()
		{
			base.OnEnable();
			
			if( elementAutoClean != false)
			{
				StartCleaningCoroutine();
			}
		}
		protected override void OnDisable()
		{
			base.OnDisable();
			
			if( elementAutoClean != false)
			{
				StopCleaningCoroutine();
			}
		}
		void StartCleaningCoroutine()
		{
			if( cleaningCoroutine != null)
			{
				StopCoroutine( cleaningCoroutine);
			}
			cleaningCoroutine = StartCoroutine
			( 
				ClockSignal( autoCleanIntervalSeconds, () =>
				{
					OnCleaning( 0.1f);
				}
			));
		}
		void StopCleaningCoroutine()
		{
			if( cleaningCoroutine != null)
			{
				StopCoroutine( cleaningCoroutine);
			}
			cleaningCoroutine = null;
		}
		IEnumerator ClockSignal( float interval, Action callback)
		{
			do
			{
				yield return new WaitForSeconds( interval);
				
				if( callback != null)
				{
					callback();
				}
			}
			while( true);
		}
		void OnCleaning( float elapsedSeconds)
		{
			if( autoCleanKeepCount * lineElementCount < disableElements.Count)
			{
				float limitTime = Time.realtimeSinceStartup + (elapsedSeconds / 60.0f);
				RecycleElement elements;
				int i0;
				
				for( i0 = disableElements.Count - 1; i0 >= 0; i0--)
				{
					if( autoCleanKeepCount * lineElementCount >= disableElements.Count)
					{
						break;
					}
					if( limitTime <= Time.realtimeSinceStartup)
					{
						break;
					}
					elements = disableElements[ i0];
					
					if( elements.Recycle != false)
					{
						disableElements.Remove( elements);
						GameObject.Destroy( elements.gameObject);
					}
				}
			}
		}
		IEnumerator Initialize()
		{
			bInitialized = false;
			
			/* 未使用項目の退避場所を作成する */
			if( recycleTransform == null)
			{
				var newObject = new GameObject( "RecycleContent");
				recycleTransform = newObject.transform;
				recycleTransform.SetParent( viewportTransform);
				recycleTransform.localPosition = Vector3.zero;
				recycleTransform.localScale = Vector3.one;
			}
			
			/* ScrollRect の onValueChanged リスナーを登録する */
			scrollRect = rectTransform.GetComponentInParent<ScrollRect>();
			if( scrollRect != null)
			{
				scrollRect.onValueChanged.AddListener( OnScrollRectValueChanged);
			}
			
			/* Content 内の項目をクリアする */
			foreach( Transform item in transform)
			{
				Destroy( item);
			}
			foreach( Transform item in recycleTransform)
			{
				Destroy( item);
			}
			
			/* 項目を管理しているコンテナをクリアする */
			disableElements = new List<RecycleElement>();
			enableElements = new SortedDictionary<int, RecycleElement>();
			
			/* レイアウト計算のため以降は次のフレームへ */
			yield return null;
			
			/* 次の要素までの距離を求める */
			elementAdvance = new Vector2(
				elementSize.x + elementSpace.x,
				elementSize.y + elementSpace.y);
			
			/* 1行(列)内の要素数を求める */
			CalculateLineCount();
			
			/* Content のサイズを求める */
			CalculateContentSize();

			bInitialized = true;
			
			/* レイアウト計算のため以降は次のフレームへ */
			yield return null;
			
			scrollPosition = new Vector2( 1.0f, 0.0f);
			
			if( bInitialized != false)
			{
				bUpdateLocation = true;
			}
			CalculateContentSize();
		}
		/**
		 * \brief スクロール座標の変更が発生した時に呼び出されます。
		 * \param position [in] スクロール座標
		 */
		void OnScrollRectValueChanged( Vector2 position)
		{
			if( bInitialized != false)
			{
				bUpdateLocation = true;
			}
			if( scrollPosition != position)
			{
				scrollPosition = position;
			}
		}
		
		/*!> 要素プレハブ */
		[SerializeField]
		GameObject elementPrefab = default;
		/*!> 要素サイズ */
		[SerializeField]
		Vector2 elementSize = new Vector2( 32, 32);
		/*!> 要素同士の間隔 */
		[SerializeField]
		Vector2 elementSpace = Vector2.zero;
		/*!> 未使用要素を定期的に破棄する */
		[SerializeField]
		bool elementAutoClean = false;
		/*!> 最低限残す未使用要素数 */
		[SerializeField]
		int autoCleanKeepCount = 1;
		/*!> 未使用要素の定期破棄の秒間 */
		[SerializeField, Range( 1.0f, 60.0f)]
		float autoCleanIntervalSeconds = 1.0f;
		
		/*!> 初期化済みフラグ */
		protected bool bInitialized;
		/*!> 未使用状態の項目を退避させる親 */
		Transform recycleTransform;
		/*!> ContentのRectTransformキャッシュ */
		protected RectTransform rectTransform;
		/*!> ViewportのRectTransformキャッシュ */
		protected RectTransform viewportTransform;
		/*!> ScrollRectキャッシュ */
		protected ScrollRect scrollRect;
		/*!> 未使用状態の要素群 */
		List<RecycleElement> disableElements;
		/*!> 使用状態の要素群 */
		protected SortedDictionary<int, RecycleElement> enableElements;
		/*!> 定期敵に未使用要素の破棄を行うコルーチン */
		Coroutine cleaningCoroutine;
		/*!> 1行(列)内の要素数 */
		int lineElementCount = 1;
		/*!> 次の行(列)までの距離 */
		Vector2 elementAdvance;
		/*!> スクロールオフセット */
		protected Vector2 scrollPosition = new Vector2( 1.0f, 0.0f);
		/*!> Viewportのサイズキャッシュ */
		Vector2 viewportSize = Vector2.zero;
		/*!> ToEnableElements() 呼び出し時に\n
		 * 既に有効済みの要素の Transform も更新する
		 */
		protected bool bUpdateTransform;
		/*!> スクロールなどにより要素の再配置が\n
		 * 必要になる可能性がある場合 true になる
		 */
		protected bool bUpdateLocation;
		/*!> 項目数 */
		int itemCount;
		
		/*!> 要素が無効になった時のコールバック */
		Action<RecycleElement> onDisableElements;
		/*!> 要素が有効になった時のコールバック */
		Action<RecycleElement, Action> onEnableElements;
	}
}
