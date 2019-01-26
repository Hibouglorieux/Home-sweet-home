using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    [SerializeField] float _maxLifeTime = 2, _healTime = .1f;
    float _time;

    float _startScale = .1f;
    Transform _child;

    [HideInInspector] public GazCooker _creator;

    void Start()
    {
        GameManager.inst.fireCount++;
        _child = transform.GetChild(0);
        _startScale = _child.localScale.x;
        _child.localScale = Vector3.one * _startScale * .1f;
    }

    void Update()
    {
        _time += _healTime * Time.deltaTime;
        if (_time > _maxLifeTime)
            _time = _maxLifeTime;

        _child.localScale = Vector3.one * Mathf.Lerp(_startScale * .1f, _startScale, _time / _maxLifeTime);
    }

    public void Attack()
    {
        _time -= _healTime * Time.deltaTime * 2.5f;
        if (_time <= 0)
        {
            GameManager.inst.fireCount--;
            if (GameManager.inst.fireCount < 0)
                GameManager.inst.fireCount = 0;
            if (_creator != null)
                _creator.FireDie();
            Destroy(gameObject);
        }
    }
}
