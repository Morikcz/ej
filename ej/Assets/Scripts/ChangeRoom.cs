using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    private Vector3 roomOffset;

    void Start() {
        cam = Camera.main.GetComponent<Camera>();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += playerChangePos;
            roomOffset += cameraChangePos; // запоминаем смещение
        }
    }

    void LateUpdate()
    {
        cam.transform.position += roomOffset; // применяем каждый кадр ПОСЛЕ аниматора
    }
}
