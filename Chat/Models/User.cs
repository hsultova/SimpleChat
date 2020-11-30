using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
	public class User
	{
		public User(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public int Id { get; private set; }

		private const string AnonymousName = "Anonymous";

		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			private set
			{
				_name = value;
				if (string.IsNullOrEmpty(_name))
				{
					_name = AnonymousName;
				}
			}
		}

	}
}
