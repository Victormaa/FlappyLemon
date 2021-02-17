using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkSign : MonoBehaviour
{
    public float ratio;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(this.gameObject, new Vector3(this.transform.localScale.x + ratio, this.transform.localScale.y + ratio, this.transform.localScale.z + ratio), 0.2f).setLoopPingPong(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
