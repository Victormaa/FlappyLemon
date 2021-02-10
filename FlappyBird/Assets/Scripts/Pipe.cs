using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Range(0.8f, 2.4f)]
    public float Speed = 0.3f;

    [SerializeField]
    private float _timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            this.transform.position -= new Vector3(Speed * Time.deltaTime, 0, 0);
            _timer += Time.deltaTime;

            if (_timer > 8.0f)
                Destroy(this.gameObject);
        }
            
    }
}
