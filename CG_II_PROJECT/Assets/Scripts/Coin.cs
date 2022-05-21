using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    

    [SerializeField]
    private ParticleSystem breakingEffect;

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            GameManager.gm.SetCoins();
            Destroy(gameObject); 
            Instantiate(breakingEffect,transform.position, Quaternion.identity);

        } else if (!collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(gameObject); 
            Instantiate(breakingEffect,transform.position, Quaternion.identity);
        }}
  

}
