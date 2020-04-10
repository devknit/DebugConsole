
using UnityEngine;
using System.Collections.Generic;

namespace DebugConsole.Command
{
	public sealed class Context
	{
		public Context( Console console, string input, string[] args)
		{
			this.console = console;
			this.input = input;
			this.args = args;
			argv = new Dictionary<string, string[]>();
			output = string.Empty;
		}
		public void Output( string message)
		{
			if( string.IsNullOrEmpty( output) == false)
			{
				output += System.Environment.NewLine;
			}
			output += message;
		}
		internal void CreateOptions( Dictionary<string, Option> options)
		{
			Option option;
			string arg;
			
			for( int i0 = 1; i0 < args.Length; ++i0)
			{
				arg = args[ i0];
				
				if( options.TryGetValue( arg.ToLower(), out option) != false)
				{
					if( argv.ContainsKey( option.name) == false)
					{
						try
						{
							var values = new string[ option.count];
							
							for( int i1 = 0; i1 < values.Length; ++i1)
							{
								values[ i1] = args[ i0 + i1 + 1];
							}
							argv.Add( option.name, values);
						}
						catch
						{
							output += string.Format( $"オプション <{arg}> の引数が不正です");
						}
					}
				}
			}
		}
		public Console console;
		public string input;
		public string[] args;
		public Dictionary<string, string[]> argv;
		public string output{ get; private set; }
	}
}
