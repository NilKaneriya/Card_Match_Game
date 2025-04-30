using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card : MonoBehaviour
{
    Image img;
    public Sprite flipSprite;
    public CardType cardType;
    [SerializeField] Sprite hideSprite;
    bool isAnimate;
    bool isSeen;

    private void OnEnable()
    {
        img = GetComponent<Image>();

        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            OpenAnimation();
        });

        isAnimate = false;
    }


    public void SetCard(CardType type, Sprite sprite)
    {
        cardType = type;
        flipSprite = sprite;

        // for showing first 1s 
        img.sprite = sprite;
        isSeen = true;
        Invoke(nameof(HideCard), 1f);
    }


    public void OpenAnimation()
    {
        if (!isSeen)
        {
            if (!isAnimate && (GameManager.instance.flippedCards.Count < 2))
            {
                isAnimate = true;
                ShowCard();
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
                GameManager.instance.OnCardFlipped(this);
            });
        });
    }

    public void HideCard()
    {
        if (isSeen)
        {
            transform.DOScaleX(0f, 0.15f).OnComplete(() =>
            {
                img.sprite = hideSprite;

                transform.DOScaleX(1f, 0.15f).OnComplete(() =>
                {
                    isAnimate = false;
                    isSeen = false;
                });
            });
        }
    }
}

public enum CardType 
{ 
    Cry, 
    Angry,
    Confusion,
    Shocking,
    Happy,
    Monkey,
    Pray,
    Smile,
    Heart,
    Fire,
    Sunglass,
    Money

}
