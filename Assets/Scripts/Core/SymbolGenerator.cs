using UnityEngine;

namespace com.eudiko.slotmachine
{
    public class SymbolGenerator : MonoBehaviour
    {
        public static SymbolGenerator Instance { get; private set; }

        [SerializeField] private SymbolData[] symbolPool;

        void Awake()
        {
            Instance = this;
        }

        public SymbolData PickSymbol()
        {
            int total = 0;
            foreach (var symbol in symbolPool)
                total += symbol.weight;

            int roll = Random.Range(0, total);

            int cumlative = 0;
            foreach (var symbol in symbolPool)
            {
                cumlative += symbol.weight;
                if (roll < cumlative)
                    return symbol;
            }

            return symbolPool[symbolPool.Length - 1];//fallback
        }

        public SymbolData[] PickAllReels()
        {
            return new SymbolData[]
            {
                PickSymbol(),
                PickSymbol(),
                PickSymbol()

            };
        }
    }
}
