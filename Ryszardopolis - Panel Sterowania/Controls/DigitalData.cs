namespace RyszardopolisPanelSterowania.Controls
{
    using System.Collections;
    using System.Collections.Generic;

    internal struct DigitalData : IEnumerable<Dictionary<string, bool>.Enumerator>
    {
        private Dictionary<string, bool> data;

        public delegate void DataChangedHandler(DataChangedEventArgs args);

        public event DataChangedHandler DataChanged;

        public int Count => data.Count;

        public bool this[string key]
        {
            get
            {
                return data[key];
            }

            set
            {
                if (!data.ContainsKey(key))
                    data.Add(key, value);
                DataChanged?.Invoke(new DataChangedEventArgs(key, value));
            }
        }

        public DigitalData()
        {
            data = new Dictionary<string, bool>();
            DataChanged = null;
        }

        public bool Contains(string key)
        {
            return data.ContainsKey(key);
        }

        IEnumerator<Dictionary<string, bool>.Enumerator> IEnumerable<Dictionary<string, bool>.Enumerator>.GetEnumerator()
        {
            yield return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return data.GetEnumerator();
        }
    }
}