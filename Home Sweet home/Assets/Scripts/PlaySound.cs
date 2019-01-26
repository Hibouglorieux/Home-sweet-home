using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    [SerializeField] AudioSource _source;

    public void Play()
    {
        if (!_source.isPlaying)
            _source.Play();
    }
}
