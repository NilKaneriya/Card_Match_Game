using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public static HomeManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject HomePanel;
    public GameObject GamePanel;
    public Button playGame;
    public Button settingPanel;
    public Button HomeBtn;
    public Slider difficultySlider;
    public GameManager gameManager;

    private void OnEnable()
    {
        playGame.onClick.AddListener(() =>
        {
            HomePanel.SetActive(false);
            GamePanel.SetActive(true);
            gameManager.InitGame(gridValue((int)difficultySlider.value));
        });

        settingPanel.onClick.AddListener(() =>
        {

        });

    }


    public void OpenHomePanel()
    {
        HomePanel.SetActive(true);
        GamePanel.SetActive(false);
    }
    Vector2 gridValue(int index)
    {
        switch (index)
        {
            case 1:
                return new Vector2(3, 3);
            case 2:
                return new Vector2(4, 4);
            case 3:
                return new Vector2(5, 5);
            case 4:
                return new Vector2(5, 6);
            case 5:
                return new Vector2(6, 7);
            default:
                return new Vector2(3, 3);
        }
    }
}
