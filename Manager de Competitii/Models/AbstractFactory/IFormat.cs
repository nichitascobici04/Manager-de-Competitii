namespace Manager_de_Competitii.Models.AbstractFactory
{
    public interface IFormat
    {
        List<Stage> GenerateStages(List<Participant> participants);
    }
}
