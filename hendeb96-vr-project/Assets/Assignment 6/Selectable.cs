/**
 * Author:  Andrew Rudasics
 * Created: 7.7.2019
 * 
 * Abstract class for selectable objects. All selectables should inherit from this
 * class and implement OnSelect
 * 
 **/

using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    // Defines behavior for object on selection
    public abstract GameObject OnSelect();

    // Defines behavior for object on hover
    public abstract GameObject OnHover();

    // Defines behavior for object on deselection 
    public abstract void OnDeselect();
}
