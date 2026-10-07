using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buller : MonoBehaviour
{

    public float speed;
    public float lifetime;
    public float distance;
    public int minDamage;
    public int maxDamage;
    public LayerMask whatIsSolid;
    public GameObject playerDestroyEffect;
    public GameObject wallDestroyEffect;
    public GameObject blockDestroyEffect;
    private int randDamage;

    [SerializeField] bool enemyBull;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifetime);
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        int rand = Random.Range(0, 21);
        if(rand <= 19){
            randDamage = Random.Range(minDamage, maxDamage);
        }
        else {
            randDamage = Random.Range(maxDamage, maxDamage + 5);
        }


        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(randDamage);
            }
            if(hitInfo.collider.CompareTag("Player") && enemyBull)
            {
                Instantiate(playerDestroyEffect, transform.position, Quaternion.identity);
                hitInfo.collider.GetComponent<Player>().ChangeHealth(-randDamage);
            }
            if(hitInfo.collider.CompareTag("Block")){
                Instantiate(blockDestroyEffect, transform.position, Quaternion.identity);
            }
            if(hitInfo.collider.CompareTag("wall")){
                Instantiate(wallDestroyEffect, transform.position, Quaternion.identity);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed *Time.deltaTime);
    }

    public void DestroyBullet() 
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
