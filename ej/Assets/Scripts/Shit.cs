using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shit : MonoBehaviour
{
    public float cooldown;
    [HideInInspector] public bool isColldown;

    private Image shieldImage;
    private Player player;

    void Start() {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isColldown = true;

    }


    void Update() {
        if(isColldown){
            shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if(shieldImage.fillAmount <= 0) {
                shieldImage.fillAmount = 1;
                isColldown = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer() {
        shieldImage.fillAmount = 1;
    }


    public void ReduceTime(int damage) {
        shieldImage.fillAmount += damage / 120f;
    }
}
