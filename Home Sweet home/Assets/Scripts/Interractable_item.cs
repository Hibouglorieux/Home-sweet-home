using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractable_item : Item {

    public Drag_item needed_item;
    [SerializeField] float _eventFixeTime, _loseTime;

    public void Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
            Debug.Log("used"); //ok
    }

    public void StartEvent()
    {

    }
}
