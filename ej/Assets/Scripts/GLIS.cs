using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLIS : MonoBehaviour
{
    public static GLIS Instance;


    public float sliderValue = 8f;
    public int postProcCheckB;


    private void Awake() {
        if(Instance == null) {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            LoadSettings();
        }
        else {
            Destroy(gameObject);
        }

    }

    public void SaveValueSlider(float value) {
        sliderValue = value;

        PlayerPrefs.SetFloat("sliderValue", value);
        PlayerPrefs.Save();
    }

    public void SaveValueCheck(int valueC) {
        postProcCheckB = valueC;

        PlayerPrefs.SetInt("postProcCheckB", valueC);
        PlayerPrefs.Save();
    }
    

    private void LoadSettings() {
        sliderValue = PlayerPrefs.GetFloat("sliderValue", 8f);
        postProcCheckB = PlayerPrefs.GetInt("postProcCheckB", 1);

    }
}
