
using System;
using UnityEngine;

namespace DebugConsole
{
	public sealed partial class Console : MonoBehaviour
	{
		public bool AddCommand( string name, Command.Base command)
		{
			if( string.IsNullOrEmpty( name) == false)
			{
				name = name.ToLower();
				
				if( command?.IsValid() != false)
				{
					if( commands.ContainsKey( name) == false)
					{
						commands.Add( name, command);
						return true;
					}
				}
			}
			return false;
		}
		public bool AddCommand( string name, string description, Func<Command.Context, bool> callback)
		{
			if( string.IsNullOrEmpty( name) == false
			&&	string.IsNullOrEmpty( description) == false
			&&	callback != null)
			{
				name = name.ToLower();
				
				if( commands.ContainsKey( name) == false)
				{
					commands.Add( name, new Command.Callback( description, callback));
					return true;
				}
			}
			return false;
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
			AddCommand( "clear", 
				"コンソールのログをクリアします",
				(context) =>
				{
					lock( logs)
					{
						logs.Clear();
						contentLayout.Clear();
					}
					return true;
				}
			);
			AddCommand( "help", 
				"コマンドヘルプ情報を表示します",
				(context) =>
				{
					var builder = new System.Text.StringBuilder();
					
					foreach( var command in commands)
					{
						builder.AppendFormat( "{0}{2}{1}{2}",
							command.Key, command.Value.GetDescription( false), System.Environment.NewLine);
					}
					context.Output( builder.ToString());
					return true;
				}
			);
			AddCommand( "systeminfo", new Command.SystemInfo());
			AddCommand( "application", new Command.Application());
			AddCommand( "paths", new Command.Paths());
			AddCommand( "memorystatus", new Command.MemoryStatus());
			AddCommand( "clipboard", new Command.Clipboard());
			AddCommand( "gc", new Command.GarbageCollection());
			AddCommand( "ip", new Command.InternetProtocol());
			AddCommand( "screen", new Command.Screen());
			AddCommand( "audio", new Command.Audio());
			AddCommand( "sendlog", new Command.SendLog());
			AddCommand( "resize", new Command.Resize());
		}
		string OnCommandResize( string input, string[] args)
		{
			if( args.Length > 1)
			{
				switch( args[ 1])
				{
					case "-w":
					case "-win":
					case "-window":
					{
						//windowTransform.sizeDelta = new Vector2( 640, 480);
						break;
					}
					case "-f":
					case "-fnt":
					case "-font":
					{
						int fontSize = logTextSettings.fontSize;
						
						if( args.Length > 2)
						{
							int size;
							
							if( int.TryParse( args[ 2], out size) != false)
							{
								fontSize = size;
							}
						}
						else
						{
							fontSize = 24;
						}
						ResizeLogFont( fontSize);
						
						break;
					}
				}
			}
			return string.Empty;
		}
		const char kCommandPrefix = ':';
	}
}
