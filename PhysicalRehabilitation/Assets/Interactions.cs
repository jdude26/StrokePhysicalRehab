using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    //Vector3 m_YAxis;
    //float m_Speed;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        //Press V to add constraints on the RigidBody (freeze all positions and rotations)
        if (Input.GetKeyDown(KeyCode.V))
        {
            //Freeze all positions and rotations
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}