using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : Item_drag {

    [SerializeField] ParticleSystem _particles;
    [SerializeField] Transform _raypoint;
    [SerializeField] float _distance;
    [SerializeField] LayerMask _layer;
    [SerializeField] float _minDst = .1f;

    void Update()
    {
        if (active)
        {
            bool wall = false;
            RaycastHit[] hits = Physics.RaycastAll(_raypoint.position, _raypoint.forward, _distance, _layer);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Flame"))
                {
                    hits[i].collider.GetComponent<Fire>().Attack();
                }
                else if (hits[i].collider.CompareTag("Wall") && (hits[i].point - _raypoint.position).sqrMagnitude < _minDst)
                {
                    print("in");
                    wall = true;
                    _particles.Stop();
                }
            }

            if (!wall && _particles.isStopped)
            {
                _particles.Play();
                print("in_2");
            }
        }
    }

    public override void Activate()
    {
        base.Activate();
        _particles.Play();
    }
    public override void Desactivate()
    {
        base.Desactivate();
        _particles.Stop();
    }
}
