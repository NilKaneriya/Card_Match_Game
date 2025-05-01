using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlippingAnimation : MonoBehaviour
{
    Image img;
    public Sprite flipSprite;
    [SerializeField] Sprite hideSprite;
    bool isAnimate;
    bool isSeen;

    private void OnEnable()
    {
        img = GetComponent<Image>();
        isAnimate = false;
    }

    public void OpenAnimation()
    {
        if(!isSeen)
        {
            if(!isAnimate)
            {
                isAnimate = true;
                ShowCard();
            }
        }
        else
        {
            if(!isAnimate)
            {
                isAnimate = true;
                HideCard();
            }
        }
    }
    void ShowCard()
    {
        transform.DOScaleX(0f, 0.15f).OnComplete(() =>
        {
            img.sprite = flipSprite;

            // Expand the card back to normal
            transform.DOScaleX(1f, 0.15f).OnComplete(() =>
            {
                isAnimate = false;
                isSeen = true;
            });
        });
    }

    void HideCard()
    {
        if(isSeen)
        {
            transform.DOScaleX(0f, 0.15f).OnComplete(() =>
            {
                img.sprite = hideSprite;

                // Expand the card back to normal
                transform.DOScaleX(1f, 0.15f).OnComplete(() =>
                {
                    isAnimate = false;
                    isSeen = false;
                });
            });
        }
    }
}
