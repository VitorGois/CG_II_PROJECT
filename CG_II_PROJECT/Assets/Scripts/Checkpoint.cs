using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform checkpoint;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            GameManager.Instance.SetLife();

            if (GameManager.lifes != 0)
            {
                player.transform.position = checkpoint.position;
                player.transform.rotation = checkpoint.rotation;
            }            
        }
    }
}
