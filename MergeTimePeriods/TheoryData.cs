namespace MergeTimePeriods
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public abstract class TheoryDataCollection : IEnumerable<object[]>
    {
        private readonly Collection<object[]> data = new Collection<object[]>();

        protected void AddDataItem(params object[] values)
        {
            data.Add(values);
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TheoryDataCollection<T1, T2> : TheoryDataCollection
    {
        public void Add(T1 item1, T2 item2)
        {
            AddDataItem(item1, item2);
        }
    }
}