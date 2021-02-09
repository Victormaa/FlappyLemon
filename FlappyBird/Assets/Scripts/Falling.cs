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
        if (Lemon.position.y < -6.0f)
        {
            Lemon.position = new Vector3(Lemon.position.x, 8.0f, Lemon.position.z);
            Lemon.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (Lemon.position.y > 8.0f)
        {
            Lemon.position = new Vector3(Lemon.position.x, -6.0f, Lemon.position.z);
            Lemon.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
