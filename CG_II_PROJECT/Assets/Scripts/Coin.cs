using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem breakingEffect;

    public AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(clip, gameObject.transform.position, 1.5f);

            GameManager.Instance.SetScore();
            Destroy(gameObject);
            //Instantiate(breakingEffect, transform.position, Quaternion.identity);
        }
        else if (!collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(gameObject);
            //Instantiate(breakingEffect, transform.position, Quaternion.identity);
        }
    }
}
