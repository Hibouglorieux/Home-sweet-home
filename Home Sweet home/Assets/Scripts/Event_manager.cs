using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_manager : MonoBehaviour {

    [SerializeField] int i;
    [SerializeField] int[] tab = new int[6]; // nombre d'evenements a faire pop
    [SerializeField] int event_rate;
    [SerializeField] int random_event;
    [SerializeField] int Ending_timer;

    public GameObject shelf;
    public GameObject fridge;
    public GameObject gaz;
    public GameObject car;
    public GameObject electricity;
    public GameObject cthulhu;


    // Use this for initialization
    void Start() {
        i = 0;
        for (int j = 0; j <= 5; j++)
            tab[j] = -1;

    }

    void Launch_event(GameObject id)
    {
        // lancer la fonction du gameobject qui lance l'animation ?
        Debug.Log("Event !!");
        i = 0;
    }

    GameObject Chose_event(bool mod)
    {
        int event_number = Random.Range(0, 6);
        int count = 0;
        for (int k = 0; k <= 5; k++)
        {
            if (mod == true)
                break;
            if (event_number == tab[k])
                return (Chose_event(true));
        }
        while (count < 5  && tab[count] != -1)
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
            default:
                {
                    Debug.Log("default in Chose_event() from Event_manager");
                    return (fridge);
                }
        }

    }
	// Update is called once per frame
	void Update () {
        if (i == 0)
            i = Random.Range(0, random_event); // a revoir ?
        i++;
        if (i >= event_rate && Ending_timer - 1000 > 0)
            Launch_event(Chose_event(false)); // a revoir ?
        if (Ending_timer <= 0)
            ; // faire fin du jeu
        Ending_timer--;
	}
}
