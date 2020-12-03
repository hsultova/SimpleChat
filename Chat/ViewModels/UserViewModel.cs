using Chat.ViewModels.Base;

namespace Chat.ViewModels
{
	public class UserViewModel : BaseViewModel
	{
		private string _userName;
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}
	}
}
