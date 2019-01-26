using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager inst;

    public uint fireCount;
    [SerializeField] uint maxFireCount = 10;
    public bool canAddFire { get {  return fireCount < maxFireCount; } }

    void Awake()
    {
        inst = this;
    }
}
