using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

   //Zmienne do fizyki, animacji, warunku czy stoi na ziemi
    private Rigidbody2D m_Rigidbody;
    private Animator m_PlayerAnimator;
    private bool m_Grounded = true;
    private bool m_Running = false;

    //Predkosc chodzenia i sila skoku
    public float m_Speed = 6;
    public float m_JumpForce = 200;



	void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerAnimator = GetComponent<Animator>();
	}

	void Update ()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Horizontal"))
        {
            //Jezeli biegnie (m_Running = true)
            m_Running = true;
            m_Rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * m_Speed, m_Rigidbody.velocity.y);
            m_PlayerAnimator.SetBool("run", true);

            //Jezeli sie porusza to
            if (m_Rigidbody.velocity != Vector2.zero)
            {
                //Jezeli predkosc jest ujemna (biegnie w lewo) obroc go w lewo
                if (m_Rigidbody.velocity.x < 0)
                {
                    transform.right = Vector2.left;
                }
                //Jezeli predkosc jest dodatnia (biegnie w prawo) obrog go w prawo
                else
                {
                    transform.right = Vector2.right;
                }
            }

        }
        else
        {
            //Jezeli nie  biega (m_Running = false)
            m_Running = false;
            m_Rigidbody.velocity = Vector2.zero;
            m_PlayerAnimator.SetBool("run", false);
        }


        //Jezeli klikniesz skacz i stoi na ziemi (m_Grounded == true) to skocz
        if (Input.GetButton("Jump") && m_Grounded)
        {
            m_Rigidbody.AddForce(new Vector2(0, Input.GetAxis("Jump") * m_JumpForce));
            m_PlayerAnimator.SetBool("jump", true);
        }
        else
        {
            m_PlayerAnimator.SetBool("jump", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Jezeli trigger przy nogach dotyka ziemi (m_Grounded = true)
        m_Grounded = true;
        //Jezeli nie biegnie to 
        if(!m_Running)
        {
            //Do animacji stania wyslac prawde
            m_PlayerAnimator.SetBool("player_stay", true);
            //Do animacji skoku wyslac falsz
            m_PlayerAnimator.SetBool("player_jump", false);
        }
        else
        {
            //Inaczej (jezeli biegnie) wyslac do biegu prawde a do skoku falsz
            m_PlayerAnimator.SetBool("player_run", true);
            m_PlayerAnimator.SetBool("player_jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Jezeli nie dotyka ziemi (m_Grounded = false)
        m_Grounded = false;
    }
}
