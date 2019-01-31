using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum FadeType { In = 0, Out = 1 };
public class UIAnimation {

    public static IEnumerator Fade(Graphic obj, FadeType fade, float duration, float mult = 1)
    {
        for (float i = 0; i <= duration; i += Time.unscaledDeltaTime)
        {
            obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, Mathf.Abs(((int)fade) - (i / duration)) * mult);
            yield return null;
        }
        obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, (1 - (int)fade) * mult);
    }

    public static IEnumerator FadeGroup(Graphic[] objs, FadeType fade, float duration, float mult = 1)
    {
        for (float i = 0; i <= duration; i += Time.unscaledDeltaTime)
        {
            for (int j = 0; j < objs.Length; j++)
                if (objs[j] != null)
                    objs[j].color = new Color(objs[j].color.r, objs[j].color.g, objs[j].color.b, Mathf.Abs(((int)fade) - (i / duration)) * mult);
            yield return null;
        }
        for (int j = 0; j < objs.Length; j++)
            if (objs[j] != null)
                objs[j].color = new Color(objs[j].color.r, objs[j].color.g, objs[j].color.b, (1 - Mathf.Abs(((int)fade)) * mult));
    }

    public static IEnumerator FadeButton(Button obj, FadeType fade, float duration, float mult = 1)
    {
        Image img = obj.transform.GetChild(1).GetComponent<Image>();
        TextMeshProUGUI text = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        return FadeGroup(new Graphic[]{ img, text } , fade, duration, mult);
    }
}
