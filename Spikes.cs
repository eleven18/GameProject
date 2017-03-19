using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

   

    private Rigidbody2D m_Rigidbody;
    private float m_Speed= 160;

	void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            m_Rigidbody.velocity = new Vector2(m_Speed * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
