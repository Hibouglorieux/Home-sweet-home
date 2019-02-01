using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float max_timer;
    public float actual_timer = 0;
    private Image img;
    private Transform image;
    private int count = 0;

    public int parentType = -1;

	void Start () {
        transform.LookAt(Camera.main.transform);
        transform.eulerAngles = (new Vector3(-transform.eulerAngles.x, 0, 0));
        img = GetComponentInChildren<Image>();
        image = transform.GetChild(0);
    }

    void Update()
    {
        if (GameManager.inst.states != GameManager.GameStates.playing)
            return;

        float percent = actual_timer / max_timer;
        img.fillAmount = percent;
        actual_timer += Time.deltaTime;
        if (percent < 0.5)
            img.color = new Color(percent * 2, 0.5f, img.color.b);
        else
            img.color = new Color(1, 1 - percent, img.color.b);
        if (percent >= 0.6)
        {
            if (count % 2 == 0)
            {
                image.localScale += new Vector3(count >= 6 ? 0.035f : 0.017f, -0.001f, 0);
                if (image.localScale.x >= 1.25f)
                    count++;
            }
            else if (count % 2 == 1)
            {
                image.localScale -= new Vector3(count >= 7 ? 0.035f : 0.017f, -0.001f, 0);
                if (image.localScale.x < 1)
                    count++;
            }
        }
        if (percent > 1)
            CanvasManager.inst.DisplayDieDialogue(parentType);
    }
}
