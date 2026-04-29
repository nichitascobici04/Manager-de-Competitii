namespace Manager_de_Competitii.Models.Command
{
    public class CancelMatchCommand : ICommand
    {
        private MatchController _receiver;

        public CancelMatchCommand(MatchController receiver)
        {
            _receiver = receiver;
        }

        public void Execute()
        {
            _receiver.Cancel();
        }
    }
}
