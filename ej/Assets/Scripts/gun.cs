using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GunType gunType;
    public float offset;
    public GameObject bull;
    public Transform shotPos;
    public GameObject effect;

    private float timeShots;
    public float startTimeShots;

    public GameObject soundsPistol;
    private Animator camAnim;

    private float rotZ;
    private Vector3 difference;
    private Player player;
    public Joystick joystick;
    public enum GunType{Default, enemy};

    public float angle;

    public float startTimeMove;
    private float timeMove;



    // Start is called before the first frame update
    void Start()
    {
        
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player.controlType == Player.ControlType.PC && gunType == GunType.Default){
            joystick.gameObject.SetActive(false);
        }    

        timeMove = startTimeMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunType == GunType.Default){
            if(player.controlType == Player.ControlType.PC){
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            else if(player.controlType == Player.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f){
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }
        }
        else if (gunType == GunType.enemy){
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);


        if(timeShots <= 0){
            if(timeMove <= 0){
                if(Input.GetMouseButton(0) && player.controlType == Player.ControlType.PC || gunType == GunType.enemy){
                    Shoot();
                }
                else if(player.controlType == Player.ControlType.Android){
                    if(joystick.Vertical != 0 || joystick.Horizontal != 0){
                        Shoot();
                    }
                }
            }
            else timeMove -= Time.deltaTime;
        }
        else {
            timeShots -= Time.deltaTime;
        }    



        // POBOROT
        angle = GetComponent<Transform>().eulerAngles.z;
        Vector3 scale = transform.localScale;

        if(gunType == GunType.Default) 
        {
            if(Player.poBorot == true){
                if(angle > 0 && angle < 180)  
                {
                    scale.x = -Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
                else 
                {
                    scale.x = Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
            }
            else {
                if(angle > 0 && angle < 180)  
                {
                    scale.x = Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
                else 
                {
                    scale.x = -Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
            }
            
        }
    }

    public void Shoot() {

        timeShots = startTimeShots;
        Instantiate(bull, shotPos.position, shotPos.rotation);
        Instantiate(soundsPistol, transform.position, Quaternion.identity);
       
        camAnim.SetTrigger("tryac");
        
        
    }

    
}
