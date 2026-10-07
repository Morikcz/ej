using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_noj_attack : MonoBehaviour
{
    public float startAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    public Animator anim;
    public GameObject effect;
    public GameObject sound;
    
    public Joystick joystick;

    private Enemy enemyes;
    private Player player;
    private float timeAttack;
    private Vector2 moveInput;
    private WeaponSwitch weapons;


    private void Start() {
        weapons = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitch>();
        enemyes = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }

    private void Update() {
        if(timeAttack <= 0){
            if(weapons.weapon == 1){
                if(player.controlType == Player.ControlType.Android){
                    if(joystick.Vertical != 0 || joystick.Horizontal != 0){
                        anim.SetTrigger("attack");
                    }
                }  
                if(Input.GetMouseButton(0) && player.controlType == Player.ControlType.PC){
                    anim.SetTrigger("attack");
                    
                }
                     
            }

            timeAttack = startAttack;
        }
        else 
        {
            timeAttack -= Time.deltaTime;
        }


    }

    public void OnAttackNoj() {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for(int i = 0; i < enemies.Length; i++){
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            Instantiate(sound, transform.position, Quaternion.identity);
            Instantiate(effect, enemyes.transform.position, Quaternion.identity);
            /*if(Player.poBorot) {
                Instantiate(effect, new Vector3(transform.position.x +1.6f, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else {
                Instantiate(effect, new Vector3(transform.position.x -1.6f, transform.position.y, transform.position.z), Quaternion.identity);
            }*/
            
        }
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


}
