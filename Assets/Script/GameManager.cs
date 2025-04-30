using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        
    }

    public List<Card> flippedCards = new List<Card>();
    public List<Sprite> finalSprite = new List<Sprite>();
    private bool checkingMatch = false;

    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI turnTxt;
    int tempscore = 0;
    int tempturn = 0;

    int score { 
        get { return tempscore; } 
        set { 
            tempscore = value;
            scoreTxt.text = $"Score : {value}";
        } 
    }
    int turn { 
        get { return tempturn; } 
        set { 
            tempturn = value; 
            turnTxt.text = $"Turn : {value}"; 
        } 
    }


    public void OnCardFlipped(Card card)
    {
        flippedCards.Add(card);
        

        // If at least 2 cards are flipped, check match
        if (flippedCards.Count >= 2 && !checkingMatch)
        {
            bool allMatch = flippedCards.All(c => c.cardType == flippedCards[0].cardType);
            if (allMatch)
            {
                Debug.Log($"match found {flippedCards[0].name} and {flippedCards[1].name}");
                flippedCards.ForEach(card => card.gameObject.SetActive(false));
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    flippedCards.ForEach(card => card.gameObject.SetActive(false));
                    flippedCards.Clear();
                    score += 1;
                    turn += 1;
                });
            }
            else
            {
                flippedCards.ForEach(card => card.HideCard());
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    flippedCards.Clear();
                    turn += 1;
                });
            }
            
        }

    }

    ///



    [System.Serializable]
    public struct CardData
    {
        public CardType type;
        public Sprite sprite;
    }

    public RectTransform gridParent;
    public GameObject cardPrefab;
    public List<CardData> availableCards;

    [Header("Grid Size")]
    public int rows = 4;
    public int columns = 4;
    public int spacing = 10;

    private void Start()
    {
        GenerateGrid();
        score = 0;
        turn = 0;
    }

    
    void GenerateGrid()
    {
        float panelWidth = gridParent.rect.width;
        float panelHeight = gridParent.rect.height;

        float totalSpacingX = spacing * (columns - 1);
        float totalSpacingY = spacing * (rows - 1);

        float availableWidth = panelWidth - totalSpacingX;
        float availableHeight = panelHeight - totalSpacingY;

        float cellSize = Mathf.Min(availableWidth / columns, availableHeight / rows);

        Vector2 startPosition = new Vector2(
            -((columns - 1) * (cellSize + spacing)) / 2f,
            ((rows - 1) * (cellSize + spacing)) / 2f
        );

        // Calculate number of pairs
        int totalCells = rows * columns;
        int pairCount = totalCells / 2;

        // Create a pool with only needed pairs
        List<CardData> pool = new List<CardData>();
        for (int i = 0; i < pairCount; i++)
        {
            CardData card = availableCards[i % availableCards.Count];
            pool.Add(card);
            pool.Add(card); // Add a pair
        }

        pool = Shuffle(pool);

        int cardIndex = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int currentCell = row * columns + col;
                if (totalCells % 2 != 0 && currentCell == totalCells - 1)
                {
                    // Leave last cell empty for odd grids
                    return;
                }

                if (cardIndex >= pool.Count) return;

                GameObject card = Instantiate(cardPrefab, gridParent); // Use gridParent
                RectTransform cardRect = card.GetComponent<RectTransform>();
                cardRect.sizeDelta = new Vector2(cellSize, cellSize);

                Vector2 pos = new Vector2(
                    startPosition.x + col * (cellSize + spacing),
                    startPosition.y - row * (cellSize + spacing)
                );
                cardRect.anchoredPosition = pos;

                card.GetComponent<Card>().SetCard(pool[cardIndex].type, pool[cardIndex].sprite);
                cardIndex++;
            }
        }
    }

    List<CardData> Shuffle<CardData>(List<CardData> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
        return list;
    }
}
