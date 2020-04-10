
namespace DebugConsole.Command
{
	public sealed class InternetProtocol : Base
	{
		public InternetProtocol() : base( "IP情報を表示します")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			var builder = new System.Text.StringBuilder();
			
			string hostname = System.Net.Dns.GetHostName();
			
			builder.Append( "[HostName] ");
			builder.AppendLine( hostname);
			
		#if !UNITY_WEBGL || UNITY_EDITOR
			var addresses = System.Net.Dns.GetHostAddresses( hostname);
			
			foreach( var address in addresses)
			{
				switch( address.AddressFamily)
				{
					case System.Net.Sockets.AddressFamily.InterNetwork:
					{
						builder.Append( "[IPv4]");
						break;
					}
					case System.Net.Sockets.AddressFamily.InterNetworkV6:
					{
						if( address.IsIPv6LinkLocal == false)
						{
							builder.Append( "[IPv6] ");
						}
						else
						{
							builder.Append( "[link-local IPv6] ");
						}
						break;
					}
					default:
					{
						builder.Append( string.Format( "[{0}] ", address.AddressFamily));
						break;
					}
				}
				builder.AppendLine( address.ToString());
			}
		#endif
			context.Output( builder.ToString());
			return true;
		}
	}
}
