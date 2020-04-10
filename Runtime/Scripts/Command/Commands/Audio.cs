
namespace DebugConsole.Command
{
	public sealed class Audio : Base
	{
		public Audio() : base( "オーディオ情報を表示します")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( "AudioSettings = {");
			builder.AppendFormat( "  driverCapabilities = {0},\n", UnityEngine.AudioSettings.driverCapabilities);
			builder.AppendFormat( "  dspTime = {0},\n", UnityEngine.AudioSettings.dspTime);
			builder.AppendFormat( "  outputSampleRate = {0},\n", UnityEngine.AudioSettings.outputSampleRate);
			builder.AppendFormat( "  speakerMode = {0},\n", UnityEngine.AudioSettings.speakerMode);
			builder.AppendLine( "}");
			builder.AppendLine( "AudioListener = {");
			builder.AppendFormat( "  pause = {0},\n", UnityEngine.AudioListener.pause);
			builder.AppendFormat( "  volume = {0},\n", UnityEngine.AudioListener.volume);
			builder.Append( "}");
			context.Output( builder.ToString());
			return true;
		}
	}
}
