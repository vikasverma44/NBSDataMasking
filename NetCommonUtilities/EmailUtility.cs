using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace NetCommonUtilities
{
    public class EmailUtility
    {
        private readonly string SMTP_Host;
        private readonly int SMTP_Port;
        private readonly string SMTP_Usr;
        private readonly string SMTP_Pwd;

        public EmailUtility(string _SMTP_Host, int _SMTP_Port, string _SMTP_Usr, string _SMTP_Pwd)
        {
            this.SMTP_Usr = _SMTP_Usr;
            this.SMTP_Pwd = _SMTP_Pwd;
            this.SMTP_Port = _SMTP_Port;
            this.SMTP_Host = _SMTP_Host;
            this.SMTP_Port = _SMTP_Port;
            this.SMTP_Host = _SMTP_Host;
        }

        public bool SendMailer(string _ToEmail, string _FromEmail, string _Cc, string _Bcc, string _MailSubject, string _MailerFilePath, Dictionary<string, string> _MailerTagsAndContent)
        {
            string _mailerContent = ReadFileContentAsString(_MailerFilePath);
            foreach (var tag in _MailerTagsAndContent)
            {
                _mailerContent = _mailerContent.Replace(tag.Key, tag.Value);
            }
            return SendMailMessage(_ToEmail, _FromEmail, _Cc, _Bcc, _MailSubject, _mailerContent, null, true, true);
        }

        public bool SendMailMessage(string _ToEmail, string _FromEmail, string _Cc, string _Bcc, string _MailSubject, string _MailBody, List<string> _AttachmentFullPath, bool _EnableMailSSL, bool _IsHtmlEmail)
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
                if (!string.IsNullOrEmpty(_ToEmail))
                {
                    mMailMessage.To.Add(new MailAddress(_ToEmail));
                }

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
                if (!string.IsNullOrEmpty(_MailSubject))
                {
                    mMailMessage.Subject = _MailSubject;
                }

                //set the body of the mail body
                if (!string.IsNullOrEmpty(_MailBody))
                {
                    mMailMessage.Body = _MailBody;
                }

                if (_AttachmentFullPath != null)
                {
                    //add any attachments from the filesystem
                    foreach (string attachmentPath in _AttachmentFullPath)
                    {
                        Attachment mailAttachment = new Attachment(attachmentPath);
                        mMailMessage.Attachments.Add(mailAttachment);
                    }
                }

                //set the format of the mail IsBodyHtml
                mMailMessage.IsBodyHtml = _IsHtmlEmail;

                //set the priority
                mMailMessage.Priority = MailPriority.Normal;

                ////create the SmtpClient instance
                //SmtpClient mSmtpClient = new SmtpClient()
                //{
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(SMTP_Usr, SMTP_Pwd),
                //    Port = SMTP_Port,
                //    Host = SMTP_Host,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    EnableSsl = _EnableMailSSL,
                //};
                SmtpClient mSmtpClient;
                //create the SmtpClient instance
                if (!String.IsNullOrEmpty(SMTP_Usr))
                {
                    mSmtpClient = new SmtpClient()
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(SMTP_Usr, SMTP_Pwd),
                        Port = SMTP_Port,
                        Host = SMTP_Host,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl = _EnableMailSSL,

                    };
                }
                else
                {
                    mSmtpClient = new SmtpClient()
                    {
                        UseDefaultCredentials = true,
                        Credentials = CredentialCache.DefaultNetworkCredentials,
                        Port = SMTP_Port,
                        Host = SMTP_Host,
                    };
                }

                //send the mail message
                mSmtpClient.Send(mMailMessage);
                mSmtpClient.Dispose();
            }
            catch { throw; }
            return true;
        }

        public bool IsValidEmailAddress(string _EmailAddress)
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

        public string ReadFileContentAsString(string _FileNameWithPath)
        {
            if (File.Exists(_FileNameWithPath))
            {
                return File.ReadAllText(_FileNameWithPath);
            }
            else { return string.Empty; }
        }

    }
}
