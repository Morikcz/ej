using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("WALLS")]
    public GameObject[] walls;
    public GameObject wallEffect;
    public GameObject door;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;


    [Header("Bonus")]
    public GameObject shield;
    public GameObject apte4ka;


    public List<GameObject> enemies;

    private roomsVariant variants;
    private bool spawned;
    private bool wallsDestroyed;

    private void Awake() {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<roomsVariant>();

    }

    private void Start() {
        variants.rooms.Add(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !spawned)
        {
            
            foreach(Transform spawner in enemySpawners){
                int rand = Random.Range(0, 11);
                if(rand < 9) {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);

                }
                else if(rand == 9){
                    Instantiate(apte4ka, spawner.position, Quaternion.identity);

                }
                else if(rand == 10){
                    Instantiate(shield, spawner.position, Quaternion.identity);
                }

                StartCoroutine(CheckEnemies());

            }
            spawned = true;
        }
    }

    IEnumerator CheckEnemies() {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }

    public void DestroyWalls() {
        foreach(GameObject wall in walls){
            if(wall != null  && wall.transform.childCount != 0) {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity);
                Destroy(wall);
            }
        }
        wallsDestroyed = true;
    }

    private void OnTriggerStay2D(Collider2D other){
        if(wallsDestroyed && other.CompareTag("wall")){
            Destroy(other.gameObject);
        }
    }
}
