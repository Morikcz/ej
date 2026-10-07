using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingDamage : MonoBehaviour
{
    [HideInInspector] public float damage;

    private TextMesh textMesh;


    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = "-" + damage;
        damage = 0;
    }


    private void OnAnimationOver() {
        Destroy(gameObject);

    }

    
}
