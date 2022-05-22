using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float forceMultiplier = 1000;

    [SerializeField]
    private float maximumVelocity = 4f;

    [SerializeField]
    private ParticleSystem deathParticles;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

    public Transform checkpoint1;
    public Transform checkpoin2;
    public Transform checkpoint3;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        var horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude <= maximumVelocity)
        {
            rb.AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime, 0, 0));
        }
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0, 0.75f, 0);
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Transform checkpoint;

            Debug.Log(collision.transform.position);
            if (collision.transform.position.x >= -7f && collision.gameObject.transform.position.x <= 7f)
            {
                checkpoint = checkpoint1;
            } 
            else if (collision.transform.position.x >= 18f && collision.gameObject.transform.position.x <= 32f)
            {
                checkpoint = checkpoin2;
            } 
            else
            {
                checkpoint = checkpoint3;
            }

            gameObject.SetActive(false);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();
            GameManager.Instance.SetLife();

            if (GameManager.lifes != 0)
            {
                gameObject.SetActive(true);
                transform.position = checkpoint.position;
                transform.rotation = checkpoint.rotation;
            }
        }
    }
}