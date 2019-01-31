using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : Interractable_item {

    [SerializeField] Transform child;
    private bool ini = false;
    [SerializeField] AudioSource sound;
    [SerializeField] Animator _anim;
    // Use this for initialization
    private void Awake()
    {
        needed_item = Drag_item.hammer;

        _anim = GetComponent<Animator>();
    }
    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            Debug.Log("used"); //ok
                               // ajouter son
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

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        timer_script.parentType = (int)Item.Interract_item.shelf;

        _anim.SetTrigger("broken");

        sound.Play();
        ini = true;
    }

    public override void End_event()
    {
        SucceedSound.Succeed();

        _anim.SetTrigger("fixed");
        ini = false;
    }
}
