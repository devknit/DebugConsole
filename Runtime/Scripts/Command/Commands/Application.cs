
namespace DebugConsole.Command
{
	public sealed class Application : Base
	{
		public Application() : base( "アプリケーションの情報を表示します")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( "Application = {");
			builder.AppendFormat( "  absoluteURL = {0},\n", UnityEngine.Application.absoluteURL);
			builder.AppendFormat( "  backgroundLoadingPriority = {0},\n", UnityEngine.Application.backgroundLoadingPriority);
			builder.AppendFormat( "  buildGUID = {0},\n", UnityEngine.Application.buildGUID);
			builder.AppendFormat( "  cloudProjectId = {0},\n", UnityEngine.Application.cloudProjectId);
			builder.AppendFormat( "  companyName = {0},\n", UnityEngine.Application.companyName);
			builder.AppendFormat( "  consoleLogPath = {0},\n", UnityEngine.Application.consoleLogPath);
			builder.AppendFormat( "  dataPath = {0},\n", UnityEngine.Application.dataPath);
			builder.AppendFormat( "  genuine = {0},\n", UnityEngine.Application.genuine);
			builder.AppendFormat( "  genuineCheckAvailable = {0},\n", UnityEngine.Application.genuineCheckAvailable);
			builder.AppendFormat( "  identifier = {0},\n", UnityEngine.Application.identifier);
			builder.AppendFormat( "  installerName = {0},\n", UnityEngine.Application.installerName);
			builder.AppendFormat( "  installMode = {0},\n", UnityEngine.Application.installMode);
			builder.AppendFormat( "  internetReachability = {0},\n", UnityEngine.Application.internetReachability);
			builder.AppendFormat( "  isBatchMode = {0},\n", UnityEngine.Application.isBatchMode);
			builder.AppendFormat( "  isConsolePlatform = {0},\n", UnityEngine.Application.isConsolePlatform);
			builder.AppendFormat( "  isEditor = {0},\n", UnityEngine.Application.isEditor);
			builder.AppendFormat( "  isFocused = {0},\n", UnityEngine.Application.isFocused);
			builder.AppendFormat( "  isMobilePlatform = {0},\n", UnityEngine.Application.isMobilePlatform);
			builder.AppendFormat( "  isPlaying = {0},\n", UnityEngine.Application.isPlaying);
			builder.AppendFormat( "  persistentDataPath = {0},\n", UnityEngine.Application.persistentDataPath);
			builder.AppendFormat( "  platform = {0},\n", UnityEngine.Application.platform);
			builder.AppendFormat( "  productName = {0},\n", UnityEngine.Application.productName);
			builder.AppendFormat( "  runInBackground = {0},\n", UnityEngine.Application.runInBackground);
			builder.AppendFormat( "  sandboxType = {0},\n", UnityEngine.Application.sandboxType);
			builder.AppendFormat( "  streamingAssetsPath = {0},\n", UnityEngine.Application.streamingAssetsPath);
			builder.AppendFormat( "  systemLanguage = {0},\n", UnityEngine.Application.systemLanguage);
			builder.AppendFormat( "  targetFrameRate = {0},\n", UnityEngine.Application.targetFrameRate);
			builder.AppendFormat( "  temporaryCachePath = {0},\n", UnityEngine.Application.temporaryCachePath);
			builder.AppendFormat( "  unityVersion = {0},\n", UnityEngine.Application.unityVersion);
			builder.AppendFormat( "  version = {0},\n", UnityEngine.Application.version);
			builder.Append( "}");
			context.Output( builder.ToString());
			return true;
		}
	}
}
