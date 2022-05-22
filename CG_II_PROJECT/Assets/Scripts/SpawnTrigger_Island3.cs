using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger_Island3 : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            GameManager.spawnRangeLeft = 43f;
            GameManager.spawnRangeRight = 57f;
            GameManager.maxHazardToSpawn = 4;
            GameManager.maxCoinToSpawn = 3;
            GameManager.spawnDrag = 2f;
            //gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
