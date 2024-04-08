using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float JumpSpeed;
    public GameObject Player1;
    private Animator Anim;
    private AnimatorStateInfo Player1Layer0;
    private bool HeavyMoving = false;
    public float PunchSlideAmt = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Heavy Punch Slide
        if(HeavyMoving == true)
        {
            if (Player1Move.FacingRight == true)
            {
                Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player1Move.FacingLeft == true)
            {
                Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }
        }


        //Listen to the Animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);


        //Standing Attacks
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Anim.SetTrigger("LightPunch");
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Anim.SetTrigger("HeavyPunch");
            }
            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");
            }
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
            }
        }


        //Crouching Attack
        if (Player1Layer0.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");
            }
        }


        //Ariel Attack
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
            }
        }
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
    }
    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
        Player1.transform.Translate(0, 0, 0);
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        Player1.transform.Translate(0.1f, 0, 0);
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        Player1.transform.Translate(-0.1f, 0, 0);
    }

    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;
    }
}
