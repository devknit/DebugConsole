using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugConsole
{
	public class RecycleElement : MonoBehaviour
	{
		public int Index
		{
			get;
			set;
		}
		public bool Enabled
		{
			get;
			set;
		}
		public bool Recycle
		{
			get;
			set;
		}
		public bool Relocation
		{
			get;
			set;
		}
	}
}
