﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupPickUp : OVRGrabbable
{
    public Animator turn;
    public Animator turnLeft;

    private void Start()
    {
        turn.SetBool("soupPickedUp", true);
        turnLeft.SetBool("soupPickedUp", true);
        Debug.Log("soupPickedUp is true in beginning");
    }
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        turn.SetBool("soupPickedUp", true);
        turnLeft.SetBool("soupPickedUp", true);
        Debug.Log("soupPickedUp is true in GrabBegin");
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
        }
    }
}
