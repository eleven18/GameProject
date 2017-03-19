using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float m_Speed = 5;           //Predkosc chodzenia
    public float m_JumpForce = 15;      //Sila skoku
    public GameObject m_PlayerObject;


    private bool m_Grounded = true;     //Czy stoi na ziemi
    private bool m_Running = false;     //Czy biega
    private Animator m_PlayerAnimator;  //Animator gracza
    private Rigidbody2D m_Rigidbody;    //Fizyka gracza

    private int m_PlayerHealth = 3;     //Ilosc zyc jakie ma gracz
    

	void Start ()
    {
        //Pobranie komponentow
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerAnimator = GetComponent<Animator>();
	}

	void Update ()
    {
        
    }

    private void FixedUpdate()
    {
        //Jezeli stoi na ziemi, nie biegnie i nie skacze
        if(m_Grounded && !m_Running && !(Input.GetButton("Jump")))
        {
            m_PlayerAnimator.SetBool("jump", false);    //Zatrzymaj animacje skoku
            m_PlayerAnimator.SetBool("stay", true);     //Uruchom animacje stania
            m_PlayerAnimator.SetBool("run", false);     //Zatrzymaj animacje biegu
        }

        //Jezeli klikniesz skok i stoi na ziemi
        if (Input.GetButton("Jump") && m_Grounded || (m_Grounded && !m_Running && Input.GetButton("Jump")))
        {
            m_Rigidbody.velocity = new Vector2(0, m_JumpForce);     //Nadaj wysokosc postaci
            m_PlayerAnimator.SetBool("jump", true);                 //Uruchom animację skoku
        }

        //Jezeli klikniesz prawo lub lewo
        if(Input.GetButton("Horizontal"))
        {
            m_Running = true;                           //Zaznacz ze biegnie
            m_PlayerAnimator.SetBool("run", true);      //Uruchom animacje biegu

            //Jezeli w trakcie biegu klikniesz skok
            if (Input.GetButton("Jump") && m_Running)
            {
                m_PlayerAnimator.SetBool("run", false);     //Zatrzymaj animacje biegu
                m_PlayerAnimator.SetBool("jump", true);     //Uruchom animacje skoku
                m_PlayerAnimator.SetBool("stay", false);    //Zatrzymaj animacje stania
            }
            else if (!(Input.GetButton("Jump") && m_Running))//W przeciwnym wypadku jezeli nie skacze i biegnie
            {
                m_PlayerAnimator.SetBool("run", true);      //Uruchom animacje biegu
                m_PlayerAnimator.SetBool("jump", false);    //Zatrzymaj animacje skoku
                m_PlayerAnimator.SetBool("stay", false);    //Zatrzymaj animacje stania
            }

            //Nadawanie predkosci chodzenia
            m_Rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * m_Speed, m_Rigidbody.velocity.y);

            //Jezeli nie stoi w miejscu
            if(m_Rigidbody.velocity != Vector2.zero)
            {
                //Jezeli idzie w lewo
                if(m_Rigidbody.velocity.x < 0)
                {
                    transform.right = Vector2.left;     //Obroc postac w lewo
                }
                else
                {
                    transform.right = Vector2.right;    //Obroc postac w prawo
                }
            }
            else                                        //W przeciwnym wypadku
            {
                m_Rigidbody.velocity = Vector2.zero;    //Zatrzymaj go w miejscu
            }
        }
        else                                            //Jezeli nie klikasz w prawo lub w lewo
        {
            m_Running = false;                          //Zaznacz ze nie biega
            m_PlayerAnimator.SetBool("run", false);     //Zatrzymaj animacje biegu
        }


    }

    //Jezeli trigger pod stopami styka sie z ziemia
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Ground")
        {
            m_Grounded = true;                          //Zaznacz ze stoi na ziemi
        }
    }

    //Jezeli trigger pod stopami przestaje sie stykac z ziemia
    private void OnTriggerExit2D(Collider2D other)
    {
       if(other.tag == "Ground")
        {
            m_Grounded = false;                         //Zaznacz ze nie stoi na ziemie
        }
    }
}
