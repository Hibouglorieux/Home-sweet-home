using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 24, _maxMoveSpeed = 8, _rotationSpeed = 100;
    float _horizontal = 0, _vertical = 0;

    [SerializeField] Transform _grabCenter;

    [SerializeField] Item _draggedItem;
    [SerializeField] LayerMask _grabLayer, _interactLayer;

    Rigidbody _rb;

    public Vector3 vc;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (_draggedItem == null)
            TryGrab();
        else TryInteract();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(_horizontal) > 0 || Mathf.Abs(_vertical) > 0)
        {
            //Rotation
            float angle = 180 - Mathf.Atan2(-_vertical, -_horizontal) * 180 / Mathf.PI + 90;
            Quaternion targetRota = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRota, Time.deltaTime * _rotationSpeed);

            //Movement
            //_rb.velocity = Vector3.zero;
            _rb.AddForce(transform.forward * _moveSpeed, ForceMode.Force);
        }
    }

    bool TryGrab()
    {
        Collider[] cols = Physics.OverlapSphere(_grabCenter.position, .8f, _grabLayer);
        if (cols.Length <= 0) return false;

        _draggedItem = cols[0].GetComponent<Item>();

        return true;
    }

    bool TryInteract()
    {
        Collider[] cols = Physics.OverlapSphere(_grabCenter.position, .8f, _interactLayer);
        if (cols.Length <= 0) return false;


        return true;
    }
}
