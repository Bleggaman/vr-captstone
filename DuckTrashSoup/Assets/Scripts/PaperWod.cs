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
    private const int QUEUE_SIZE = 4;
    
    
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
        base.GrabEnd(linearVelocity, angularVelocity);
        
        // Determine velocity at time of release
        var positions = EmptyPositionCache();
        linearVelocity = positions[0] - positions[1];
        for (int i = 1; i < QUEUE_SIZE; i++) {
            linearVelocity += positions[i - 1] - positions[i];
        }
        linearVelocity /= QUEUE_SIZE - 1;
        
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = linearVelocity * (1 / Time.deltaTime) / COUNTER_RATE;
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
