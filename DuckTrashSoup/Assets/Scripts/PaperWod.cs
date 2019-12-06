using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperWod : OVRGrabbable {
    public AudioSource crumpleSound;
    public AudioSource collisionCrumple;

    public Queue<>
    
    
    protected override void Start() {
        base.Start();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        StackOfPaper.Instance.PaperWodGotGrabbed(this.gameObject);
        crumpleSound.Play();
    }

    private void OnCollisionEnter(Collision other) {
        collisionCrumple.Play();

        if (other.gameObject.tag == "Player") {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity) {
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
