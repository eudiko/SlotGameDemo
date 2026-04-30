using TMPro;
using UnityEngine;

namespace com.eudiko.slotmachine
{
    public class BettingManager : MonoBehaviour
    {
        public static BettingManager Instance { get; private set; }

        [SerializeField] private int startingGold = 200;
        private TextMeshProUGUI goldText;
        private TextMeshProUGUI betText;
        private TextMeshProUGUI resultText;

    }
}
