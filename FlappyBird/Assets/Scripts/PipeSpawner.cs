using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float generateRate = 1.0f;
    [SerializeField]
    private float timer = 0;

    [SerializeField] private List<GameObject> _Pipe;
    int GameLevel = 0;
    bool checklevel = false;
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
            generateRate = 4.6f;

        if ((Pipe_sum == 5 || Pipe_sum == 15) && checklevel)
        {
            GameLevel += 1;
            checklevel = false;
        }
            
        if (timer > generateRate)
        {
            if (!_Pipe[GameLevel])
                GameLevel = 0;

            checklevel = true;
            GameObject newPipe = Instantiate(_Pipe[GameLevel]);
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-Height, Height), 0);
            timer = 0;
            Pipe_sum += 1;
            generateRate = Random.Range(2.05f, 3.35f);
        }
        if (GameManager.Instance.isPlaying)
        {
            timer += Time.deltaTime;
        }
       
    }
}
