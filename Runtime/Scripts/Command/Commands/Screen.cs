
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
			DisplayToBuilder( builder);
			ScreenToBuilder( builder);
			context.Output( builder.ToString());
			return true;
		}
		static void DisplayToBuilder( System.Text.StringBuilder builder)
		{
			builder.Append( "Display = {\n");
			builder.Append( "  main = {\n");
			builder.AppendFormat( "    active = {0},\n", UnityEngine.Display.main.active);
			builder.AppendFormat( "    systemWidth = {0},\n", UnityEngine.Display.main.systemWidth);
			builder.AppendFormat( "    systemHeight = {0},\n", UnityEngine.Display.main.systemHeight);
			builder.AppendFormat( "    renderingWidth = {0},\n", UnityEngine.Display.main.renderingWidth);
			builder.AppendFormat( "    renderingHeight = {0},\n", UnityEngine.Display.main.renderingHeight);
			builder.Append( "  },\n");
			if( UnityEngine.Display.displays.Length > 0)
			{
				builder.Append( "  displays = [\n");
				
				for( int i0 = 0; i0 < UnityEngine.Display.displays.Length; ++i0)
				{
					builder.Append( "    {\n");
					builder.AppendFormat( "      active = {0},\n", UnityEngine.Display.displays[ i0].active);
					builder.AppendFormat( "      systemWidth = {0},\n", UnityEngine.Display.displays[ i0].systemWidth);
					builder.AppendFormat( "      systemHeight = {0},\n", UnityEngine.Display.displays[ i0].systemHeight);
					builder.AppendFormat( "      renderingWidth = {0},\n", UnityEngine.Display.displays[ i0].renderingWidth);
					builder.AppendFormat( "      renderingHeight = {0},\n", UnityEngine.Display.displays[ i0].renderingHeight);
					builder.Append( "    }\n");
				}
				builder.Append( "  }\n");
			}
			else
			{
				builder.Append( "  displays = []\n");
			}
			builder.Append( "}");
		}
		static void ScreenToBuilder( System.Text.StringBuilder builder)
		{
			builder.Append( "Screen = {\n");
			builder.AppendFormat( "  fullScreen = {0},\n", UnityEngine.Screen.fullScreen);
			builder.AppendFormat( "  fullScreenMode = {0},\n", UnityEngine.Screen.fullScreenMode);
			builder.AppendFormat( "  dpi = {0},\n", UnityEngine.Screen.dpi);
			builder.AppendFormat( "  width = {0},\n", UnityEngine.Screen.width);
			builder.AppendFormat( "  height = {0},\n", UnityEngine.Screen.height);
			builder.AppendFormat( "  currentResolution = {0},\n", UnityEngine.Screen.currentResolution);
			
			if( UnityEngine.Screen.resolutions.Length > 0)
			{
				builder.Append( "  resolutions = [\n");
				
				for( int i0 = 0; i0 < UnityEngine.Screen.resolutions.Length; ++i0)
				{
					builder.Append( "    {\n");
					builder.AppendFormat( "      width = {0},\n", UnityEngine.Screen.resolutions[ i0].width);
					builder.AppendFormat( "      height = {0},\n", UnityEngine.Screen.resolutions[ i0].height);
					builder.AppendFormat( "      refreshRate = {0}\n", UnityEngine.Screen.resolutions[ i0].refreshRate);
					builder.Append( "    },\n");
				}
				builder.Append( "  ],\n");
			}
			else
			{
				builder.Append( "  resolutions = [],\n");
			}
			builder.AppendFormat( "  safeArea = {0},\n", UnityEngine.Screen.safeArea);
			
			if( UnityEngine.Screen.cutouts.Length > 0)
			{
				builder.Append( "  cutouts = [\n");
				
				for( int i0 = 0; i0 < UnityEngine.Screen.cutouts.Length; ++i0)
				{
					builder.AppendFormat( "    {0},\n", UnityEngine.Screen.cutouts[ i0]);
				}
				builder.Append( "  ],\n");
			}
			else
			{
				builder.Append( "  cutouts = [],\n");
			}
			builder.AppendFormat( "  brightness = {0},\n", UnityEngine.Screen.brightness);
			builder.AppendFormat( "  orientation = {0},\n", UnityEngine.Screen.orientation);
			builder.AppendFormat( "  autorotateToLandscapeLeft = {0},\n", UnityEngine.Screen.autorotateToLandscapeLeft);
			builder.AppendFormat( "  autorotateToLandscapeRight = {0},\n", UnityEngine.Screen.autorotateToLandscapeRight);
			builder.AppendFormat( "  autorotateToPortrait = {0},\n", UnityEngine.Screen.autorotateToPortrait);
			builder.AppendFormat( "  autorotateToPortraitUpsideDown = {0},\n", UnityEngine.Screen.autorotateToPortraitUpsideDown);
			builder.AppendFormat( "  sleepTimeout = {0},\n", UnityEngine.Screen.sleepTimeout);
			builder.Append( "}");
		}
	}
}
