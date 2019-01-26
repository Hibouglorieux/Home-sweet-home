using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interractable_item {

    [SerializeField] GameObject door;

	void Start () {
        needed_item = Drag_item.none;
        duration_of_event = 20;
	}

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        Launch_animation();
    }

    void Launch_animation()
    {
        ; // faire la porte
    }
}
