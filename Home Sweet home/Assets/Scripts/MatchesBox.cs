using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesBox : Item_drag {

    [SerializeField] Matches _matches;

    public override void Activate()
    {
        if (!PlayerInteraction.inst.TryInteract())
            Instantiate(_matches).transform.position = _transform.position;
    }
}
