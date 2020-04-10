
namespace DebugConsole.Command
{
	public sealed class Clipboard : Base
	{
		public Clipboard() : base( "ログをクリップボードに貼り付けます")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			UnityEngine.GUIUtility.systemCopyBuffer = context.console.GetLogString();
			context.Output( "ログをクリップボードに貼り付けました");
			return true;
		}
	}
}
