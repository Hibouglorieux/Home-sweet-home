using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesBox : Item_drag {

    [SerializeField] Matches _matches;

    public override void Activate()
    {
        if (!PlayerInteraction.inst.TryInteract())
        {
            if (GameManager.inst.canAddFire)
                Instantiate(_matches).transform.position = _transform.position;
        }
    }
}
