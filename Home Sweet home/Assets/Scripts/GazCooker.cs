using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazCooker : Interractable_item
{
    [SerializeField] Fire _fire;
    [SerializeField] Transform[] _spawnPoint;
    uint _fireCount;

    private void Awake()
    {
        needed_item = Drag_item.none;
    }

    public override bool Launch_event(Drag_item item_held)
    {
        /*if (item_held == needed_item)
        {
            if (timer_script && (timer_script.actual_timer -= timer_script.max_timer / duration_of_event) < 0)
            {
                Destroy(timer_script.gameObject);
                timer_script = null;
                End_event();
                return true;
            }
        }*/
        return false;
    }

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;

        _fireCount = 0;
        for (int i = 0; i < _spawnPoint.Length; i++)
        {
            Fire f = Instantiate(_fire);
            f._creator = this;
            f.transform.position = _spawnPoint[i].position;

            _fireCount++;
        }
    }

    public override void End_event()
    {
        Debug.Log("gaz safe for now");
        Destroy(timer_script.gameObject);
        timer_script = null;
    }

    public void FireDie()
    {
        _fireCount--;
        if (_fireCount <= 0)
            End_event();
    }
}
