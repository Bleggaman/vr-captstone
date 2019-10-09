using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlegText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int framesPerUpdate = 30;
    private int counter = 0;
    private int bIndex = 0;
    private char[] bleg = new char[22];

    // Update is called once per frame
    void Update() {
        if(++counter % framesPerUpdate == 0) {
            UpdateBleg();
        }
    }

    private void UpdateBleg() {
        // Update the letters: b-> . | l -> b | e -> l | g -> e | . -> g
        bleg[bIndex % 22] = '.';
        bleg[(bIndex + 1) % 22] = 'B';
        bleg[(bIndex + 2) % 22] = 'l';
        bleg[(bIndex + 3) % 22] = 'e';
        bleg[(bIndex + 4) % 22] = 'g';
        bIndex++;

        text.text = new string(bleg);
    }
}
