using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupPickUp : OVRGrabbable
{
    public Animator turn;
    public Animator turnLeft;
	
	public ParticleSystem ps;
	public Light roomLight;
	
	public Color lit;
	public Color alert;

    private void Start()
    {
        turn.SetBool("soupPickedUp", true);
        turnLeft.SetBool("soupPickedUp", true);
        Debug.Log("soupPickedUp is true in beginning");
    }
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
		// Stop the soup particles, turn the lights red, play them bad tunes, rotate heads
		ps.Stop();
		roomLight.color = alert;
        turn.SetBool("soupPickedUp", true);
        turnLeft.SetBool("soupPickedUp", true);
        Debug.Log("soupPickedUp is true in GrabBegin");
    }
	
	public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity) {
		base.GrabEnd(linearVelocity, angularVelocity);
		
		
		// Play the soup particles, turn the lights white, stop them bad tunes, rotate heads
		ps.Play();
		roomLight.color = lit;
	}

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
        }
    }
}
