namespace CFRs.BE.Model
{
    public class SendEmailEntity
    {
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MailCC { get; set; }
        public string MailBCC { get; set; }
    }
}