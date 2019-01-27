using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijoncteur : Interractable_item {

    public AudioSource sound;

    private void Awake()
    {
        sound.Stop();
    }

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        timer_script.parentType = (int)Item.Interract_item.electricity;
    }

}
