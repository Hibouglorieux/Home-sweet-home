using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : Item_drag {

    [SerializeField] ParticleSystem _particles;
    [SerializeField] Transform[] _raypoints;
    [SerializeField] float _distance;
    [SerializeField] LayerMask _layer;
    [SerializeField] float _minDst = .1f;

    void Update()
    {
        if (active)
        {
            bool wall = false;
            RaycastHit[] hits = Physics.RaycastAll(_raypoints[0].position, _raypoints[0].forward, _distance, _layer);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Flame"))
                {
                    hits[i].collider.GetComponent<Fire>().Attack();
                }
                else if (hits[i].collider.CompareTag("Wall") && (hits[i].point - _raypoints[0].position).sqrMagnitude < _minDst)
                {
                    wall = true;
                    _particles.Stop();
                }
            }

            hits = Physics.RaycastAll(_raypoints[1].position, _raypoints[1].forward, _distance, _layer);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Flame"))
                {
                    hits[i].collider.GetComponent<Fire>().Attack();
                }
                else if (hits[i].collider.CompareTag("Wall") && (hits[i].point - _raypoints[1].position).sqrMagnitude < _minDst)
                {
                    wall = true;
                    _particles.Stop();
                }
            }

            if (!wall && _particles.isStopped)
            {
                _particles.Play();
            }
        }
    }

    public override void Activate()
    {
        //base.Activate();
        active = true;
        _particles.Play();
    }
    public override void Desactivate()
    {
        base.Desactivate();
        _particles.Stop();
    }
}
