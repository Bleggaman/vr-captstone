using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckQuack : OVRGrabbable
{
    public AudioSource quack;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        quack.Play();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
        }
    } 
    
}
