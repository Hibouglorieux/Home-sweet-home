using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_drag : Item {

    public Drag_item id;

    Rigidbody _rb;
    Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Drag(Transform parent, Vector3 position)
    {
        _transform.parent = parent;
        _transform.localPosition = position;
        _rb.isKinematic = true;
    }

    public void Drop()
    {
        _transform.parent = null;
        _rb.isKinematic = false;
    }
}
