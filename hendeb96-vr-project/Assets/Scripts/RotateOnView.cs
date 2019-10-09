using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnView : MonoBehaviour {

    public Transform subject;
    public Animator anim;

    public float lowerWindow;
    public float upperWindow;
    public float rotationDuration;
    public float closedRotation = 0;
    public float openRotation = 90;
    private bool rotated = false;
    private IEnumerator rotation;
    
    // Update is called once per frame
    void Update() {
        if (!this.rotated && this.InView()) {
            this.rotated = true;
            Debug.Log("Rotated open");
            anim.SetBool("character_nearby", true);
            //this.rotation = Rotate(this.openRotation, this.closedRotation);
            //StartCoroutine(this.rotation);
        } else if (this.rotated && !this.InView() && this.rotation == null) {
            this.rotated = false;
            Debug.Log("Rotated closed");
            anim.SetBool("character_nearby", false);
            //this.rotation = Rotate(this.closedRotation, this.openRotation);
            //StartCoroutine(this.rotation);
        }
        //Debug.Log("\tcurrent Rotation: " + this.transform.rotation.eulerAngles);
    }


    private bool InView() {
        return CurrYRoation() > lowerWindow && CurrYRoation() < upperWindow;
    }

    private float CurrYRoation()
    {
        return this.subject.rotation.eulerAngles.y;
    }


    private IEnumerator Rotate(float targetRotation, float baseRotation) {
        Vector3 eulerRotation = this.transform.rotation.eulerAngles;
        float distanceToRotate = targetRotation - eulerRotation.y;
        float maxDistanceToRotate = targetRotation - baseRotation;
        float percentRotated = distanceToRotate / maxDistanceToRotate;
        float distancePerFrame = maxDistanceToRotate / rotationDuration;

        Debug.Log("Initial Rotation percent: " + percentRotated);
        while (Mathf.Abs(percentRotated) < 0.95) {
            // Update rotation
            eulerRotation.y += distancePerFrame * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(eulerRotation);

            // Update tracking
            distanceToRotate = targetRotation - eulerRotation.y;
            percentRotated = distanceToRotate / maxDistanceToRotate;

            // Wait a frame
            Debug.Log("----------------------");
            Debug.Log("\tcurrent Rotation percent: " + percentRotated);
            Debug.Log("\tcurrent Rotation: " + this.transform.rotation.eulerAngles);
            yield return new WaitForEndOfFrame();
        }
        eulerRotation.y = targetRotation;
        this.transform.rotation = Quaternion.Euler(eulerRotation);
        this.transform.Rotate(new Vector3(0, maxDistanceToRotate, 0));
        this.rotation = null;
    }
}
