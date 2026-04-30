using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            contentTransform = GetComponent<RectTransform>();
        }

        void Start()
        {
            if (symbols.Length > 0)
                symbolHeight = symbols[0].rectTransform.sizeDelta.y;
        }

        void Update()
        {
            if (!isSpinning) return;

            contentTransform.anchoredPosition += Vector2.down * spinSpeed * Time.deltaTime;

            foreach (var symbol in symbols)
            {
                float viewportY = contentTransform.anchoredPosition.y
                                + symbol.rectTransform.anchoredPosition.y;

                if (viewportY < -symbolHeight * 2)
                {
                    float maxLocalY = float.MinValue;
                    foreach (var s in symbols)
                        if (s.rectTransform.anchoredPosition.y > maxLocalY)
                            maxLocalY = s.rectTransform.anchoredPosition.y;

                    symbol.rectTransform.anchoredPosition =
                        new Vector2(0, maxLocalY + symbolHeight);
                }
            }
        }

        public void StartSpin()
        {
            contentTransform.DOKill();
            isSpinning = true;
        }

        public Tween StopSpin(SymbolData target)
        {
            isSpinning = false;

            float currentY = contentTransform.anchoredPosition.y;

            Image closestSymbol = null;
            float closestDist = float.MaxValue;

            foreach (var sym in symbols)
            {
                float viewportY = currentY + sym.rectTransform.anchoredPosition.y;
                float dist = Mathf.Abs(viewportY);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestSymbol = sym;
                }
            }

            if (target != null && closestSymbol != null)
            {
                closestSymbol.sprite = target.sprite;
            }

            float targetY = closestSymbol != null
                ? -closestSymbol.rectTransform.anchoredPosition.y
                : Mathf.Round(currentY / symbolHeight) * symbolHeight;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(contentTransform.DOAnchorPosY(targetY - 40f, 0.25f)
                .SetEase(Ease.OutQuad));
            sequence.Append(contentTransform.DOAnchorPosY(targetY, 0.35f)
                .SetEase(Ease.OutBounce));

            return sequence;
        }
    }
}
