using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Interractable_item
{
    AudioSource _source;

    [SerializeField] GameObject[] _candles;
    uint _candleLighted = 0;
    const uint _maxCandle = 4;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public override bool Launch_event(Drag_item item_held)
    {
        if (item_held == needed_item /*|| needed_item == Drag_item.none*/)
        {
            if (timer_script )//&& (timer_script.actual_timer -= timer_script.max_timer / duration_of_event) < 0)
            {
                _candleLighted++;

                if (_candleLighted <= _maxCandle)
                {
                    _candles[_candleLighted - 1].SetActive(true);
                    if (_candleLighted == _maxCandle)
                        End_event();
                    return true;
                }
            }
        }
        return false;
    }

    public override void StartEvent()
    {
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion()).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        timer_script.parentType = (int)Item.Interract_item.cthulhu;


        _source.time = 0;
        _source.Play();

        _candleLighted = 0;
        for (int i = 0; i < _candles.Length; i++)
        {
            _candles[i].SetActive(false);
        }
    }

    public override void End_event()
    {
        _source.Stop();

        Destroy(timer_script.gameObject);
        timer_script = null;
    }
}
