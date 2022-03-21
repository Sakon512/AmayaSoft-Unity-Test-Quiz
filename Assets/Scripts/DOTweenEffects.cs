using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DOTweenEffects : MonoBehaviour
{
    Sequence seq;
    float textFadeInDuration = 0.7f;
    float textFadeOutDuration = 0.7f;
    int imageHalfFadeOutDuration = 1;
    int imageFadeInDuration = 1;
    int imageFadeOutDuration = 1;

    public int GetImageFadeInDuration()
    {
        return imageFadeInDuration;
    }
    public int GetImageFadeOutDuration()
    {
        return imageFadeOutDuration;
    }
    public void TextFadeIn(Text text)
    {
        text.DOFade(1, textFadeInDuration);
    }
    public void TextFadeOut(Text text)
    {
        text.DOFade(0, textFadeOutDuration);  
    }
    public void ImageFadeIn(Image image)   
    {
        image.DOFade(1, imageFadeInDuration);
    }
    public void ImageFadeOut(Image image)   
    {
        image.DOFade(0, imageFadeOutDuration).OnComplete(() => image.gameObject.SetActive(false));
    }
    public void ImageHalfFadeOut(Image image)
    {
        image.DOFade(0.6f, imageHalfFadeOutDuration);
    }

    public void RisingBounceEffect(Transform transform)
    {
        seq = DOTween.Sequence();
        seq
            .Append(transform.DOScale(1.3f, 0.3f))
            .Append(transform.DOScale(1f, 0.3f))
            .SetEase(Ease.OutBounce);
    }

    public void CardAppearRisingBounseEffect(Transform transform)
    {
        transform
               .DOScale(0.8f, 1.15f)
               .SetEase(Ease.OutBounce);
    }

    public void WrongCardBounceEffect(Transform transform, Card card)
    {
        seq = DOTween.Sequence();
        seq
            .Append(transform.DOLocalMoveX(15, 0.7f))
            .Append(transform.DOLocalMoveX(-20, 0.4f))
            .Append(transform.DOLocalMoveX(10, 0.4f))
            .Append(transform.DOLocalMoveX(0, 0.4f))
            .SetEase(Ease.OutBack)
            .AppendCallback(() => card.GetComponent<Card>().SetAnimating(false));
    }
}
