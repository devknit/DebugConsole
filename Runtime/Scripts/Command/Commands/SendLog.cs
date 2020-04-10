
namespace DebugConsole.Command
{
	public sealed class SendLog : Base
	{
		public SendLog() : base( "ログを送信します")
		{
			AddOption( "メールで送信します", 0, "mail", "ml", "m");
		}
		protected override bool OnInvoke( Context context)
		{
			if( context.argv.ContainsKey( "mail") != false)
			{
				UnityEngine.Application.OpenURL( "mailto:?body=" + context.console.GetLogString( "%0d%0a"));
			}
			return true;
		}
	}
}
