using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    private Transform m_CameraTransform;
    public Transform m_PlayerTransform;

	void Start ()
    {
        m_CameraTransform = GetComponent<Transform>();
	}
	

	void Update ()
    {
        if(m_PlayerTransform.position.x <= 0)
        {
            m_CameraTransform.position = new Vector3(0, 0, -10);
        }
        else
        {
            m_CameraTransform.position = new Vector3(m_PlayerTransform.position.x, 0, -10);
        }

        if(m_PlayerTransform.position.x >= 158)
        {
            m_CameraTransform.position = new Vector3(158, 0, -10);
        }
        
	}
}
