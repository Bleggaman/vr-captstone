using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransport : OVRGrabbable {

    public string targetRoom;
    public MeshRenderer door;
    public Material lit;
    public Material unlit;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        SceneManager.LoadScene(targetRoom);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) return;

        this.door.material = lit;
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) return;

        this.door.material = unlit;
    }
}
