using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingPanel : MonoBehaviour
{
    public Toggle SoundTgl;
    public List<Toggle> BgColorTgl;
    public Slider revealTime;
    public TextMeshProUGUI revealTimeTxt;

    // Start is called before the first frame update
    private void OnEnable()
    {
        SoundTgl.isOn = PlayerPrefs.GetInt("Sound", 1) == 0 ? false : true;

        int BgColor = PlayerPrefs.GetInt("BgColor", 0);
        BgColorTgl[BgColor].isOn = true;


        revealTime.value = PlayerPrefs.GetInt("RevealTime", 0);
        GetTime(revealTime.value);

        revealTime.onValueChanged.AddListener((float val) =>
        {
            float time = GetTime(val);
            PlayerPrefs.SetInt("RevealTime", (int)val);
        });


        SoundTgl.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
            {
                SoundManager.instance.SetVolume(1);
                PlayerPrefs.SetInt("Sound", 1);
            }
            else
            {
                SoundManager.instance.SetVolume(0);
                PlayerPrefs.SetInt("Sound", 0);
            }
        });

        int i = 0;
        foreach(Toggle tgl in BgColorTgl)
        {
            int index = i;
            tgl.onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    PlayerPrefs.SetInt("BgColor", index);
                    Debug.Log("color is : " + index);
                    HomeManager.instance.BgColor(index);
                }
            });
            i++;
        }
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
        revealTimeTxt.text = $"{time}s";
        return time;
    }


    enum BGColor
    {
        yellow = 0,
        red = 1,
        green = 2,
        white = 3
    }
}
