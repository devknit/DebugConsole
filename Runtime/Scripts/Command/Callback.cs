
using System;
using UnityEngine;

namespace DebugConsole.Command
{
	public sealed class Callback : Base
	{
		public Func<Context, bool> Method
		{
			get;
			private set;
		}
		public Callback( string description, Func<Context, bool> method) : base( description)
		{
			Method = method;
		}
		protected override bool OnInvoke( Context context)
		{
			if( Method != null)
			{
				return Method( context);
			}
			return false;
		}
	}
}
