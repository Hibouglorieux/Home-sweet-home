using System.Collections;
using System.Collections.Generic;
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
    }

    public static IEnumerator FadeButton(Button obj, FadeType fade, float duration, float mult = 1)
    {
        Image img = obj.GetComponent<Image>();
        Text text = obj.transform.GetChild(0).GetComponent<Text>();
        for (float i = 0; i <= duration; i += Time.unscaledDeltaTime)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Abs(((int)fade) - (i / duration)) * mult);
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Abs(((int)fade) - (i / duration)) * mult);
            yield return null;
        }
    }
}
