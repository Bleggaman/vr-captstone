using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnView : MonoBehaviour {

    public Transform subject;
    public float lowerWindow;
    public float upperWindow;
    private bool rotated = false;
    
    // Update is called once per frame
    void Update() {
        if (!this.rotated && this.InView()) {
            this.rotated = true;
            Debug.Log("Rotated open");
        } else if (this.rotated && !this.InView()) {
            this.rotated = false;
            Debug.Log("Rotated closed");
        }
    }


    private bool InView() {
        return this.subject.rotation.eulerAngles.y > lowerWindow && this.subject.rotation.eulerAngles.y < upperWindow;
    }
}
