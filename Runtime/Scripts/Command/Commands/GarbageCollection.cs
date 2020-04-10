
namespace DebugConsole.Command
{
	public sealed class GarbageCollection : Base
	{
		public GarbageCollection() : base( "現在割当たれているマネージドメモリのサイズを表示します")
		{
			AddOption( "ガベージコレクションの発生を待ってからサイズを計算します", 0, "wait", "w");
		}
		protected override bool OnInvoke( Context context)
		{
			var builder = new System.Text.StringBuilder();
			bool wait = false;
			
			if( context.argv.ContainsKey( "wait") != false)
			{
				wait = true;
			}
			builder.AppendFormat( "[Mono] {0:#,0} KiB\n", System.GC.GetTotalMemory( wait) / 1024);
			context.Output( builder.ToString());
			return true;
		}
	}
}
