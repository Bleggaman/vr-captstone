using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBasket : MonoBehaviour {

    public AudioSource spaceJam;
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("paperWad")) return;
        
        // TODO: throw confetti
        
        this.spaceJam.Play();
    }
}
