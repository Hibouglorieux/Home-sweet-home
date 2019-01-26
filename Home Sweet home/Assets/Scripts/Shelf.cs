using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : Interractable_item {

    [SerializeField] Transform child;
    private bool ini = false;
    // Use this for initialization
    private void Awake()
    {
        needed_item = Drag_item.hammer;
        duration_of_event = 20f;
    }
    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            Debug.Log("used"); //ok
                               // ajouter son
            if (child.rotation.x < -0.038f)
                child.eulerAngles += new Vector3(7, 0, 0);
            if (timer_script && (timer_script.actual_timer -= timer_script.max_timer / 15) < 0)
            {
                Destroy(timer_script.gameObject);
                timer_script = null;
                End_event();
                return true;
            }
        }
        return false;
    }

    public override void End_event()
    {
        child.eulerAngles = new Vector3(0, -90, 0);
        ini = false;
    }

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        ini = true;
    }

    void Update () {
        if (ini == true && child.rotation.x > -0.14f)
             child.eulerAngles -= new Vector3(0.5f, 0, 0);
		
	}
}
