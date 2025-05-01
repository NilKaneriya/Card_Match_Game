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
        int revealTimeIndex = PlayerPrefs.GetInt("RevealTime", 0);
        float delay = GetTime(revealTimeIndex);
        Invoke(nameof(HideCard), delay);
    }
    float GetTime(float val)
    {
        float time = 0f;
        switch (val)
        {
            case 0:
                time = 0;
                break;

            case 1:
                time = 0.2f;
                break;

            case 2:
                time = 0.4f;
                break;

            case 3:
                time = 0.6f;
                break;

            case 4:
                time = 0.8f;
                break;

            case 5:
                time = 1f;
                break;

        }
        return time;
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
            SoundManager.instance.PlaySFX(SoundClip.FlipSound);
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
