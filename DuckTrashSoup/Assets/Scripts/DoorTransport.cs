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
	
	public OVRScreenFade fade;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        StartCoroutine(Transition());
    }

	private IEnumerator Transition() {
		fade.FadeOut();
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(targetRoom);
	}

    public void LightDoor() {
        this.door.material = lit;
    }

    public void UnlightDoor() {
        this.door.material = unlit;
    }
}
