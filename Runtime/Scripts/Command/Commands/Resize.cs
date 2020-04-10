
namespace DebugConsole.Command
{
	public sealed class Resize : Base
	{
		public Resize() : base( "コンソールをリサイズします")
		{
			AddOption( "ログのフォントサイズを指定", 1, "font", "fnt", "f");
			AddOption( "ログのウィンドウサイズを指定", 2, "window", "win", "w");
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
			if( context.argv.TryGetValue( "window", out values) != false)
			{
				UnityEngine.Vector2 windowSize;
				
				if( float.TryParse( values[ 0], out windowSize.x) != false
				&&	float.TryParse( values[ 1], out windowSize.y) != false)
				{
					windowSize = context.console.ResizeLogWindow( windowSize);
					context.Output( string.Format( $"コンソールのウィンドウサイズを{windowSize}に変更しました"));
					return true;
				}
			}
			return false;
		}
	}
}
