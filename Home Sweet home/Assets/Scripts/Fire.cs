using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    [SerializeField] float _maxLifeTime = 2, _healTime = .1f;
    float _time;

    private void Update()
    {
        _time += _healTime * Time.deltaTime;
        if (_time > _maxLifeTime)
            _time = _maxLifeTime;
    }

    public void Attack()
    {
        _time -= _healTime * Time.deltaTime * 3;
        if (_time <= 0)
            Destroy(gameObject);
    }
}
