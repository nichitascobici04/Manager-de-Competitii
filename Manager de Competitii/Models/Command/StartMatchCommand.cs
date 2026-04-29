namespace Manager_de_Competitii.Models.Command
{
    public class StartMatchCommand : ICommand
    {
        private MatchController _receiver;

        public StartMatchCommand(MatchController receiver)
        {
            _receiver = receiver;
        }

        public void Execute()
        {
            _receiver.Start();
        }
    }
}
