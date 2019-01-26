using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_camera : MonoBehaviour {

    public GameObject player;
    private Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + pos;
	}
}
