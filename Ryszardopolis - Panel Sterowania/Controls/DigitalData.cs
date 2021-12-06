namespace RyszardopolisPanelSterowania.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
using static RyszardopolisPanelSterowania.Controls.DigitalData;

    internal struct DigitalData : IEnumerable<Dictionary<string, (bool value, DataChangedHandler eventHandler)>.Enumerator>
    {
        private static Dictionary<string, (bool value, DataChangedHandler eventHandler)> data;

        public delegate void DataChangedHandler(DataChangedEventArgs args);
        public int Count => data.Count;

        public bool this[string key]
        {
            get
            {
                if (!data.ContainsKey(key))
                {
                    data.Add(key, (false, null));
                    data[key].eventHandler?.Invoke(new DataChangedEventArgs(key, data[key].value));
                }
                return data[key].value;
            }

            set
            {
                if (!data.ContainsKey(key))
                {
                    data.Add(key, (value, null));
                    data[key].eventHandler?.Invoke(new DataChangedEventArgs(key, value));
                }
                if (data[key].value != value)
                {
                    var d = data[key];
                    d.value = value;
                    data[key] = d;
                    data[key].eventHandler?.Invoke(new DataChangedEventArgs(key, value));
                }
            }
        }

        public static bool GetData(string key)
        {
            if (!data.ContainsKey(key))
            {
                data.Add(key, (false, null));
                data[key].eventHandler?.Invoke(new DataChangedEventArgs(key, data[key].value));
            }
            return data[key].value;
        }

        public void RegisterElement(string key, DataChangedHandler method)
        {
            if (!data.ContainsKey(key))
            {
                data.Add(key, (false, null));
            }
            var d = data[key];
            d.eventHandler += method;
            data[key] = d;
        }

        public void UnregisterEvent(string key, DataChangedHandler method)
        {
            if (!data.ContainsKey(key))
            {
                data.Add(key, (false, null));
            }
            else
            {
                var d = data[key];
                d.eventHandler -= method;
                data[key] = d;
            }
        }

        public DigitalData()
        {
            data = new Dictionary<string, (bool value, DataChangedHandler eventHandler)>();
        }

        public bool Contains(string key)
        {
            return data.ContainsKey(key);
        }

        IEnumerator<Dictionary<string, (bool value, DataChangedHandler eventHandler)>.Enumerator> IEnumerable<Dictionary<string, (bool value, DataChangedHandler eventHandler)>.Enumerator>.GetEnumerator()
        {
            yield return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return data.GetEnumerator();
        }
    }
}