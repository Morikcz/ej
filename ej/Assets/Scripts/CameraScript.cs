using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
//using UnityEngine.Rendering.Universal;


public class CameraScript : MonoBehaviour
{


   // private UniversalAdditionalCameraData  cameraData;
    public PostProcessVolume postLayer;

     

    void Start()
    {
        postLayer = GetComponent<PostProcessVolume>();

        if(GLIS.Instance.postProcCheckB == 1){
            //cameraData.renderPostProcessing = true;
            postLayer.enabled = true;
            Debug.Log("da");

        }
        else if(GLIS.Instance.postProcCheckB == 0){
            //cameraData.renderPostProcessing = false;
            postLayer.enabled = false;
            Debug.Log("net");
        }

    }
    //переделать в START

    /*void Update() {
        Debug.Log("GLIS.Instance: " + (GLIS.Instance == null ? "NULL" : "OK"));

        if(GLIS.Instance.postProcCheckB == 1){
            //cameraData.renderPostProcessing = true;
            postLayer.enabled = false;
            Debug.Log("da");

        }
        else if(GLIS.Instance.postProcCheckB == 0){
            //cameraData.renderPostProcessing = false;
            postLayer.enabled = false;
            Debug.Log("net");
        }

    }*/
   
}
