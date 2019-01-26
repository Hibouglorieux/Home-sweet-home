using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Interractable_item {

    [SerializeField] GameObject child_particle;
    private bool ini;
    [SerializeField] ParticleSystem particles;
    [SerializeField] AudioClip _begining, _ending;
    [SerializeField] AudioSource _source;


    private void Start()
    {
        StartEvent();
    }
    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            Debug.Log("used"); //ok
            // ajouter son
            if (timer_script && (timer_script.actual_timer -= timer_script.max_timer / duration_of_event) < 0)
            {
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
        ini = false;
        particles.Play();

        _source.clip = _begining;
        _source.time = 0;
        _source.Play();
    }

    public override void End_event()
    {
        child_particle.SetActive(false);
        particles.Stop();

        _source.Stop();
        Destroy(timer_script.gameObject);
        timer_script = null;
    }

    void Update () {
		if (ini == false && timer_script && timer_script.actual_timer >= duration_of_event / 2)
        {
            child_particle.SetActive(true);

            ini = true;

            _source.clip = _ending;
            _source.time = 0;
            _source.Play();
        }
	}
}
