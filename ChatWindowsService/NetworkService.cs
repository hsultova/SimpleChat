using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ChatWindowsService
{
	public partial class NetworkService : ServiceBase
	{
		public NetworkService()
		{
			InitializeComponent();
		}

		internal static ServiceHost host = null;

		protected override void OnStart(string[] args)
		{
			if (host != null)
			{
				host.Close();
			}
			host = new ServiceHost(typeof(ChatService.ChatService));
			host.Open();
		}

		protected override void OnStop()
		{
			if (host != null)
			{
				host.Close();
				host = null;
			}
		}
	}
}
