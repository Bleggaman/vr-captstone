using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickForce : MonoBehaviour {
    public float strength;
    public float rotateStrength;
    
    void Update() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            var forward = this.transform.rotation * Vector3.forward;
            var click = Camera.main.ScreenPointToRay(Input.mousePosition);
//            Debug.Log("mouse at " + click);
//            Ray beam = new Ray(this.transform.position, forward);
            Debug.DrawRay(click.origin, click.direction, Color.magenta);
            RaycastHit hit;
            //Debug.DrawRay(this.transform.position, forward, Color.red);
            if (Physics.Raycast(click, out hit)) {
                Debug.Log("Hit the " + hit.collider.name);
                var rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.AddForce(Vector3.up * strength);
                    rb.AddTorque(forward * rotateStrength);
                }
            }
        }
    }
}
