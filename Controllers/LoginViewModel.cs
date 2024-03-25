using System.ComponentModel.DataAnnotations;

namespace Demo1_Day2
{
	public class LoginViewModel
	{
		public string Email { get; set; }
		[DataType(DataType.Password)]//this will change the input type to password
		public string Password { get; set; }
	}
}