
namespace DebugConsole.Command
{
	public sealed class FrameRate : Base
	{
		public FrameRate() : base( "フレームレートを変更します")
		{
			AddOption( "VSyncCountを指定(1 or 2)", 1, "vsync", "v");
			AddOption( "FrameRateを指定(1...300)", 1, "frame", "f");
			AddOption( "RenderingIntervalを指定(1...60)", 1, "render", "r");
		}
		protected override bool OnInvoke( Context context)
		{
			int prevSyncCount = UnityEngine.QualitySettings.vSyncCount;
			int prevFrameRate = UnityEngine.Application.targetFrameRate;
			int prevInterval = UnityEngine.Rendering.OnDemandRendering.renderFrameInterval;
			string[] values;
			bool ret = false;
			
			if( context.argv.TryGetValue( "vsync", out values) != false)
			{
				int vSyncCount;
				
				if( int.TryParse( values[ 0], out vSyncCount) != false)
				{
					if( vSyncCount > 0 && vSyncCount <= 2)
					{
						UnityEngine.QualitySettings.vSyncCount = vSyncCount;
						ret = true;
					}
				}
			}
			if( context.argv.TryGetValue( "frame", out values) != false)
			{
				int frameRate;
				
				if( int.TryParse( values[ 0], out frameRate) != false)
				{
					if( frameRate > 0 && frameRate <= 300)
					{
						UnityEngine.QualitySettings.vSyncCount = 0;
						UnityEngine.Application.targetFrameRate = frameRate;
						ret = true;
					}
				}
				
			}
			if( context.argv.TryGetValue( "render", out values) != false)
			{
				int interval;
				
				if( int.TryParse( values[ 0], out interval) != false)
				{
					if( interval >= 0 && interval <= 60)
					{
						UnityEngine.Rendering.OnDemandRendering.renderFrameInterval = interval;
						ret = true;
					}
				}
				
			}
			else if( context.args.Length == 1)
			{
				context.Output( string.Format( $"VSyncCount = {UnityEngine.QualitySettings.vSyncCount}"));
				context.Output( string.Format( $"FrameRate = {UnityEngine.Application.targetFrameRate}"));
				context.Output( string.Format( $"RenderingInterval = {UnityEngine.Rendering.OnDemandRendering.renderFrameInterval}"));
				return true;
			}
			if( ret != false)
			{
				if( prevSyncCount != UnityEngine.QualitySettings.vSyncCount)
				{
					context.Output( string.Format( $"VSyncCount has changed. {prevSyncCount} => {UnityEngine.QualitySettings.vSyncCount}"));
				}
				else
				{
					context.Output( string.Format( $"VSyncCount = {UnityEngine.QualitySettings.vSyncCount}"));
				}
				if( prevFrameRate != UnityEngine.Application.targetFrameRate)
				{
					context.Output( string.Format( $"FrameRate has changed. {prevFrameRate} => {UnityEngine.Application.targetFrameRate}"));
				}
				else
				{
					context.Output( string.Format( $"FrameRate = {UnityEngine.Application.targetFrameRate}"));
				}
				if( prevInterval != UnityEngine.Rendering.OnDemandRendering.renderFrameInterval)
				{
					context.Output( string.Format( $"RenderingInterval has changed. {prevInterval} => {UnityEngine.Rendering.OnDemandRendering.renderFrameInterval}"));
				}
				else
				{
					context.Output( string.Format( $"RenderingInterval = {UnityEngine.Rendering.OnDemandRendering.renderFrameInterval}"));
				}
			}
			return ret;
		}
	}
}
