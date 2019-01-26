using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijoncteur : Interractable_item {

    public AudioSource sound;

    private void Awake()
    {
        sound.Stop();
    }

        
}
