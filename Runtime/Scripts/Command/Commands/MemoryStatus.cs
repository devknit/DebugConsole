
namespace DebugConsole.Command
{
	public sealed class MemoryStatus : Base
	{
		public MemoryStatus() : base( "メモリの状態を表示します")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			long monoUsed = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong();
			long monoSize = UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong();
			long totalUsed = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong();
			long totalSize = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong();
			var builder = new System.Text.StringBuilder();
			builder.AppendFormat( "[Mono] {0:#,0} / {1:#,0} KiB ({2:f1}%)\n", 
				monoUsed / 1024, monoSize / 1024, 100.0 * monoUsed / monoSize);
			builder.AppendFormat( "[Total] {0:#,0}/{1:#,0} KiB ({2:f1}%)\n",
				totalUsed / 1024, totalSize / 1024, 100.0 * totalUsed / totalSize);
			context.Output( builder.ToString());
			return true;
		}
	}
}
