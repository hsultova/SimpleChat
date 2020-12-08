using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ChatWindowsService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			if (System.Environment.UserInteractive)
			{
				if (args.Count() == 1)
				{
					if (args[0] == "-install")
						ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });

					if (args[0] == "-uninstall")
						ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
				}
				return;
			}

			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
				new NetworkService()
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
