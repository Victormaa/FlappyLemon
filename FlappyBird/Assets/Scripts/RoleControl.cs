using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleControl : MonoBehaviour
{
    public GameObject Lemon;

    private Rigidbody2D LemonRIG;

    private Vector2 OriginalGravity;

    private bool windOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        LemonRIG = Lemon.GetComponent<Rigidbody2D>();
        OriginalGravity = Physics2D.gravity;

        GameManager.Instance.onPlayingEvent.AddListener(RolePlaying);
        GameManager.Instance.onPauseEvent.AddListener(RolePause);
        GameManager.Instance.onPlayingNormalEvent.AddListener(WindOff);
        GameManager.Instance.onPlayingWindOnEvent.AddListener(WindOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isMenu)
        {
            Physics2D.gravity = Vector2.zero;
            LemonRIG.velocity = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameManager.Instance.SwitchState("Playing");
                LemonRIG.velocity = Vector2.zero;
                LemonRIG.AddForce(Vector2.up * 4.0f, ForceMode2D.Impulse);
            }
        }

        if (GameManager.Instance.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!windOn)
                {
                    LemonRIG.velocity = Vector2.zero;
                    LemonRIG.AddForce(Vector2.up * 4.0f, ForceMode2D.Impulse);
                }
                else
                {
                    LemonRIG.velocity = Vector2.zero;
                    LemonRIG.AddForce(-Vector2.up * 4.0f, ForceMode2D.Impulse);
                }
            }
        }
        if (GameManager.Instance.isPause)
        {
            //the Pause stuff

        }

    }

    private void RolePause()
    {
        Physics2D.gravity = Vector2.zero;
        LemonRIG.velocity = Vector2.zero;
    }

    private void RolePlaying()
    {
        Physics2D.gravity = OriginalGravity;
    }

    public void WindOn()
    {
        //set to abnormal state
        Physics2D.gravity = -OriginalGravity;
        windOn = true;
    }
     public void WindOff()
    {
        //Reset to normal state
        Physics2D.gravity = OriginalGravity;
        windOn = false;
    }
}
