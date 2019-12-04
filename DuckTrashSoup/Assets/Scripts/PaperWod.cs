using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperWod : OVRGrabbable {
    public AudioSource crumpleSound;
    public AudioSource collisionCrumple;
    
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        StackOfPaper.Instance.PaperWodGotGrabbed(this.gameObject);
        crumpleSound.Play();
    }

    private void OnCollisionEnter(Collision other) {
        collisionCrumple.Play();
    }
}
