using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger_Island2 : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            GameManager.spawnRangeLeft = 18f;
            GameManager.spawnRangeRight = 32f;
            GameManager.maxHazardToSpawn = 3;
            GameManager.maxCoinToSpawn = 2;
            GameManager.spawnDrag = 2f;
            //gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
