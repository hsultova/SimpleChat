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

		public DateTime DateTime { get; set; }

		public string UserName { get; set; }
	}
}
