using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matches : MonoBehaviour {

    [SerializeField] GameObject _fire;
    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(_fire).transform.position = collision.contacts[0].point;

        if (_rb.velocity.sqrMagnitude <= .1f)
            Destroy(gameObject);
    }

    /*public override void Activate()
    {
        base.Activate();
        _particles.Play();
    }
    public override void Desactivate()
    {
        base.Desactivate();
        GetComponent<Rigidbody>().isKinematic = false;
        PlayerInteraction.inst.Drop();
    }*/
}
