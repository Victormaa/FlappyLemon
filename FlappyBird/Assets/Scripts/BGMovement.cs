using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    public float moveingSpeed;

    private float startPoint;
    private float length;
    float cameraLength;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.transform.position.x;
        length = this.GetComponent<SpriteRenderer>().bounds.size.x;
        cameraLength = 2 * Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position= new Vector3(this.transform.position.x + moveingSpeed*Time.deltaTime, this.transform.position.y, this.transform.position.z);
        if(transform.position.x > startPoint + length - cameraLength)
        {
            this.transform.position = new Vector3(startPoint,this.transform.position.y, this.transform.position.z);
        }
    }
}
