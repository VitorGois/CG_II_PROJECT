using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttacher1 : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            anim.Play("Move_Platform_1");

            go.transform.parent = transform;
            go.transform.localScale = new Vector3(0.3f, 2f, 0.25f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            go.transform.rotation = Quaternion.Euler(0, 0, 0); // So that the character doesn't bug the scales when on the platform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        if (other.gameObject.CompareTag("Player"))
        {
            go.transform.parent = null;
            go.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
