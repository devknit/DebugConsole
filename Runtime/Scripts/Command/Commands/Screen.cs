
namespace DebugConsole.Command
{
	public sealed class Screen : Base
	{
		public Screen() : base( "スクリーン情報を表示します")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( "Screen = {");
			builder.AppendFormat( "  autorotateToLandscapeLeft = {0},\n", UnityEngine.Screen.autorotateToLandscapeLeft);
			builder.AppendFormat( "  autorotateToLandscapeRight = {0},\n", UnityEngine.Screen.autorotateToLandscapeRight);
			builder.AppendFormat( "  autorotateToPortrait = {0},\n", UnityEngine.Screen.autorotateToPortrait);
			builder.AppendFormat( "  autorotateToPortraitUpsideDown = {0},\n", UnityEngine.Screen.autorotateToPortraitUpsideDown);
			builder.AppendFormat( "  currentResolution = {0},\n", UnityEngine.Screen.currentResolution);
			builder.AppendFormat( "  dpi = {0},\n", UnityEngine.Screen.dpi);
			builder.AppendFormat( "  fullScreen = {0},\n", UnityEngine.Screen.fullScreen);
			builder.AppendFormat( "  fullScreenMode = {0},\n", UnityEngine.Screen.fullScreenMode);
			builder.AppendFormat( "  height = {0},\n", UnityEngine.Screen.height);
			builder.AppendFormat( "  orientation = {0},\n", UnityEngine.Screen.orientation);
			builder.AppendFormat( "  resolutions = {0},\n", UnityEngine.Screen.resolutions);
			builder.AppendFormat( "  safeArea = {0},\n", UnityEngine.Screen.safeArea);
			builder.AppendFormat( "  sleepTimeout = {0},\n", UnityEngine.Screen.sleepTimeout);
			builder.AppendFormat( "  width = {0},\n", UnityEngine.Screen.width);
			builder.Append( "}");
			context.Output( builder.ToString());
			return true;
		}
	}
}
