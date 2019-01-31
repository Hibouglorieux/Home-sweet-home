using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Interractable_item {


    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            //Debug.Log("used"); //ok
            SucceedSound.Succeed();
            PlayerInteraction.inst.Detroy_held_obj();
            return true;
        }
        return false;
    }
}
