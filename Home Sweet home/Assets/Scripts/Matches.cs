using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matches : MonoBehaviour {

    [SerializeField] GameObject _fire, _timer;
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
        {
            Transform fire = Instantiate(_fire).transform;
            fire.position = collision.contacts[0].point;
            Transform timer = Instantiate(_timer).transform;
            timer.parent = fire;
            timer.position = fire.position + Vector3.up * 4;
            timer.GetComponent<Timer>().parentType = (int)Item.Interract_item.fire;
        }

        if (_rb.velocity.sqrMagnitude <= .2f)
            Destroy(gameObject);
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }   
}
