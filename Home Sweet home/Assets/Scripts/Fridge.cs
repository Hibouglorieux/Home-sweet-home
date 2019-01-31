using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interractable_item {

    [SerializeField] Transform door;
    AudioSource _source;
    private bool ini;

	void Start () {
        _source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (ini == true && door.eulerAngles.y > 150)
            door.Rotate(new Vector3(0, -0.5f, 0));
    }

    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            //Debug.Log("used"); //ok
            if (timer_script != null)
            {
                End_event();
            }
            return true;
        }
        return false;
    }

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        timer_script.parentType = (int)Item.Interract_item.fridge;

        ini = true;

        _source.time = 0;
        _source.Play();
    }

    public override void End_event()
    {
        SucceedSound.Succeed();
        _source.Stop();

        door.eulerAngles = new Vector3(0, -90, 0);
        Destroy(timer_script.gameObject);
        timer_script = null;
        ini = false;

    }
}
