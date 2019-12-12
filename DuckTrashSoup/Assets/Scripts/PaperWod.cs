using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperWod : OVRGrabbable {
    public AudioSource crumpleSound;
    public AudioSource collisionCrumple;

    public Queue<Vector3> positionsCache;
    private int counter;

    private const int COUNTER_RATE = 2;
    private const int QUEUE_SIZE = 3;
	private const float VELOCITY_MAX = 22;
	private const float VELOCITY_MIN = 12;
	private const float VELOCITY_THROW_THRESHOLD = 8;

    public float throwMultiplier;
	
	// This indicates for how many frames after the release of the wad that it will follow the hand still
	public int directionBufferLength;
    
    
    protected override void Start() {
        base.Start();
        positionsCache = new Queue<Vector3>();
        for (int i = 0; i < QUEUE_SIZE; i++) {
            positionsCache.Enqueue(Vector3.zero);
        }
    }

    

    private void Update() {
        // If grabbed, queue the position every 3rd frame
        if (this.isGrabbed && ++counter % COUNTER_RATE == 0) {
            positionsCache.Dequeue();
            positionsCache.Enqueue(this.transform.position);
        }
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        StackOfPaper.Instance.PaperWodGotGrabbed(this.gameObject);
        crumpleSound.Play();
        
        positionsCache.Enqueue(this.transform.position);
        positionsCache.Dequeue();
    }

    private void OnCollisionEnter(Collision other) {
        collisionCrumple.Play();

        if (other.gameObject.tag == "Player") {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity) {
        StartCoroutine(DelayRelease(linearVelocity, angularVelocity));
    }
	
	private IEnumerator DelayRelease(Vector3 linearVelocity, Vector3 angularVelocity) {
		for (int i = 0; i < this.directionBufferLength; i++)
			yield return new WaitForEndOfFrame();
		
		base.GrabEnd(linearVelocity, angularVelocity);
        
        // Determine velocity at time of release
        var positions = EmptyPositionCache();
        linearVelocity = positions[0] - positions[1];
        for (int i = 1; i < QUEUE_SIZE; i++) {
            linearVelocity += positions[i - 1] - positions[i];
        }
        linearVelocity /= QUEUE_SIZE - 1;
        
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        linearVelocity = linearVelocity * (1 / Time.deltaTime) / COUNTER_RATE * throwMultiplier;
		float preMagnitude = linearVelocity.magnitude;
		Debug.Log("Pre velocity magnitude on release: " + preMagnitude);
		
		if (preMagnitude > VELOCITY_MAX) {
			linearVelocity *= VELOCITY_MAX / preMagnitude;
		} else if (preMagnitude < VELOCITY_MIN && preMagnitude > VELOCITY_THROW_THRESHOLD) {
			linearVelocity *= VELOCITY_MIN / preMagnitude;
		}
		
		
		Debug.Log("      Velocity magnitude on release: " + linearVelocity.magnitude);
		rb.velocity = linearVelocity;
	}
    
    // Returns a list of the most recent QUEUE_SIZE positions. Positions at front of list are more recent
    private List<Vector3> EmptyPositionCache() {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < QUEUE_SIZE; i++) {
            positions.Insert(0, positionsCache.Dequeue());
            positionsCache.Enqueue(Vector3.zero);
        }

        return positions;
        
    }
}
