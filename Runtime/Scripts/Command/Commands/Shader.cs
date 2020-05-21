
namespace DebugConsole.Command
{
	public sealed class Shader : Base
	{
		public Shader() : base( "シェーダ情報を表示します")
		{
			AddOption( "グローバルLODを設定します", 1, "lod", "l");
		}
		protected override bool OnInvoke( Context context)
		{
			string[] values;
			
			if( context.argv.TryGetValue( "lod", out values) != false)
			{
				int lod;
				
				bool result = int.TryParse( values[ 0], out lod);
				if( result != false)
				{
					if( lod > 0 && lod <= int.MaxValue)
					{
						UnityEngine.Shader.globalMaximumLOD = lod;
					}
					else
					{
						result = false;
					}
				}
				if( result == false)
				{
					context.Output( "引数が不定です");
					return false;
				}
			}
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( "Shader = {");
			builder.AppendFormat( "  globalMaximumLOD = {0},\n", UnityEngine.Shader.globalMaximumLOD);
			builder.AppendFormat( "  globalRenderPipeline = {0},\n", UnityEngine.Shader.globalRenderPipeline);
			builder.Append( "}");
			context.Output( builder.ToString());
			return true;
		}
	}
}
