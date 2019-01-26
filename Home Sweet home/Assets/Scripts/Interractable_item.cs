using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractable_item : Item {

    public Drag_item needed_item;
    [SerializeField] float _eventFixeTime, _loseTime;
    public GameObject timer;
    [SerializeField] float duration_of_event;
    private Timer real_timer = null;

    public void Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            Debug.Log("used"); //ok
            if (real_timer)
                Destroy(real_timer);
            real_timer = null;
        }
    }

    public void StartEvent()
    {
        real_timer = Instantiate(timer, new Vector3 (transform.position.x, 4,transform.position.z), new Quaternion()).GetComponent<Timer>();
        real_timer.max_timer = duration_of_event;
    }
}
