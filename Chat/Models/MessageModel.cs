using System;

namespace Chat.Models
{
	public class MessageModel
	{
		public MessageModel(string content, DateTime dateTime, string userName)
		{
			Content = content ?? string.Empty;
			DateTime = dateTime;
			UserName = userName;
		}

		public string Content { get; set; }

		private DateTime _dateTime;
		public DateTime DateTime
		{
			get
			{
				return _dateTime;
			}
			private set
			{
				_dateTime = value;
				if (_dateTime.Date == DateTime.Now.Date)
				{
					DateTimeDisplay = _dateTime.ToString("HH:mm");
				}
				else
				{
					DateTimeDisplay = _dateTime.ToString();
				}
			}
		}

		public string DateTimeDisplay { get; set; }

		public string UserName { get; set; }
	}
}
