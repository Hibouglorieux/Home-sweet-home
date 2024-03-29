﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction inst;

    [SerializeField] Transform _grabbedPosition;
    [SerializeField] Transform _grabCenter;

    Item_drag _draggedItem;
    [SerializeField] LayerMask _grabLayer, _interactLayer;

    [SerializeField] float _grabRadius = 1;

    Animator _anim;

    [SerializeField] AudioClip[] _dragDrop;
    [SerializeField] AudioSource _source;

    private void Awake()
    {
        inst = this;
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (GameManager.inst.states != GameManager.GameStates.playing)
            return;

        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.O))
        {
            /*bool grabNow = false;
            if (_draggedItem == null)
                grabNow = TryGrab();
            else _draggedItem.Activate();*/
            //if (!grabNow)
                //TryInteract();

            if (_draggedItem != null)
                _draggedItem.Activate();
            else TryInteract();
        }
        else if (Input.GetKeyUp("joystick button 0") || Input.GetKeyUp(KeyCode.O))
        {
            if (_draggedItem != null)
                _draggedItem.Desactivate();
            else TryGrab();
        }
        else if ((Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.P)) && _draggedItem != null)
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
        _anim.SetTrigger("Grab");

        _source.clip = _dragDrop[0];
        _source.Play();

        return true;
    }

    public bool TryInteract()
    {
        Collider[] cols = Physics.OverlapSphere(_grabCenter.position, _grabRadius, _interactLayer);
        if (cols.Length <= 0) return false;

        Interractable_item iteract = cols[0].GetComponent<Interractable_item>();
        bool validation = iteract.Launch_event(_draggedItem != null ? _draggedItem.id : Item.Drag_item.none);
        if (validation)
            ;//iteract.DoAction

        return validation;
    }

    public void Drop()
    {
        _source.clip = _dragDrop[1];
        _source.Play();

        _draggedItem.Drop(GetComponent<Rigidbody>().velocity);
        _draggedItem = null;

        _anim.SetTrigger("Drop");
    }

    public void Detroy_held_obj()
    {
        Destroy(_draggedItem.gameObject);
        _draggedItem = null;

        _anim.SetTrigger("Drop");
    }
}