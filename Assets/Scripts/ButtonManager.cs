using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject panelSettings;

    private void Start(){
        panelSettings.SetActive(false);
    }

    public void Play() {
        SceneManager.LoadScene("Main");

    }

    public void Settings() {
        if(panelSettings.activeSelf == false){
            panelSettings.SetActive(true);

        }
        else if(panelSettings.activeSelf == true) {
            panelSettings.SetActive(false);
        }
    }

    public void Exit() {
        Application.Quit();
    } 

}
