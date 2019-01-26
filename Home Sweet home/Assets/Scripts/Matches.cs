using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matches : MonoBehaviour {

    [SerializeField] GameObject _fire;
    Rigidbody _rb;

    float _lifeTime = 4f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitToDestroy(_lifeTime));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (GameManager.inst.canAddFire)
            Instantiate(_fire).transform.position = collision.contacts[0].point;

        if (_rb.velocity.sqrMagnitude <= .2f)
            Destroy(gameObject);
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }   
}
