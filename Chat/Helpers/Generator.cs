using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Helpers
{
	public static class Generator
	{
		public static string GenerateId()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
