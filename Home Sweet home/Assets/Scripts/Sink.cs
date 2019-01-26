using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Interractable_item {

    [SerializeField] GameObject child_particle;
    private bool ini;
    [SerializeField] ParticleSystem particles;

    private void Awake()
    {
        duration_of_event = 15f;
        particles.Stop();
    }

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        ini = false;
        particles.Play();
    }

    public override void End_event()
    {
        child_particle.SetActive(false);
        particles.Stop();
    }

    void Update () {
		if (ini == false && timer_script && timer_script.actual_timer >= duration_of_event / 2)
        {
            child_particle.SetActive(true);
            Debug.Log("set active activated");
            ini = true;
        }
	}
}
