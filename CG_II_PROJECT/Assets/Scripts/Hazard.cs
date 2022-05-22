using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Hazard : MonoBehaviour
{
    Vector3 rotation;

    [SerializeField]
    private ParticleSystem breakingEffect;

    private CinemachineImpulseSource cinemachineImpulseSource; 
    private Player player;

    public AudioClip clip;

    private void Start() {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindObjectOfType<Player>();

        var xRotation = Random.Range(90f, 180f);
        rotation = new Vector3(-xRotation, 0);

        gameObject.SetActive(true);
    }

    private void Update() {
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (!collision.gameObject.CompareTag("Hazard") && !collision.gameObject.CompareTag("Coin"))
        {
            AudioSource.PlayClipAtPoint(clip, gameObject.transform.position, 0.8f);

            Destroy(gameObject);
            Instantiate(breakingEffect, transform.position, Quaternion.identity);

            if (player != null)
            {
                var distance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1f / distance;

                cinemachineImpulseSource.GenerateImpulse(force);  
            }
        }
    }

}
