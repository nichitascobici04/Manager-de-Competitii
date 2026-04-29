namespace Manager_de_Competitii.Models.Iterator
{
    public interface IMatchCollection
    {
        IMatchIterator CreateIterator();
        IMatchIterator CreateStadiumIterator(string stadiumName);
    }
}
