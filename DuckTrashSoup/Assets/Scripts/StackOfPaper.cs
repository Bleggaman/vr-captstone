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
        for (int i = 0; i < numWods; i++) {
            var wod = Instantiate(paperPrefab, this.transform.position, Quaternion.identity);
            paperWods.Add(wod);
            for (int j = 0; j < i; j++) {
                Physics.IgnoreCollision(wod.GetComponent<Collider>(), paperWods[j].GetComponent<Collider>());
            }
        }
    }



    public void PaperWodGotGrabbed(GameObject grabbedWod) {
        paperWods.Remove(grabbedWod);
        paperWods.Add(grabbedWod);

        paperWods[0].transform.position = this.transform.position;
    }
}
