using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject _thePipe;
    public float generateRate = 1.0f;
    [SerializeField]
    private float timer = 0;
    public GameObject Pipe;
    public float Height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > generateRate)
        {
            GameObject newPipe = Instantiate(Pipe);
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-Height, Height), 0);
            timer = 0;
            generateRate = Random.Range(1.6f, 3.2f);
        }
        if (GameManager.Instance.isPlaying)
        {
            timer += Time.deltaTime;
        }
       
    }
}
