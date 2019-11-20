/**
 * Author:  Andrew Rudasics
 * Created: 7.7.2019
 * 
 * Abstract class for selectable objects. All selectables should inherit from this
 * class and implement OnSelect
 * 
 **/

using UnityEngine;

public class PointerSelector : MonoBehaviour
{
    [SerializeField]
    private float castDistance = Mathf.Infinity;
    private GameObject selected, hover;
    private bool targeting;
    public DebugUI dbui;

    private int castMask;

    // Start is called before the first frame update
    void Start()
    {
        castMask = 1 << 10;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool mouseDown = Input.GetMouseButtonDown(0);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, castDistance, castMask))
        {
            GameObject temp = hit.collider.gameObject;

            // Selection Behavior
            if (mouseDown)
            {
                // Deselect Currently Selected
                if (selected != null && temp != selected)
                    selected.GetComponent<Selectable>().OnDeselect();
                // Select hovered object
                selected = temp.GetComponent<Selectable>().OnSelect();
                hover = selected;
                dbui.SetSelected(selected);
            }
            else
            {
                // Deselect old hover 
                if (hover != null && hover != temp && temp == selected)
                    hover.GetComponent<Selectable>().OnDeselect();
                // Set hover behavior
                else
                {
                    if (hover == null)
                        hover = temp;

                    // Hover on 
                    if (temp != hover && hover != selected && hover != null)
                        hover.GetComponent<Selectable>().OnDeselect();
                    hover = temp.GetComponent<Selectable>().OnHover();
                }
            }
        }
        else
        {
            if (mouseDown)
            {
                if (selected != null)
                {
                    selected.GetComponent<Selectable>().OnDeselect();
                    selected = null;
                }
            }

            if (hover != null && hover != selected)
            {
                hover.GetComponent<Selectable>().OnDeselect();
                hover = null;
            }
        }
    }
}
