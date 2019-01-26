using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Transform _grabbedPosition;
    [SerializeField] Transform _grabCenter;

    [SerializeField] Item_drag _draggedItem;
    [SerializeField] LayerMask _grabLayer, _interactLayer;

    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            bool grabNow = false;
            if (_draggedItem == null)
                grabNow = TryGrab();
            else _draggedItem.Activate();
            //if (!grabNow)
                //TryInteract();
        }
        else if (Input.GetKeyUp("joystick button 0"))
        {
            if (_draggedItem != null)
                _draggedItem.Desactivate();
        }
        else if (Input.GetKeyDown("joystick button 1") && _draggedItem != null)
            Drop();

        if (_draggedItem != null)
            _draggedItem.SetPositionRotation(_grabbedPosition.position, _grabbedPosition.rotation);
    }

    bool TryGrab()
    {
        Collider[] cols = Physics.OverlapSphere(_grabCenter.position, .8f, _grabLayer);
        if (cols.Length <= 0) return false;

        _draggedItem = cols[0].GetComponentInParent<Item_drag>();
        _draggedItem.Drag(_grabbedPosition, Vector3.zero);

        return true;
    }

    bool TryInteract()
    {
        Collider[] cols = Physics.OverlapSphere(_grabCenter.position, .8f, _interactLayer);
        if (cols.Length <= 0) return false;

        Interractable_item iteract = cols[0].GetComponent<Interractable_item>();

        iteract.Launch_event(_draggedItem != null ? _draggedItem.id : Item.Drag_item.none);

        return true;
    }

    void Drop()
    {
        _draggedItem.Drop(GetComponent<Rigidbody>().velocity);
        _draggedItem = null;
    }
}