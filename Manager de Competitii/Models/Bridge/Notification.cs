namespace Manager_de_Competitii.Models.Bridge
{
    public abstract class Notification
    {
        protected IMessageSender _sender;

        public Notification(IMessageSender sender)
        {
            _sender = sender;
        }

        public abstract void Notify(string subject, string details);
    }
}
