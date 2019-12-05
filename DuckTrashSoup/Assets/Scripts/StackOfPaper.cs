using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOfPaper : MonoBehaviour {

    public static StackOfPaper Instance;
    
    public GameObject paperPrefab;
    public int numWods;

    private List<GameObject> paperWods;
    
    
    void Awake() {
        Instance = this;
        paperWods = new List<GameObject>();
        var player = GameObject.FindWithTag("Player").GetComponent<Collider>();
        
        for (int i = 0; i < numWods; i++) {
            var wod = Instantiate(paperPrefab, this.transform.position, Quaternion.identity);
            var wodCollider = wod.GetComponent<Collider>();
            
            paperWods.Add(wod);
            for (int j = 0; j < i; j++) {
                Physics.IgnoreCollision(wodCollider, paperWods[j].GetComponent<Collider>());
            }
            
        }
    }



    public void PaperWodGotGrabbed(GameObject grabbedWod) {
        paperWods.Remove(grabbedWod);
        paperWods.Add(grabbedWod);

        var resetWod = paperWods[0];
        resetWod.transform.position = this.transform.position;
        resetWod.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
