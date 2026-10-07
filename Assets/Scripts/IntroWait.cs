using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroWait : MonoBehaviour
{
    public float waitTimeIntro;

    private void Start() {
        StartCoroutine(waitForIntro());

    }


    IEnumerator waitForIntro()  {
        yield return new WaitForSeconds(waitTimeIntro);
        SceneManager.LoadScene(1);
    }

}
