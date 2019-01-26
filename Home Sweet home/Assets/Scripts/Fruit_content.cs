using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_content : Interractable_item {

    [SerializeField] GameObject Fruit_prefab;
    private GameObject inst_prefab;


    public override void StartEvent()
    {
        inst_prefab = Instantiate(Fruit_prefab, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), new Quaternion());
        timer_script = Instantiate(timer, new Vector3(transform.position.x, 4, transform.position.z), new Quaternion(), inst_prefab.transform).GetComponent<Timer>();
        timer_script.max_timer = duration_of_event;
        // gerer les particules
    }

}
