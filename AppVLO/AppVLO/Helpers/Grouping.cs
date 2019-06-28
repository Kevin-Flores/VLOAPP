using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppVLO.Helpers
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

    }
}
