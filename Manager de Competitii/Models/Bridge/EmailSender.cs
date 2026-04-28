namespace Manager_de_Competitii.Models.Bridge
{
    public class EmailSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"[EmailSender] Sending via Email: {message}");
        }
    }
}
