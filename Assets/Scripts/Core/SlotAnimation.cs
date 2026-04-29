using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

namespace com.eudiko.slotmachine
{
    public class SlotAnimation : MonoBehaviour
    {
        private RectTransform contentTransform;
        [SerializeField] private Image[] symbols;

        [SerializeField] private float spinSpeed = 2000f;
        private float symbolHeight = 100;

        private bool isSpinning;

        void Awake()
        {
            contentTransform = gameObject.GetComponent<RectTransform>();
        }

        void Start()
        {
            if (symbols.Length > 0)
                symbolHeight = symbols[0].rectTransform.sizeDelta.y;
        }

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                StartSpin();
            }

            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                StopSpin();
            }

            if (!isSpinning)
                return;

            contentTransform.anchoredPosition += Vector2.down * spinSpeed * Time.deltaTime;

            foreach (var symbol in symbols)
            {
                if (contentTransform.anchoredPosition.y < -symbolHeight * 3)
                {
                    contentTransform.anchoredPosition += Vector2.up * symbolHeight * symbols.Length;
                }
            }
        }

        public void StartSpin()
        {
            isSpinning = true;
            contentTransform.DOKill();
        }

        public Tween StopSpin()
        {
            isSpinning = false;
            float currentY = contentTransform.anchoredPosition.y;
            float snappedY = Mathf.Round(currentY / symbolHeight) * symbolHeight;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(contentTransform.DOAnchorPosY(snappedY - 40f, 0.25f)
                .SetEase(Ease.OutQuad));

            sequence.Append(contentTransform.DOAnchorPosY(snappedY, 0.35f)
                .SetEase(Ease.OutBounce));

            return sequence;
        }
    }
}
