using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoleControl : MonoBehaviour
{
    public Animator animator;

    public AudioSource ScoredSFX; 

    public GameObject Lemon;

    private Rigidbody2D LemonRIG;

    private Vector2 OriginalGravity;

    private bool windOn = false;

    UnityAction onplayingAction;
    UnityAction unplayingAction;

    private bool is_scoring = false;
    // Start is called before the first frame update
    void Start()
    {
        LemonRIG = Lemon.GetComponent<Rigidbody2D>();
        OriginalGravity = Physics2D.gravity;

        onplayingAction += delegate { RoleAnimation(true); };
        unplayingAction += delegate { RoleAnimation(false); };
        GameManager.Instance.onPlayingEvent.AddListener(onplayingAction);
        GameManager.Instance.offPlayingEvent.AddListener(unplayingAction);

        GameManager.Instance.onMenuEvent.AddListener(RoleInMenuState);
        GameManager.Instance.offMenuEvent.AddListener(RoleOutMenuState);
        GameManager.Instance.onPlayingEvent.AddListener(RolePlaying);
        GameManager.Instance.onPauseEvent.AddListener(RolePause);
        GameManager.Instance.onPlayingNormalEvent.AddListener(WindOff);
        GameManager.Instance.onPlayingWindOnEvent.AddListener(WindOn);

        GameManager.Instance.onEndEvent.AddListener(PlayerDie);
        GameManager.Instance.offEndEvent.AddListener(WindOff);

        RoleInMenuState(); //since the on menu event would not invoke at the first time for some reason so you would be better to set up a initialize function to handle this
    }

    private void RoleInMenuState()
    {
        #region floating 
        LeanTween.moveLocalY(this.gameObject, 0.5f, 0.5f).setLoopPingPong();
        #endregion
    }

    private void RoleOutMenuState()
    {
        #region stop floating 
        LeanTween.cancelAll();
        #endregion
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
                Physics2D.gravity = OriginalGravity;
                LemonRIG.velocity = Vector2.zero;
                LemonRIG.AddForce(Vector2.up * 4.0f, ForceMode2D.Impulse);
            }
        }

        if (GameManager.Instance.isPlaying && this.transform.position.y < 8.0f)//
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

    private void RoleAnimation(bool isplaying)
    {
        Debug.Log("animation isplaying: " + isplaying);
        animator.SetBool("isPlaying", isplaying);
    }

    private void RolePause()
    {
        Physics2D.gravity = Vector2.zero;
        LemonRIG.velocity = Vector2.zero;
    }

    //private void RoleUnPause() => LemonRIG.constraints = RigidbodyConstraints2D.FreezeRotation;

    private void RolePlaying() => Physics2D.gravity = windOn ? -OriginalGravity : OriginalGravity;

    public void WindOn()
    {
        //set to abnormal state
        Physics2D.gravity = -OriginalGravity;
        LemonRIG.velocity = Vector2.zero;
        windOn = true;
    }
     public void WindOff()
    {
        //Reset to normal state
        Physics2D.gravity = OriginalGravity;
        LemonRIG.velocity = Vector2.zero;
        windOn = false;
    }

    private void PlayerDie()
    {
        LemonRIG.velocity = Vector2.zero;
        animator.SetBool("isDie", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pipe")&&GameManager.Instance.isPlaying)
        {
            //something happens to you when you die
            GameManager.Instance.SwitchState("End");
        }
        if (collision.CompareTag("Point") && GameManager.Instance.isPlaying)
        {
            //scoring
            is_scoring = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Point") && GameManager.Instance.isPlaying)
        {
            //scoring
            GameManager.Instance.game_Score += 1;
            //Sounds
            ScoredSFX.Play();

            is_scoring = false;
        }
    }

}
