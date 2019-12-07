using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckQuack : MonoBehaviour
{
    public AudioSource quack;

    private void OnCollisionEnter(Collision other)
    {
        quack.Play();

        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
        }
    } 
    
}
