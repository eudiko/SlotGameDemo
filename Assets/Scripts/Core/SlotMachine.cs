using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.eudiko.slotmachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private SlotAnimation reelLeft;
        [SerializeField] private SlotAnimation reelCenter;
        [SerializeField] private SlotAnimation reelRight;

        [SerializeField] private float reelStopInterval = 0.4f;
        [SerializeField] private float minSpinDuration = 1.5f;

        private SymbolData[] results;
        private bool isSpinning = false;

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && !isSpinning)
                StartCoroutine(SpinSlots());
        }

        private IEnumerator SpinSlots()
        {
            isSpinning = true;

            results = SymbolGenerator.Instance.PickAllReels();

            reelLeft.StartSpin();
            reelCenter.StartSpin();
            reelRight.StartSpin();

            yield return new WaitForSeconds(minSpinDuration);

            Tween leftTween = reelLeft.StopSpin(results[0]);
            yield return leftTween.WaitForCompletion();

            yield return new WaitForSeconds(reelStopInterval);

            Tween centerTween = reelCenter.StopSpin(results[1]);
            yield return centerTween.WaitForCompletion();

            yield return new WaitForSeconds(reelStopInterval);

            Tween rightTween = reelRight.StopSpin(results[2]);
            yield return rightTween.WaitForCompletion();

            EvaluateResult();

            isSpinning = false;
        }

        private void EvaluateResult()
        {
            bool isWin = results[0].thisSymbol == results[1].thisSymbol
                      && results[1].thisSymbol == results[2].thisSymbol;

            if (isWin)
            {
                Debug.Log("Win");
            }
            else
            {
                Debug.Log("Lose");
            }
        }
    }
}
