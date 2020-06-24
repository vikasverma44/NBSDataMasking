using NetFileWatcherWinService.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace NetFileWatcherWinService.Services
{
    class EmailService
    {
        readonly Logger Logger;
        readonly ConfigHelper ConfigHelper;
        public EmailService(Logger Logger, ConfigHelper configHelper)
        {
            this.Logger = Logger;
            this.ConfigHelper = configHelper;
        }

        public bool SendNotification(string _EmailBody)
        {
            try
            {
                string mailTo = ConfigHelper.Model.SMTP_To;
                string mailFrom = ConfigHelper.Model.SMTP_From;
                string mailBCC = string.Empty;
                string mailCC = string.Empty;
                string mailSubject = "NBS System Generated Mail. Please Do not Reply";
                string mailBody = _EmailBody;
                bool mailHTML = true;
                List<string> mailAttachmentPath = new List<string>();
                return SendMailMessage(mailTo, mailFrom, mailBCC, mailCC, mailSubject,
                    mailBody, mailAttachmentPath, mailHTML);
            }
            catch { return false; }
        }

        /// <summary>
        /// This helper class sends an email message using the System.Net.Mail namespace
        /// </summary>
        /// <param name="_FromEmail">Sender email address</param>
        /// <param name="_ToEmail">Recipient email address</param>
        /// <param name="_Bcc">Blind carbon copy email address</param>
        /// <param name="_Cc">Carbon copy email address</param>
        /// <param name="_MailSubject">Subject of the email message</param>
        /// <param name="_MailBody">Body of the email message</param>
        /// <param name="attachment">File to attach</param>
        private bool SendMailMessage(string _ToEmail, string _FromEmail, string _Cc, string _Bcc,
            string _MailSubject, string _MailBody, List<string> _AttachmentFullPath, bool _HtmlEnabled)
        {
            try
            {
                //create the MailMessage object
                MailMessage mMailMessage = new MailMessage();

                //set the sender address of the mail message
                if (!string.IsNullOrEmpty(_FromEmail))
                {
                    mMailMessage.From = new MailAddress(_FromEmail);
                }

                //set the recipient address of the mail message
                mMailMessage.To.Add(new MailAddress(_ToEmail));

                //set the carbon copy address
                if (!string.IsNullOrEmpty(_Cc))
                {
                    mMailMessage.CC.Add(new MailAddress(_Cc));
                }

                //set the blind carbon copy address
                if (!string.IsNullOrEmpty(_Bcc))
                {
                    mMailMessage.Bcc.Add(new MailAddress(_Bcc));
                }

                //set the subject of the mail message
                if (string.IsNullOrEmpty(_MailSubject))
                {
                    mMailMessage.Subject = "NBS System Generated Mail. Please Do not Reply";
                }
                else
                {
                    mMailMessage.Subject = _MailSubject;
                }

                //set the body of the mail message
                mMailMessage.Body = _MailBody;

                //set the format of the mail message body
                mMailMessage.IsBodyHtml = _HtmlEnabled;

                //set the priority
                mMailMessage.Priority = MailPriority.Normal;

                //add any attachments from the filesystem
                foreach (var attachmentPath in _AttachmentFullPath)
                {
                    Attachment mailAttachment = new Attachment(attachmentPath);
                    mMailMessage.Attachments.Add(mailAttachment);
                }

                SmtpClient mSmtpClient;
                //create the SmtpClient instance

                if (!String.IsNullOrEmpty(ConfigHelper.Model.SMTP_Usr))
                {
                    mSmtpClient = new SmtpClient()
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(ConfigHelper.Model.SMTP_Usr, ConfigHelper.Model.SMTP_Pwd),
                        Port = ConfigHelper.Model.SMTP_Port,
                        Host = ConfigHelper.Model.SMTP_Host,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl = true,

                    };
                }
                else
                {
                    mSmtpClient = new SmtpClient()
                    {
                        UseDefaultCredentials = true,
                        Credentials = CredentialCache.DefaultNetworkCredentials,
                        Port = ConfigHelper.Model.SMTP_Port,
                        Host = ConfigHelper.Model.SMTP_Host,
                    };
                }

                //send the mail message
                mSmtpClient.Send(mMailMessage);
                mSmtpClient.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SendMailMessage");
                return false;
            }
            return true;
        }


        /// <summary>
        /// Determines whether an email address is valid.
        /// </summary>
        /// <param name="_EmailAddress">The email address to validate.</param>
        /// <returns>
        /// 	<c>true</c> if the email address is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidEmailAddress(string _EmailAddress)
        {
            // An empty or null string is not valid
            if (String.IsNullOrEmpty(_EmailAddress))
            {
                return (false);
            }

            // Regular expression to match valid email address
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            // Match the email address using a regular expression
            Regex re = new Regex(emailRegex);
            if (re.IsMatch(_EmailAddress))
                return (true);
            else
                return (false);
        }
    }
}
