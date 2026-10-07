using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSceneUI : MonoBehaviour
{
    public Slider slider;
    public Toggle postProc;

    private void Start() {
        if(GLIS.Instance != null) {
            slider.value = GLIS.Instance.sliderValue;
            if(GLIS.Instance.postProcCheckB == 0){
                postProc.isOn = false;
            }
            else if(GLIS.Instance.postProcCheckB == 1){
                postProc.isOn = true;
            }
        }
    }


    public void OnSliderChanged() {
        if(GLIS.Instance != null){
            GLIS.Instance.SaveValueSlider(slider.value);
        }
    }

    //PPT - postProcessingToggle
    public void OnPPTChanged() {
        if(GLIS.Instance != null) {
            if(postProc.isOn == true) {
                GLIS.Instance.SaveValueCheck(1);

            }
            else if(postProc.isOn == false) {

                GLIS.Instance.SaveValueCheck(0);
            }
        }
    }
}
