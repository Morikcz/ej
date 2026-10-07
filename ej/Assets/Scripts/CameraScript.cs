using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;


public class CameraScript : MonoBehaviour
{


    private UniversalAdditionalCameraData  cameraData;
    public Volume volume;

    void Start()
    {
        cameraData = GetComponent<UniversalAdditionalCameraData>();
        volume = GetComponent<Volume>();
        Debug.Log(volume != null ? "Volume найден" : "Volume НЕ найден");
    }
    //переделать в START

    void Update() {
        if(GLIS.Instance.postProcCheckB == 1){
            volume.enabled = true;

        }
        else if(GLIS.Instance.postProcCheckB == 0){
            volume.enabled = false;
        }
    }
   
}
