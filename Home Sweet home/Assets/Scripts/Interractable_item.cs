using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractable_item : Item {

    public Drag_item needed_item;
    public GameObject timer;
    [SerializeField] protected float duration_of_event;
    public Timer timer_script = null;

    public virtual bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            Debug.Log("used"); //ok
            if (timer_script)
                Destroy(timer_script.gameObject);
            timer_script = null;
            End_event();
            return true;
        }
        return false;
    }

    public virtual void End_event()
    {
    }
    public virtual void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3 (transform.position.x, 4,transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
    }

    public virtual void Animation()
    {
    }
}
