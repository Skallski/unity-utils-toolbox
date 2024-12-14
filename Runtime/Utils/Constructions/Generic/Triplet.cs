using UnityEngine;

namespace UtilsToolbox.Constructions.Generic
{
    [System.Serializable]
    public struct Triplet<TItem1, TItem2, TItem3>
    {
        [field: SerializeField] public TItem1 Item1 { get; set; }
        [field: SerializeField] public TItem2 Item2 { get; set; }
        [field: SerializeField] public TItem3 Item3 { get; set; }

        public Triplet((TItem1, TItem2, TItem3) triplet)
        {
            Item1 = triplet.Item1;
            Item2 = triplet.Item2;
            Item3 = triplet.Item3;
        }
        
        public Triplet(TItem1 item1, TItem2 item2, TItem3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
    }
}