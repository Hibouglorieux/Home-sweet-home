using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disjoncter : Interractable_item
{
    [SerializeField] ParticleSystem _particle;
    [SerializeField] AudioSource _source;

    [SerializeField] float _sparkSoundLoop = 2f;

    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
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
        timer_script.parentType = (int)Item.Interract_item.electricity;

        StartCoroutine(LoopSparkSound());
        _particle.Play();
    }

    public override void End_event()
    {
        _source.Stop();
        SucceedSound.Succeed();

        _particle.Stop();
        Destroy(timer_script.gameObject);
        timer_script = null;
    }

    IEnumerator LoopSparkSound()
    {
        while (timer_script != null)
        {
            _source.Play();
            yield return new WaitForSecondsRealtime(_sparkSoundLoop);
        }
    }
}
