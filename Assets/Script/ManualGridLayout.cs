using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public int columns = 3;   
    public int rows = 3;     

    [Header("Spacing")]
    public Vector2 spacing = new Vector2(10f, 10f); 

    [Header("Padding")]
    public Vector2 padding = new Vector2(10f, 10f);

    [Header("Child Elements")]
    public RectTransform[] children; 

    private RectTransform panelRect;

    void Start()
    {
        ArrangeGrid();
    }

    public void ArrangeGrid()
    {
        if (children == null || children.Length == 0)
        {
            return;
        }

        panelRect = GetComponent<RectTransform>();

        float panelWidth = panelRect.rect.width - (padding.x * 2) - (spacing.x * (columns - 1));
        float panelHeight = panelRect.rect.height - (padding.y * 2) - (spacing.y * (rows - 1));

        float cellWidth = panelWidth / columns;
        float cellHeight = panelHeight / rows;

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] == null)
                continue;

            int row = i / columns;
            int column = i % columns;

            float x = padding.x + (cellWidth + spacing.x) * column;
            float y = -(padding.y + (cellHeight + spacing.y) * row);

            children[i].anchorMin = new Vector2(0, 1);
            children[i].anchorMax = new Vector2(0, 1);
            children[i].pivot = new Vector2(0, 1);
            children[i].sizeDelta = new Vector2(cellWidth, cellHeight);
            children[i].anchoredPosition = new Vector2(x, y);
        }
    }
}
