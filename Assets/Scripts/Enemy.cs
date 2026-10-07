using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    public GameObject floatingDamage;

    private float timeAttack;
    public float startTimeAttack;

    public int health;
    public float speed;
    public int damage;  
    public GameObject deathEffect;
    public Transform attackPos;
    public LayerMask Player;
    public float attackRange;
    public GameObject anglesNullObject;

    public float startTimeMove;
    private float timeMove;

    private AddRoom room;
    private Player player;
    private Animator anim;
    private Animator camAnim;

    private void Start() {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        room = GetComponentInParent<AddRoom>();
        timeMove = startTimeMove;
    }
    
    private void Update()
    {
        if(health <= 0){
            Destroy(gameObject);
            room.enemies.Remove(gameObject);
        }

        if(player.transform.position.x > transform.position.x){
            transform.eulerAngles = new Vector3(0, 180, 0);   
        }   
        else{
            transform.eulerAngles = new Vector3(0, 0, 0);   
        }
                

        if(timeMove <= 0){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else timeMove -= Time.deltaTime;

    }

    public void TakeDamage(int damage)
    {        
        health -= damage;
        Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 2.56f);
        Instantiate(floatingDamage, damagePos, Quaternion.identity);
        floatingDamage.GetComponentInChildren<floatingDamage>().damage = damage;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            if(timeAttack <= 0){
                if(timeMove <= 0){
                    anim.SetTrigger("OYA_attack");
                }
                else timeMove -= Time.deltaTime;
            }
            else {
                timeAttack -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyAttack() {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPos.position, attackRange, Player);
        for(int i = 0; i < players.Length; i++){
            players[i].GetComponent<Player>().ChangeHealth(-damage);
            Instantiate(deathEffect, player.transform.position, Quaternion.identity);
            timeAttack = startTimeAttack;
            camAnim.SetTrigger("tryac");
        }
        
        //player.health -= damage;
        
    }
}
