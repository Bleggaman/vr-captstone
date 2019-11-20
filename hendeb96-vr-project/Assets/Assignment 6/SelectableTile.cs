/**
 * Author:  Andrew Rudasics
 * Created: 7.7.2019
 * 
 * Abstract class for selectable objects. All selectables should inherit from this
 * class and implement OnSelect
 * 
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableTile : Selectable
{
    [SerializeField]
    private Material def, hover, selected;
    private bool isSelected;

    public override GameObject OnSelect()
    {
        Debug.Log("Selected");
        isSelected = true;
        gameObject.GetComponent<MeshRenderer>().material = selected;
        return this.gameObject;
    }

    public override GameObject OnHover()
    {
        if (gameObject.GetComponent<MeshRenderer>().material != hover && !isSelected)
        {
            gameObject.GetComponent<MeshRenderer>().material = hover;
        }
        return this.gameObject;
    }

    public override void OnDeselect()
    {
        gameObject.GetComponent<MeshRenderer>().material = def;
        isSelected = false;
    }
}