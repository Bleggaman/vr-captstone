using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioLoop : MonoBehaviour
{
    public AudioSource ambient1, ambient2;

    // Start is called before the first frame update
    void Start()
    {
        ambient1.time = 0;
        ambient2.time = 8;
        ambient1.Play();
        ambient2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
