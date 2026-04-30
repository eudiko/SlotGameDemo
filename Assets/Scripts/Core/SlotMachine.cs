using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace com.eudiko.slotmachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private SlotAnimation reelLeft;
        [SerializeField] private SlotAnimation reelCenter;
        [SerializeField] private SlotAnimation reelRight;

        [SerializeField] private float reelStopInterval = 0.4f;
        [SerializeField] private float minSpinDuration = 1.5f;
        [SerializeField] private Button spinButton;

        private SymbolData[] results;
        private bool isSpinning = false;

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                OnSpinButtonPressed();
        }

        public void OnSpinButtonPressed()
        {
            if (!isSpinning)
            {
                AudioManager.Instance.PlaySpinButton();
                StartCoroutine(SpinSlots());
            }
        }

        private IEnumerator SpinSlots()
        {
            isSpinning = true;
            if (spinButton != null) spinButton.interactable = false;

            if (!BettingManager.Instance.ReduceBetAmount())
            {
                isSpinning = false;
                if (spinButton != null) spinButton.interactable = true;
                yield break;
            }

            results = SymbolGenerator.Instance.PickAllReels();

            AudioManager.Instance.PlaySpinLoop();

            reelLeft.StartSpin();
            reelCenter.StartSpin();
            reelRight.StartSpin();

            yield return new WaitForSeconds(minSpinDuration);

            Tween leftTween = reelLeft.StopSpin(results[0]);
            yield return leftTween.WaitForCompletion();
            AudioManager.Instance.PlayReelStop();

            yield return new WaitForSeconds(reelStopInterval);

            Tween centerTween = reelCenter.StopSpin(results[1]);
            yield return centerTween.WaitForCompletion();
            AudioManager.Instance.PlayReelStop();

            yield return new WaitForSeconds(reelStopInterval);

            Tween rightTween = reelRight.StopSpin(results[2]);
            yield return rightTween.WaitForCompletion();
            AudioManager.Instance.PlayReelStop();

            AudioManager.Instance.StopSpinLoop();

            EvaluateResult();

            isSpinning = false;
            if (spinButton != null) spinButton.interactable = true;
        }

        private void EvaluateResult()
        {
            bool isWin = results[0].thisSymbol == results[1].thisSymbol
                      && results[1].thisSymbol == results[2].thisSymbol;

            if (isWin)
            {
                AudioManager.Instance.PlayWin();
                BettingManager.Instance.OnWin(results[0].payoutMultiplier);
            }
            else
            {
                AudioManager.Instance.PlayLose();
                BettingManager.Instance.OnLoss();
            }
        }
    }
}
