using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlinkSign : MonoBehaviour
{
    public float ratio;
    UnityAction ColorToRed;
    UnityAction ColorToYellow;
    // Start is called before the first frame update
    void Start()
    {
        ColorToRed += delegate
        {
            ColorChange(Color.HSVToRGB(0, 65, 100));
        };
        ColorToYellow += delegate
        {
            ColorChange(Color.yellow);
        };
        GameManager.Instance.onPlayingWindOnEvent.AddListener(ColorToRed);
        GameManager.Instance.offPlayingWindOnEvent.AddListener(ColorToYellow);
        LeanTween.scale(this.gameObject, new Vector3(this.transform.localScale.x + ratio, this.transform.localScale.y + ratio, this.transform.localScale.z + ratio), 0.2f).setLoopPingPong(-1);
    }

    public void ColorChange(Color To)
    {
        SpriteRenderer a = this.GetComponent<SpriteRenderer>();
        if (a == null)
            return;
        if (a.name != "WarnSign_red")
            return;
        a.color = To;

    }
}
