using UnityEngine;

public class Car : Interractable_item {

    Animator _anim;
    bool playing;

    private void Awake()
    {
        needed_item = Drag_item.keys;
    }

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            StartEvent();
    }

    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            End_event();
        }
        return false;
    }

    public override void StartEvent()
    {
        if (playing) return;
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        timer_script.parentType = (int)Item.Interract_item.car;

        playing = true;
        _anim.SetTrigger("StartAlarm");
    }

    public override void End_event()
    {
        playing = false;
        Destroy(timer_script.gameObject);
        timer_script = null;
        _anim.SetTrigger("CutAlarm");
    }
}
