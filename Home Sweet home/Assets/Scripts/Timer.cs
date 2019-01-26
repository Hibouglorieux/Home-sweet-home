using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float max_timer;
    [SerializeField] float actual_timer = 0;
    private Image img;

	void Start () {
        transform.LookAt(Camera.main.transform);
        transform.eulerAngles = (new Vector3(-transform.eulerAngles.x, 0, 0));
        img = GetComponentInChildren<Image>();
    }
	
	void Update () {
        float percent = actual_timer / max_timer;
        img.fillAmount = percent;
        actual_timer += Time.deltaTime;
        if (percent < 0.5)
            img.color = new Color(percent * 2, 0.5f, img.color.b);
        else
            img.color = new Color(1, 1 - percent, img.color.b);
        if (actual_timer > max_timer)
            ; // gameover
    }
}
