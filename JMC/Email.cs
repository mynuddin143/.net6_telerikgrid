using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace JMC {
	public class Email {
		#region Properties

		private MailMessage MailMessage { get; set; }

		#endregion


		#region Constructors

		public Email(string from, string to, string cc, string bcc, string subject, string body) {
			this.MailMessage = new MailMessage();

			string fromAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
			this.MailMessage.From = new MailAddress(fromAddress, from);

			string[] bccemails = bcc.Split(';');
			foreach (string email in bccemails) {
				if (email.Trim().Length != 0 && email.Contains("@")) {
					this.MailMessage.Bcc.Add(email);
				}
			}

			string[] ccemails = cc.Split(';');
			foreach (string email in ccemails) {
				if (email.Trim().Length != 0 && email.Contains("@")) {
					this.MailMessage.CC.Add(email);
				}
			}

			string[] toemails = to.Split(';');
			foreach (string email in toemails) {
				if (email.Trim().Length != 0 && email.Contains("@")) {
					this.MailMessage.To.Add(email);
				}
			}

			this.MailMessage.Subject = subject;
			this.MailMessage.Body = body;
			this.MailMessage.IsBodyHtml = true;
		}

		#endregion


		#region Methods

		public void AddAttachment(string path) {
			this.MailMessage.Attachments.Add(new Attachment(path));
		}

		public void Send() {
			SmtpClient client = new SmtpClient();
			client.Send(this.MailMessage);
		}

		#endregion


		#region Static Methods

		public static void SendMessage(string to, string cc, string subject, string body) {
			SendMessage("", to, cc, string.Empty, subject, body);
		}

		public static void SendMessage(string from, string to, string cc, string subject, string body) {
			SendMessage(from, to, cc, string.Empty, subject, body);
		}

		public static void SendMessage(string from, string to, string cc, string bcc, string subject, string body) {
			if (!to.jIsEmpty()) {
				MailMessage message = new MailMessage();
				string fromAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
				message.From = new MailAddress(fromAddress, from);
				string[] bccApplicationAddress = ConfigurationManager.AppSettings["BCCEmailAddress"].Split(';');

				List<string> originalDestinations = new List<string>();

				foreach (string email in bccApplicationAddress) {
					if (email.Trim().Length != 0 && email.Contains("@")) {
						message.Bcc.Add(email);
						originalDestinations.Add(email);
					}
				}

				string[] bccemails = bcc.Split(';');
				foreach (string email in bccemails) {
					if (email.Trim().Length != 0 && email.Contains("@")) {
						message.Bcc.Add(email);
						originalDestinations.Add(email);
					}
				}

				string[] ccemails = cc.Split(';');
				foreach (string email in ccemails) {
					if (email.Trim().Length != 0 && email.Contains("@")) {
						message.CC.Add(email);
						originalDestinations.Add(email);
					}
				}

				string[] toemails = to.Split(';');
				foreach (string email in toemails) {
					if (email.Trim().Length != 0 && email.Contains("@")) {
						message.To.Add(email);
						originalDestinations.Add(email);
					}
				}

				message.Subject = subject;
				message.Body = body;
				message.IsBodyHtml = true;

#if DEBUG
			message.CC.Clear();
			message.To.Clear();
			message.Bcc.Clear();
			string[] toEmails = ConfigurationManager.AppSettings["ErrorEmailAddress"].TrimNull().Split(';');
			foreach (string email in toEmails) {
				if (email.Trim().Length != 0 && email.Contains("@")) {
					message.To.Add(email);
				}
			}
			body += "Debug message: Email would have been Sent to (";
			foreach (string eadd in originalDestinations) {
				body += eadd + " ";
			}
			body += ")";
#else
				string FromEmailTitle = ConfigurationManager.AppSettings["FromEmailTitle"];
				if (FromEmailTitle.Contains("DEV") || FromEmailTitle.Contains("QA") || FromEmailTitle.Contains("Local")) {
					message.CC.Clear();
					message.To.Clear();
					message.Bcc.Clear();
					string[] toEmails = (ConfigurationManager.AppSettings["ErrorEmailAddress"] ?? "").Trim().Split(';');
					foreach (string email in toEmails) {
						if (email.Trim().Length != 0 && email.Contains("@")) {
							message.To.Add(email);
						}
					}
					body += "Debug message: Email would have been Sent to (";
					foreach (string eadd in originalDestinations) {
						body += eadd + " ";
					}
					body += ")";
				}
#endif
				try {
					SmtpClient client = new SmtpClient();
					client.Send(message);
				} catch {
					message = new MailMessage();
					string fromAddress2 = ConfigurationManager.AppSettings["FromEmailAddress"];
					message.From = new MailAddress(fromAddress, from);
					message.Subject = subject;
					body += "Failed to send Email to one or more of the following (";
					foreach (string eadd in originalDestinations) {
						body += eadd + " ";
					}
					body += ")";
					message.Body = body;
					message.IsBodyHtml = true;

					string[] failToSendAddress = ConfigurationManager.AppSettings["FailToSendAddress"].Split(';');
					if (failToSendAddress.IsNull() || failToSendAddress.Count() == 0) {
						failToSendAddress = ConfigurationManager.AppSettings["ErrorEmailAddress"].Split(';');
					}
					foreach (string email in failToSendAddress) {
						if (email.Trim().Length != 0 && email.Contains("@")) {
							message.To.Add(email);
						}
					}
					SmtpClient client = new SmtpClient();
					client.Send(message);
				}
			}
		}


        public static void SendMessage(string from, string to, string cc, string bcc, string subject, string body, AlternateView htmlView)
        {
            if (!to.jIsEmpty())
            {
                MailMessage message = new MailMessage();
                string fromAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
                message.From = new MailAddress(fromAddress, from);
                string[] bccApplicationAddress = ConfigurationManager.AppSettings["BCCEmailAddress"].Split(';');

                List<string> originalDestinations = new List<string>();

                foreach (string email in bccApplicationAddress)
                {
                    if (email.Trim().Length != 0 && email.Contains("@"))
                    {
                        message.Bcc.Add(email);
                        originalDestinations.Add(email);
                    }
                }

                string[] bccemails = bcc.Split(';');
                foreach (string email in bccemails)
                {
                    if (email.Trim().Length != 0 && email.Contains("@"))
                    {
                        message.Bcc.Add(email);
                        originalDestinations.Add(email);
                    }
                }

                string[] ccemails = cc.Split(';');
                foreach (string email in ccemails)
                {
                    if (email.Trim().Length != 0 && email.Contains("@"))
                    {
                        message.CC.Add(email);
                        originalDestinations.Add(email);
                    }
                }

                string[] toemails = to.Split(';');
                foreach (string email in toemails)
                {
                    if (email.Trim().Length != 0 && email.Contains("@"))
                    {
                        message.To.Add(email);
                        originalDestinations.Add(email);
                    }
                }

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(htmlView);


#if DEBUG
                message.CC.Clear();
                message.To.Clear();
                message.Bcc.Clear();
                string[] toEmails = ConfigurationManager.AppSettings["ErrorEmailAddress"].TrimNull().Split(';');
                foreach (string email in toEmails)
                {
                    if (email.Trim().Length != 0 && email.Contains("@"))
                    {
                        message.To.Add(email);
                    }
                }
                body += "Debug message: Email would have been Sent to (";
                foreach (string eadd in originalDestinations)
                {
                    body += eadd + " ";
                }
                body += ")";
#else
				string FromEmailTitle = ConfigurationManager.AppSettings["FromEmailTitle"];
				if (FromEmailTitle.Contains("DEV") || FromEmailTitle.Contains("QA") || FromEmailTitle.Contains("Local")) {
					message.CC.Clear();
					message.To.Clear();
					message.Bcc.Clear();
					string[] toEmails = (ConfigurationManager.AppSettings["ErrorEmailAddress"] ?? "").Trim().Split(';');
					foreach (string email in toEmails) {
						if (email.Trim().Length != 0 && email.Contains("@")) {
							message.To.Add(email);
						}
					}
					body += "Debug message: Email would have been Sent to (";
					foreach (string eadd in originalDestinations) {
						body += eadd + " ";
					}
					body += ")";
				}
#endif
                try
                {
                    SmtpClient client = new SmtpClient();
                    client.Send(message);
                }
                catch
                {
                    message = new MailMessage();
                    string fromAddress2 = ConfigurationManager.AppSettings["FromEmailAddress"];
                    message.From = new MailAddress(fromAddress, from);
                    message.Subject = subject;
                    body += "Failed to send Email to one or more of the following (";
                    foreach (string eadd in originalDestinations)
                    {
                        body += eadd + " ";
                    }
                    body += ")";
                    message.Body = body;
                    message.IsBodyHtml = true;

                    string[] failToSendAddress = ConfigurationManager.AppSettings["FailToSendAddress"].Split(';');
                    if (failToSendAddress.IsNull() || failToSendAddress.Count() == 0)
                    {
                        failToSendAddress = ConfigurationManager.AppSettings["ErrorEmailAddress"].Split(';');
                    }
                    foreach (string email in failToSendAddress)
                    {
                        if (email.Trim().Length != 0 && email.Contains("@"))
                        {
                            message.To.Add(email);
                        }
                    }
                    SmtpClient client = new SmtpClient();
                    client.Send(message);
                }
            }
        }

		#endregion


		#region IDisposable Members



		#endregion
	}
}
