using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject hazardPrefab;
    public int maxHazardToSpawn = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazards());
    }

    private IEnumerator SpawnHazards()
    {
        var hazardToSpawn = Random.Range(1, maxHazardToSpawn);

        for (int i = 0; i < hazardToSpawn; i++) 
        {
            var x = Random.Range(-7, 7);
            var drag = Random.Range(1f, 3f);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, 0), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().drag = drag;
        }

        yield return new WaitForSeconds(1f);

        yield return SpawnHazards();
    }

}
