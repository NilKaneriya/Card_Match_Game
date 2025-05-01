using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataModel
{
    [System.Serializable]
    public class SaveData
    {
        public int score;
        public int turn;
        public int rows;
        public int columns;
        public List<SavedCard> cards;
    }

    [System.Serializable]
    public class SavedCard
    {
        public CardType type;
        public bool isMatched;
        public int row;
        public int column;
    }
}
