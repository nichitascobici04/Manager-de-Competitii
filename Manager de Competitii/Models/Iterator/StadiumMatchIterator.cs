namespace Manager_de_Competitii.Models.Iterator
{
    public class StadiumMatchIterator : IMatchIterator
    {
        private MatchList _collection;
        private string _targetStadium;
        private int _position = 0;

        public StadiumMatchIterator(MatchList collection, string targetStadium)
        {
            _collection = collection;
            _targetStadium = targetStadium;
        }

        public bool HasNext()
        {
            while (_position < _collection.GetItems().Count)
            {
                if (_collection.GetItems()[_position].Stadium == _targetStadium)
                {
                    return true;
                }
                _position++;
            }
            return false;
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
