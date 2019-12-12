using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEyesTurn : MonoBehaviour
{
    public Animator anim;
    public bool soupPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("soupPickedUp", soupPickedUp);
    }
}
