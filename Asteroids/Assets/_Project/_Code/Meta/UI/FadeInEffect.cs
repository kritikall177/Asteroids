using DG.Tweening;
using UnityEngine;

namespace _Project._Code.Meta.UI
{
    public class FadeInEffect : IFadeEffect
    {
        private float fadeDuration = 1f;

        public void FadeAnimation(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(1f, fadeDuration).SetUpdate(true).OnComplete(() => EnableCanvasGroup(canvasGroup));
        }

        private void EnableCanvasGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}