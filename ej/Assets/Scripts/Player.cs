using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : Sounds
{
    [Header("CONTROLS")]
    public static bool poBorot = true;
    public ControlType controlType;
    public enum ControlType{PC, Android}
    public float Speed;
    public Joystick joy;


    [Header("HEALTH")]
    public float health = 100;
    public GameObject effect_podbor;
    public Image bar;
    public float fill;
    public GameObject floatingDamage;

    [Header("SHIELD")]
    public GameObject shield;
    public Shit shieldTimer;
    public GameObject effect_podbor_shit;

    private Animator anim;
    private Animator camAnim;
    private Rigidbody2D rb;
    public Vector2 moveInput;
    private Vector2 moveVelocity;


    [Header("WEAPONS")]
    public List<GameObject> unlockWeapons;
    public GameObject[] allWeapons;
    public Image iconWeapon;
    public GameObject noj;
    public GameObject bonys_opyjue;
    public GameObject gun;
    public WeaponSwitch weapons; 
    public bool bonys_opyjue_unlock = false;


    [Header("KEY")]
    public GameObject keyIcon;
    public GameObject wallEffect;


    public AudioSource sound_xodba;
    private bool keyButtonPushed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        weapons = GetComponent<WeaponSwitch>();

        if(controlType == ControlType.PC) {
            joy.gameObject.SetActive(false);
        }

        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

        fill = health / 100;
        bar.fillAmount = fill;
        
        if(controlType == ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
        }
        else if(controlType == ControlType.Android)
        {
            moveInput = new Vector2(joy.Horizontal, joy.Vertical);

        }


        moveVelocity = moveInput * Speed;

        if(moveInput.x != 0 || moveInput.y != 0){
            if((joy.Horizontal <= 0.5 && joy.Horizontal >= -0.5) || (joy.Vertical <= 0.5 && joy.Vertical >= -0.5)){
                anim.SetBool("isRun", true);
                anim.SetBool("isRunSpeed", false);
            }
            if((joy.Horizontal >= 0.5 || joy.Horizontal <= -0.5) || (joy.Vertical >= 0.5 || joy.Vertical <= -0.5)){
                anim.SetBool("isRunSpeed", true);
                anim.SetBool("isRun", false);
            }
            
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isRunSpeed", false);
        }

        

        if(transform.localScale.x < 0 && moveInput.x > 0){
            Flip();
        }
        else if(transform.localScale.x > 0 && moveInput.x < 0){
            Flip();
        }


        if(health <= 0){
            if(poBorot == true){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else {
                poBorot = !poBorot;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }

        if(health >= 101) {
            health = 100;
        }

        if(Input.GetKeyDown(KeyCode.Q)) SwitchWeapon();

        /*if(controlType == ControlType.PC && Input.GetKeyDown(KeyCode.Alpha1)){
            SwitchWeapon();
        }
        else if(controlType == ControlType.PC && Input.GetKeyDown(KeyCode.Alpha2)){
            SwitchWeapon();
        }*/


        //kolxo3uwe
        for (int i = 0; i < unlockWeapons.Count; i++){
            if(unlockWeapons[i].activeInHierarchy){
                weapons.weapon = i;
            }
        }

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


    private void Flip(){
        poBorot = !poBorot;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        
    }


    public void ChangeHealth(int healthValue){
        if(!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0){
            health += healthValue;
        }
        else if(shield.activeInHierarchy && healthValue < 0){
            shieldTimer.ReduceTime(healthValue);
        }

        if(healthValue < 0 && !shield.activeInHierarchy){
            camAnim.SetTrigger("tryac");
            anim.SetTrigger("bol");
            Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 2.56f);
            Instantiate(floatingDamage, damagePos, Quaternion.identity);
        }
    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("au-2")){
            ChangeHealth(+50);
            Destroy(other.gameObject);
            Instantiate(effect_podbor, other.transform.position, Quaternion.identity);
        }
        else if(other.CompareTag("shit")){
            if(!shield.activeInHierarchy){
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isColldown = true;
                Destroy(other.gameObject);
                Instantiate(effect_podbor_shit, other.transform.position, Quaternion.identity);
            }
            else{
                shieldTimer.ResetTimer();
                Instantiate(effect_podbor_shit, other.transform.position, Quaternion.identity);

                Destroy(other.gameObject);
            }
        }
        else if(other.CompareTag("bonys_opyjue")){
            for(int i = 0; i < allWeapons.Length; i++){
                if(other.name == allWeapons[i].name){
                    unlockWeapons.Add(allWeapons[i]);
                    bonys_opyjue_unlock = true;
                }
            }
            SwitchWeapon();
            Destroy(other.gameObject);
        }

        else if(other.CompareTag("key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
        }

    }


    public void OnKeyButtonDown() {
        keyButtonPushed = !keyButtonPushed;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Door") && keyButtonPushed && keyIcon.activeInHierarchy){
            Instantiate(wallEffect, other.transform.position, Quaternion.identity);
            keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonPushed = false;
        }
    }

    public void SwitchWeapon() {
        for(int i = 0; i < unlockWeapons.Count; i++) 
        {
            if(unlockWeapons[i].activeInHierarchy) 
            {
                unlockWeapons[i].SetActive(false);
                if(i != 0) 
                {
                    iconWeapon.sprite = unlockWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                    unlockWeapons[i - 1].SetActive(true);
                    anim.SetTrigger("switchWeapon");
                    weapons.weapon = i;
                }

                else
                {
                    unlockWeapons[unlockWeapons.Count - 1].SetActive(true);
                    iconWeapon.sprite = unlockWeapons[unlockWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
                    anim.SetTrigger("switchWeapon");
                    weapons.weapon = 2;
                }
                break;
                iconWeapon.SetNativeSize();
            }
        }
        
    }
}   

