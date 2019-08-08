using System.Threading.Tasks;

namespace WebApp.Notifications
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}
