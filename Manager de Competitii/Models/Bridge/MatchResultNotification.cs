namespace Manager_de_Competitii.Models.Bridge
{
    public class MatchResultNotification : Notification
    {
        public MatchResultNotification(IMessageSender sender) : base(sender) { }

        public override void Notify(string subject, string details)
        {
            string fullMessage = $"Match Result: {subject} - {details}";
            _sender.SendMessage(fullMessage);
        }
    }
}
