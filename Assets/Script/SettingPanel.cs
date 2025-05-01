using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public Toggle SoundTgl;
    
    // Start is called before the first frame update
    private void OnEnable() 
    {
        SoundTgl.isOn = PlayerPrefs.GetInt("Sound", 1) == 0 ? false : true;


        SoundTgl.onValueChanged.AddListener((bool isOn) => 
        {
            if(isOn) 
            {
                SoundManager.instance.SetVolume(1);
                PlayerPrefs.SetInt("Sound", 1);
            }
            else{
                SoundManager.instance.SetVolume(0);
                PlayerPrefs.SetInt("Sound", 0);
            }
        });



    }

    
}
