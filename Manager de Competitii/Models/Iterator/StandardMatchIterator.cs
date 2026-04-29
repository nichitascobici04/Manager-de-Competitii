namespace Manager_de_Competitii.Models.Iterator
{
    public class StandardMatchIterator : IMatchIterator
    {
        private MatchList _collection;
        private int _position = 0;

        public StandardMatchIterator(MatchList collection)
        {
            _collection = collection;
        }

        public bool HasNext()
        {
            return _position < _collection.GetItems().Count;
        }

        public ScheduledMatch Next()
        {
            if (HasNext())
            {
                return _collection.GetItems()[_position++];
            }
            return null;
        }
    }
}
