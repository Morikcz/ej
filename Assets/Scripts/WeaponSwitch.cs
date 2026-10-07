using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject gun;
    public GameObject noj;
    public GameObject bonys_opyjue;
    public int weapon;
    public Image iconWeapon123;

    private Animator anim;
    private Player player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = player.GetComponent<Animator>();
    }


    private void Update() {
        if(player.controlType == Player.ControlType.PC && Input.GetKeyDown(KeyCode.Alpha1)){
            if(gun.activeInHierarchy == false){
                gun.SetActive(true);
                noj.SetActive(false);
                bonys_opyjue.SetActive(false);
                weapon = 1;
                iconWeapon123.sprite = gun.GetComponent<SpriteRenderer>().sprite;
                anim.SetTrigger("switchWeapon");

            }
        }
        else if(player.controlType == Player.ControlType.PC && Input.GetKeyDown(KeyCode.Alpha2)){
            if(noj.activeInHierarchy == false){
                gun.SetActive(false);
                noj.SetActive(true);
                bonys_opyjue.SetActive(false);
                weapon = 2;
                iconWeapon123.sprite = noj.GetComponent<SpriteRenderer>().sprite;
                anim.SetTrigger("switchWeapon");

            }
        }
        else if(player.controlType == Player.ControlType.PC && Input.GetKeyDown(KeyCode.Alpha3)){
            if(bonys_opyjue.activeInHierarchy == false){
                if(player.bonys_opyjue_unlock == true)
                {
                    gun.SetActive(false);
                    noj.SetActive(false);
                    bonys_opyjue.SetActive(true);
                    weapon = 3;
                    iconWeapon123.sprite = bonys_opyjue.GetComponent<SpriteRenderer>().sprite;
                    anim.SetTrigger("switchWeapon");
                }
                
            }
        }
    }
}
