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
        if (Input.GetButton("Horizontal"))
        {
            m_Running = true;
            m_Rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * m_Speed, m_Rigidbody.velocity.y);
            m_PlayerAnimator.SetBool("run", true);

            if (m_Rigidbody.velocity != Vector2.zero)
            {
                if (m_Rigidbody.velocity.x < 0)
                {
                    transform.right = Vector2.left;
                }
                else
                {
                    transform.right = Vector2.right;
                }
            }

        }
        else
        {
            m_Running = false;
            m_Rigidbody.velocity = Vector2.zero;
            m_PlayerAnimator.SetBool("run", false);
        }



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

    private void FixedUpdate()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_Grounded = true;
        if(!m_Running)
        {
            m_PlayerAnimator.SetBool("player_stay", true);
            m_PlayerAnimator.SetBool("player_jump", false);
        }
        else
        {
            m_PlayerAnimator.SetBool("player_run", true);
            m_PlayerAnimator.SetBool("player_jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        m_Grounded = false;
    }
}
