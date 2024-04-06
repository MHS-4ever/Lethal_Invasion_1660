using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CanWalkRight = true;
    private bool CanWalkLeft = true;
    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OppPosition;
    private bool FacingLeft = false;
    private bool FacingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Listen to the Animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Cannot exit Screen
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

        if(ScreenBounds.x > Screen.width - 200)
        {
            CanWalkRight = false;
        }
        if (ScreenBounds.x < 200)
        {
            CanWalkLeft = false;
        }
        else if (ScreenBounds.x > 200 && ScreenBounds.x < Screen.width - 200)
        {
            CanWalkRight = true;
            CanWalkLeft = true;
        }


        //Get the Opponent's position
        OppPosition = Opponent.transform.position;


        //Facing left or right of the opponent
        if(OppPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
        }
        if (OppPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
        }


        //Walking Forward & Backward
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                    if (CanWalkRight == true)
                    {
                        Anim.SetBool("Forward", true);
                        transform.Translate(WalkSpeed, 0, 0);
                    }   
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                    if(CanWalkLeft == true)
                    {
                        Anim.SetBool("Backward", true);
                        transform.Translate(-WalkSpeed, 0, 0);
                    }
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }


        //Jumping & Crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            if(IsJumping == false)
            {
                IsJumping = true;
                Anim.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch", true);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch", false);
        }

        IEnumerator JumpPause()
        {
            yield return new WaitForSeconds(1.0f);
            IsJumping = false;
        }

        IEnumerator FaceLeft ()
        {
            if(FacingLeft == true)
            {
                FacingLeft = false;
                FacingRight = true;
                yield return new WaitForSeconds(0.15f);
                Player1.transform.Rotate(0, 180, 0);
            }
        }

        IEnumerator FaceRight()
        {
            if (FacingRight == true)
            {
                FacingRight = false;
                FacingLeft = true;
                yield return new WaitForSeconds(0.15f);
                Player1.transform.Rotate(0, -180, 0);
            }
        }
    }
}
