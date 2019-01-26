using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager inst;

    public enum GameStates  { starting, playing, paused, end }
    public GameStates states = GameStates.starting;
    public GameObject cnvs;

    public int fireCount;
    [SerializeField] uint maxFireCount = 10;
    public bool canAddFire { get {  return fireCount < maxFireCount; } }

    void Awake()
    {
        inst = this;
        cnvs.SetActive(true);
    }
}
