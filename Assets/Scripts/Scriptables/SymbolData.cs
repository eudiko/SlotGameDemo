using UnityEngine;

namespace com.eudiko.slotmachine
{
    public enum Symbol
    {
        Seven,
        Cherry,
        Bell,
        Bar,
    }
    [CreateAssetMenu(fileName = "SymbolAsset", menuName = "Slot/Symbol")]
    public class SymbolData : ScriptableObject
    {
        public Symbol thisSymbol;
        public Sprite sprite;
        public int weight;
        public int payoutMultiplier;
    }
}
