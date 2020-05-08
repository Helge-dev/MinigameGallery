using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public int nrOfBounces;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.name == "Table")
        {
            nrOfBounces++;
            Debug.Log(nrOfBounces);
        }
        
    }
}
