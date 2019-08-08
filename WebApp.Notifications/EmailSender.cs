using System;
using System.Net.Mail;

namespace WebApp.Notifications
{
	public class EmailSender
	{
		public static void Main(string[] args)
		{
			SmtpClient client = new SmtpClient(args[0]);

			MailAddress from = new MailAddress("fori.bori@abv.bg",
			   "Hristo " + (char)0xD8 + "Varbanov",
			System.Text.Encoding.UTF8);

			MailAddress to = new MailAddress("keltata@abv.bg");

			MailMessage message = new MailMessage(from, to);
			message.Body = "This is a test email message sent by an application. ";

			string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
			message.Body += Environment.NewLine + someArrows;
			message.BodyEncoding = System.Text.Encoding.UTF8;
			message.Subject = "TEST MESSAGE ASGJQWJNGWJQ" + someArrows;
			message.SubjectEncoding = System.Text.Encoding.UTF8;

			string userState = "test message1";
			client.SendAsync(message, userState);

			message.Dispose();
		}
	}
}
