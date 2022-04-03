using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{

    [SerializeField]
    private float forceMultiplier = 25f;

    [SerializeField]
    private float maximumVelocity = 3.5f;

    [SerializeField]
    private ParticleSystem deathParticles;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource; 
    
    // Start is called before the first frame update
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
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();
        } 
    }
}
