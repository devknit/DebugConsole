
namespace DebugConsole.Command
{
	public sealed class Paths : Base
	{
		public Paths() : base( "パスの情報を表示します")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendFormat( "UnityEngine.Application.consoleLogPath{1}>\"{0}\"{1}{1}", 
				UnityEngine.Application.consoleLogPath, System.Environment.NewLine);
			builder.AppendFormat( "UnityEngine.Application.dataPath{1}>\"{0}\"{1}{1}", 
				UnityEngine.Application.dataPath, System.Environment.NewLine);
			builder.AppendFormat( "UnityEngine.Application.persistentDataPath{1}>\"{0}\"{1}{1}", 
				UnityEngine.Application.persistentDataPath, System.Environment.NewLine);
			builder.AppendFormat( "UnityEngine.Application.streamingAssetsPath{1}>\"{0}\"{1}{1}", 
				UnityEngine.Application.streamingAssetsPath, System.Environment.NewLine);
			builder.AppendFormat( "UnityEngine.Application.temporaryCachePath{1}>\"{0}\"{1}{1}", 
				UnityEngine.Application.temporaryCachePath, System.Environment.NewLine);
			builder.AppendFormat( "UnityEngine.Caching.defaultCache.path{1}>\"{0}\"{1}{1}", 
				UnityEngine.Caching.defaultCache.path, System.Environment.NewLine);
			builder.AppendFormat( "UnityEngine.Caching.currentCacheForWriting.path{1}>\"{0}\"{1}{1}", 
				UnityEngine.Caching.currentCacheForWriting.path, System.Environment.NewLine);
			context.Output( builder.ToString());
			return true;
		}
	}
}
