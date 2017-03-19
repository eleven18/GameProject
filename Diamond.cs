using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private float m_Time = 0.0f;
    private float m_Minimum = -1.2f;
    private float m_Maximum = -0.8f;

	void Start ()
    {
       
	}


    void Update()
    {

        transform.position = new Vector3(transform.position.x, Mathf.Lerp(m_Minimum, m_Maximum, m_Time), transform.position.z);        
        m_Time += 0.5f * Time.deltaTime;
        
        if(m_Time > 1.0f)
        {
            float temp = m_Maximum;
            m_Maximum = m_Minimum;
            m_Minimum = temp;
            m_Time = 0.0f;
        }        
        
        Debug.Log(m_Time);



    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
