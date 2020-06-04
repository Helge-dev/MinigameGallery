using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public int nrOfBouncesBad;
    public int nrOfBouncesGood;
    public BallMovement ballMovement;
    public Rigidbody rigidbody;

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.name == "Table")
        {
            FindObjectOfType<AudioManager>().Play("Bounce");

            if(ballMovement.left == true)
            {
                if(rigidbody.position.x < 0)
                {
                    nrOfBouncesGood++;
                }
                else
                {
                    nrOfBouncesBad++;
                }
            }
            else if (ballMovement.left == false)
            {
                if (rigidbody.position.x > 0)
                {
                    nrOfBouncesGood++;
                }
                else
                {
                    nrOfBouncesBad++;
                }
            }
            //Debug.Log("bad" + nrOfBouncesBad);
            //Debug.Log("good" + nrOfBouncesGood);
        }
        
    }
}
