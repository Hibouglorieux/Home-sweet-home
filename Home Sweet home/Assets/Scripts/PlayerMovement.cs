using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 24, _maxMoveSpeed = 8, _rotationSpeed = 100;
    float _horizontal = 0, _vertical = 0;

    [SerializeField] Animator _anim;

    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.inst.states != GameManager.GameStates.playing)
            return;

        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (GameManager.inst.states != GameManager.GameStates.playing)
            return;

        if (Mathf.Abs(_horizontal) > 0 || Mathf.Abs(_vertical) > 0)
        {
            //Rotation
            float angle = 180 - Mathf.Atan2(-_vertical, -_horizontal) * 180 / Mathf.PI + 90;
            Quaternion targetRota = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRota, Time.fixedDeltaTime * _rotationSpeed);

            //Movement
            //_rb.velocity = Vector3.zero;
            _rb.AddForce(transform.forward * _moveSpeed, ForceMode.Force);

        }

        _anim.SetFloat("Speed", new Vector2(_rb.velocity.x, _rb.velocity.z).sqrMagnitude);
    }
}
