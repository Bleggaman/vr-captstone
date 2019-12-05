using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        DoorTransport door = other.GetComponent<DoorTransport>();
        if (door == null) return;
        
        door.LightDoor();
    }
    
    private void OnTriggerExit(Collider other) {
        DoorTransport door = other.GetComponent<DoorTransport>();
        if (door == null) return;
        
        door.UnlightDoor();
    }
}
