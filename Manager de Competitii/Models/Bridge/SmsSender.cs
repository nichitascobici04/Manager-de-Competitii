namespace Manager_de_Competitii.Models.Bridge
{
    public class SmsSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"[SmsSender] Sending via SMS: {message}");
        }
    }
}
