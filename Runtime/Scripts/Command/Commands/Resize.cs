
namespace DebugConsole.Command
{
	public sealed class Resize : Base
	{
		public Resize() : base( "コンソールをリサイズします")
		{
			AddOption( "ログのフォントサイズを指定", 1, "font", "f");
		}
		protected override bool OnInvoke( Context context)
		{
			string[] values;
			
			if( context.argv.TryGetValue( "font", out values) != false)
			{
				int fontSize;
				
				if( int.TryParse( values[ 0], out fontSize) != false)
				{
					fontSize = context.console.ResizeLogFont( fontSize);
					context.Output( string.Format( $"コンソールのフォントサイズを{fontSize}に変更しました"));
					return true;
				}
			}
			return false;
		}
	}
}
