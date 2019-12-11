using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperWod : OVRGrabbable {
    public AudioSource crumpleSound;
    public AudioSource collisionCrumple;

    public Queue<Vector3> positionsCache;
    private int counter;

    private const int COUNTER_RATE = 3;
    private const int QUEUE_SIZE = 3;
    
    
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
        var positions = EmptyPositionCache();
        Debug.Log("Positions in queue are: " + positions[2] + " -> " + positions[1] + " -> " + positions[0]);
        linearVelocity = positions[0] - positions[1];
        //float angle = Vector3.Angle(positions[0] - positions[2], linearVelocity);
        
        base.GrabEnd(COUNTER_RATE * linearVelocity, angularVelocity);
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
