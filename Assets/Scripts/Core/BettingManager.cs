using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.eudiko.slotmachine
{
    public class BettingManager : MonoBehaviour
    {
        public static BettingManager Instance { get; private set; }

        [SerializeField] private int startingGold = 200;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI betText;
        [SerializeField] private TextMeshProUGUI resultText;

        [SerializeField] private Button bet10Button;
        [SerializeField] private Button bet20Button;
        [SerializeField] private Button bet50Button;

        private int currentGold;
        private int currentBet = 10;

        void Awake()
        {
            Instance = this;
            currentGold = startingGold;
        }

        void Start()
        {
            bet10Button.onClick.AddListener(() => SetBet(10));
            bet20Button.onClick.AddListener(() => SetBet(20));
            bet50Button.onClick.AddListener(() => SetBet(50));
            UpdateUI();
        }

        private void SetBet(int betAmount)
        {
            currentBet = betAmount;
            AudioManager.Instance.PlayBetButton();
            UpdateUI();
        }

        public bool ReduceBetAmount()
        {
            if (currentGold < currentBet)
            {
                resultText.text = "Not Enough Gold";
                return false;
            }

            currentGold -= currentBet;
            resultText.text = " ";
            UpdateUI();
            return true;
        }

        public void OnLoss()
        {
            resultText.text = "Better Luck Next Time";
        }

        public void OnWin(int payoutMultiplier)
        {
            int winAmount = currentBet * payoutMultiplier;
            currentGold += winAmount;
            resultText.text = "$You Won {winAmount} G";
            UpdateUI();
        }

        private void UpdateUI()
        {
            goldText.text = $"Gold: {currentGold}";
            betText.text = $"Bet: {currentBet}";
            bet10Button.interactable = currentGold >= 10;
            bet20Button.interactable = currentGold >= 20;
            bet50Button.interactable = currentGold >= 50;
        }

    }
}
