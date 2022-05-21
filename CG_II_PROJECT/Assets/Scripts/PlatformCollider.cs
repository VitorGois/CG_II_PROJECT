using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{

    public GameObject Player;
    Animator anim;

  
    void Update()
    {
         if (tag == "Player") 
         {
             anim.SetTrigger("PlatformMove"); }
    }


/*
    public GameObject Player;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider collision) 
     {
         if (collision.tag == "Player") 
         {
             Player.transform = transform; 
             Player.transform.localScale = new Vector3(1F, 1F, 1F);
         }
 }
 private void OnTriggerExit(Collider collision) 
     {
         if (collision.tag == "Player") 
         {
             Player.transform.parent = null; 
             Player.transform.localScale = new Vector3(1F, 1F, 1F);

         }
     }*/
}
