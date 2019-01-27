using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interractable_item {

    [SerializeField] Transform door;
    private bool ini;

	void Start () {
        needed_item = Drag_item.none;
        duration_of_event = 20;
	}

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        timer_script.parentType = (int)Item.Interract_item.fridge;

        ini = true;
    }

    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            Debug.Log("used"); //ok
                door.eulerAngles = new Vector3(0, -90, 0);
            if (timer_script != null)
                Destroy(timer_script.gameObject);
                timer_script = null;
            ini = false;
                return true;
        }
        return false;
    }


    void Update()
    {
        if (ini == true && door.eulerAngles.y > 150)
            door.Rotate(new Vector3(0, -0.5f, 0));
    }
}
