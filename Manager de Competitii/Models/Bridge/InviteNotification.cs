namespace Manager_de_Competitii.Models.Bridge
{
    public class InviteNotification : Notification
    {
        public InviteNotification(IMessageSender sender) : base(sender) { }

        public override void Notify(string subject, string details)
        {
            string fullMessage = $"Invitation to Join: {subject}. Details: {details}";
            _sender.SendMessage(fullMessage);
        }
    }
}
