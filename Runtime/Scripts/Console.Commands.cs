
using UnityEngine;
using System.Collections.Generic;

namespace DebugConsole
{
	public sealed partial class Console : MonoBehaviour
	{
		public void AddCommand( Command.Base command, params string[] names)
		{
			if( names?.Length > 0 && command?.IsValid() != false)
			{
				string preName = string.Join( ", ", names);
				string name;
				
				if( preCommands.ContainsKey( preName) == false)
				{
					for( int i0 = 0; i0 < names.Length; ++i0)
					{
						name = names[ i0];
						
						if( string.IsNullOrEmpty( name) == false)
						{
							name = name.ToLower();
							
							if( commands.ContainsKey( name) == false)
							{
								commands.Add( name, command);
							}
						}
					}
					preCommands.Add( preName, command.GetDescription( false));
				}
			}
		}
		public void AddCommand( string description, System.Action<Command.Base> constructor, System.Func<Command.Context, bool> callback, params string[] names)
		{
			var command = new Command.Callback( description, callback);
			constructor?.Invoke( command);
			AddCommand( command, names);
		}
		public bool RemoveCommand( string name)
		{
			if( string.IsNullOrEmpty( name) == false)
			{
				if( name[ 0] != kCommandPrefix)
				{
					name = kCommandPrefix + name;
				}
				if( commands.ContainsKey( name) == false)
				{
					commands.Remove( name);
					return true;
				}
			}
			return false;
		}
		void OnEntryDefaultCommands()
		{
			AddCommand(  
				"コンソールのログをクリアします",
				null,
				(context) =>
				{
					lock( logs)
					{
						logs.Clear();
						contentLayout.Clear();
					}
					return true;
				},
				"clear"
			);
			AddCommand(  
				"コマンドヘルプ情報を表示します",
				null,
				(context) =>
				{
					var builder = new System.Text.StringBuilder();
					
					foreach( var command in preCommands)
					{
						builder.AppendFormat( "[{0}]{2}{1}",
							command.Key, command.Value, System.Environment.NewLine);
					}
					context.Output( builder.ToString());
					return true;
				},
				"help"
			);
			AddCommand( new Command.SystemInfo(), "systeminfo", "system", "sys");
			AddCommand( new Command.Application(), "application", "app");
			AddCommand( new Command.FrameRate(), "framerate", "fps");
			AddCommand( new Command.Paths(), "paths");
			AddCommand( new Command.MemoryStatus(), "memorystatus", "memory", "mem");
			AddCommand( new Command.Clipboard(), "clipboard", "cb");
			AddCommand( new Command.GarbageCollection(), "gc");
			AddCommand( new Command.InternetProtocol(), "ip");
			AddCommand( new Command.Screen(), "screen");
			AddCommand( new Command.Audio(), "audio");
			AddCommand( new Command.SendLog(), "sendlog", "send");
			AddCommand( new Command.Resize(), "resize");
		}
		void OnCommands( string text, Color color)
		{
			if( text[ 0] == kCommandPrefix)
			{
				Command.Base command;
				
				string[] args = text.Trim().Remove( 0, 1).Split( ' ');
				
				if( commands.TryGetValue( args[ 0], out command) != false)
				{
					var context = new Command.Context( this, text, args);
					try
					{
						command.Invoke( context);
					}
					catch( System.Exception e)
					{
						context.Output( e.ToString());
					}
					Append( context.output, color);
				}
				else
				{
					Append( "コマンドが見つかりません", color);
				}
			}
		}
		const char kCommandPrefix = ':';
		Dictionary<string, Command.Base> commands = new Dictionary<string, Command.Base>();
		Dictionary<string, string> preCommands = new Dictionary<string, string>();
	}
}
