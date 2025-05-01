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
    public GameObject SettingPanel;
    public Button playGame;
    public Button settingBtn;
    public Button HomeBtn;
    public Slider difficultySlider;
    public GameManager gameManager;

    public Color[] bgColor;
    public Camera cam;
    public void BgColor(int color)
    {
        cam.backgroundColor = bgColor[color];
    }


    private void OnEnable()
    {
        int volume = PlayerPrefs.GetInt("Sound", 1);
        SoundManager.instance.SetVolume(volume);
        BgColor(PlayerPrefs.GetInt("BgColor", 0));


        playGame.onClick.AddListener(() =>
        {
            HomePanel.SetActive(false);
            GamePanel.SetActive(true);
            gameManager.InitGame(gridValue((int)difficultySlider.value));
        });

        settingBtn.onClick.AddListener(() =>
        {
            SettingPanel.SetActive(true);
        });

        LoadGameIfExist();
    }
    void LoadGameIfExist()
    {
        SaveData data = SaveManager.LoadGame();
        if (data == null)
        {
            OpenHomePanel();
        }
        else
        {
            if (data.score == data.maxScore)
            {
                SaveManager.DeleteSave();
                OpenHomePanel();
                return;
            }
            HomePanel.SetActive(false);
            GamePanel.SetActive(true);
            gameManager.LoadSavedGame(data);
        }
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

