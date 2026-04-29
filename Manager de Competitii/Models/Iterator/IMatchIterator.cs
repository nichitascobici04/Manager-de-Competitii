namespace Manager_de_Competitii.Models.Iterator
{
    public interface IMatchIterator
    {
        bool HasNext();
        ScheduledMatch Next();
    }
}
