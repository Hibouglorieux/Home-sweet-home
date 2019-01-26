using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_drag : Item {

    public Drag_item id;
    public bool active;

    protected Transform _transform;
    protected Rigidbody _rb;
    protected Collider _collider;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Drag(Transform parent, Vector3 position)
    {
        //_transform.parent = parent;
        //_transform.localPosition = position;
        _rb.isKinematic = true;
        _collider.enabled = false;
    }

    public void Drop(Vector3 velocity)
    {
        //_transform.parent = null;
        _collider.enabled = true;
        _rb.isKinematic = false;
        _rb.velocity = velocity;
    }

    public void SetPositionRotation(Vector3 position, Quaternion rotation)
    {
        _transform.position = position;
        _transform.rotation = rotation;
    }

    public virtual void Activate()
    {
        active = true;
    }
    public virtual void Desactivate()
    {
        active = false;
    }
}
