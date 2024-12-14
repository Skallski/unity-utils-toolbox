using UnityEngine;

namespace UtilsToolbox.Constructions.Generic
{
    [System.Serializable]
    public struct Pair<TItem1, TItem2>
    {
        [field: SerializeField] public TItem1 Item1 { get; set; }
        [field: SerializeField] public TItem2 Item2 { get; set; }

        public Pair((TItem1, TItem2) pair)
        {
            Item1 = pair.Item1;
            Item2 = pair.Item2;
        }

        public Pair(TItem1 item1, TItem2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}