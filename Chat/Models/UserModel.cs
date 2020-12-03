namespace Chat.Models
{
	public class UserModel
	{
		public UserModel(int id, string name)
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
