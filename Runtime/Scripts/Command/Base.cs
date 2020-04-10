
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace DebugConsole.Command
{
	public abstract class Base
	{
		public Base( string description)
		{
			this.description = description;
			
			AddOption( "コマンドの説明を表示します", 0, "help", "h");
		}
		public virtual bool IsValid()
		{
			return string.IsNullOrEmpty( description) == false;
		}
		public string GetDescription( bool details)
		{
			var builder = new System.Text.StringBuilder();
			
			builder.AppendLine( description);
			
			if( details != false)
			{
				foreach( var option in preOptions)
				{
					builder.AppendFormat( "<{0}>{2}{1}{2}", 
						option.Key, option.Value, System.Environment.NewLine);
				}
			}
			return builder.ToString();
		}
		public bool Invoke( Context context)
		{
			bool ret = false;
			
			context.CreateOptions( options);
			
			if( context.argv.ContainsKey( "help") == false)
			{
				ret = OnInvoke( context);
			}	
			if( ret == false)
			{
				context.Output( string.Format( 
					"コマンドの処理に失敗しました{0}",
					System.Environment.NewLine));
				context.Output( GetDescription( true));
			}
			return ret;
		}
		public void AddOption( string description, int count, params string[] names)
		{
			if( names?.Length > 0
			&&	string.IsNullOrEmpty( description) == false)
			{
				if( count < 0)
				{
					count = 0;
				}
				string preName = string.Join( ", ", names);
				
				if( preOptions.ContainsKey( preName) == false)
				{
					string name = names[ 0];
					string key;
					
					for( int i0 = 0; i0 < names.Length; ++i0)
					{
						key = names[ i0];
						
						if( string.IsNullOrEmpty( key) == false)
						{
							if( key[ 0] != '-')
							{
								key = "-" + key;
							}
							if( options.ContainsKey( key) == false)
							{
								options.Add( key, new Option( key, name, count, description));
							}
						}
					}
					preOptions.Add( preName, description);
				}
			}
		}
		protected abstract bool OnInvoke( Context context);
		
		string description;
		Dictionary<string, Option> options = new Dictionary<string, Option>();
		Dictionary<string, string> preOptions = new Dictionary<string, string>();
	}
}
