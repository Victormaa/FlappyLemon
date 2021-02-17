using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float generateRate = 1.0f;
    [SerializeField]
    private float timer = 0;
    public GameObject Pipe;
    public float Height;

    private int Pipe_sum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pipe_sum == 3)
            generateRate = 4.3f;

        if (timer > generateRate)
        {
            GameObject newPipe = Instantiate(Pipe);
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-Height, Height), 0);
            timer = 0;
            Pipe_sum += 1;
            generateRate = Random.Range(1.85f, 3.2f);
        }
        if (GameManager.Instance.isPlaying)
        {
            timer += Time.deltaTime;
        }
       
    }
}
