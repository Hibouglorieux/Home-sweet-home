using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_manager : MonoBehaviour {

    [SerializeField] int i;
    int[] tab = new int[8]; // nombre d'evenements a faire pop
    [SerializeField] int event_rate;
    [SerializeField] int random_event;
    [SerializeField] float Ending_timer = 120;

    public GameObject shelf;
    public GameObject fridge;
    public GameObject gaz;
    public GameObject car;
    public GameObject electricity;
    public GameObject cthulhu;
    public GameObject sink;
    public GameObject fruit;


    // Use this for initialization
    void Start() {
        i = 0;
        for (int j = 0; j < 8; j++)
            tab[j] = -1;

    }

    void Launch_event(GameObject id)
    {
        if (id.GetComponent<Interractable_item>().timer_script != null)
        {
            Launch_event(Chose_event(true));
            return ;
        }
        id.GetComponent<Interractable_item>().StartEvent();
        // lancer la fonction du gameobject qui lance l'animation ?
        Debug.Log("Event !!" + id.ToString());
        i = 0;
    }

    GameObject Chose_event(bool mod)
    {
        int event_number = Random.Range(0, 8);
        int count = 0;
        for (int k = 0; k <= 7; k++)
        {
            if (mod == true)
                break;
            if (event_number == tab[k])
                return (Chose_event(true));
        }
        while (count < 7  && tab[count] != -1)
            count++;
        tab[count] = event_number;
        switch (event_number)
        {
            case 0:
                return (shelf);
            case 1:
                return (fridge);
            case 2:
                return (gaz);
            case 3:
                return (car);
            case 4:
                return (electricity);
            case 5:
                return (cthulhu);
            case 6:
                return (sink);
            case 7:
                return (fruit);
            default:
                {
                    Debug.Log("default in Chose_event() from Event_manager");
                    return (fridge);
                }
        }

    }
	// Update is called once per frame
	void Update () {
        if (GameManager.inst.states != GameManager.GameStates.playing)
            return;
        if (i == 0)
            i = Random.Range(0, random_event); // a revoir ?
        i++;
        if (i >= event_rate && Ending_timer - 1000 > 0)
            Launch_event(Chose_event(false)); // a revoir ?
        if (Ending_timer <= 0)
            CanvasManager.inst.DisplayWonDialogue(); // faire fin du jeu
        Ending_timer -= Time.deltaTime;
	}
}
