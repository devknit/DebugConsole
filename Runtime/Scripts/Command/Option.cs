
namespace DebugConsole.Command
{
	internal class Option
	{
		public Option( string key, string name, int count, string description)
		{
			this.key = key;
			this.name = name;
			this.count = count;
			this.description = description.Trim();
		}
		
		public int count;
		public string key;
		public string name;
		public string description;
	}
}
