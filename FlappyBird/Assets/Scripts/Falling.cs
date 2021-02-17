using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public Transform Lemon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Lemon.position.y < -3.7f)
        {
            if(GameManager.Instance.isPlaying)
                GameManager.Instance.SwitchState("End");

            //Lemon.position = new Vector3(Lemon.position.x, 8.0f, Lemon.position.z);
            Lemon.GetComponent<Rigidbody2D>().gravityScale = 0;
            Lemon.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (Lemon.position.y > 10.7f)
        {
            if (GameManager.Instance.isPlaying)
                GameManager.Instance.SwitchState("End");

            //Lemon.position = new Vector3(Lemon.position.x, 8.0f, Lemon.position.z);
            Lemon.GetComponent<Rigidbody2D>().gravityScale = 0;
            Lemon.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
