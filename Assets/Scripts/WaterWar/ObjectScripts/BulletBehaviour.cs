using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    const float moveSpeed = 0.18f; //movement Speed
    const float maxLifeTime = 0.6f; //Lifetime of object in seconds
    float currentLifeTime = 0f; //Lifetime of object in seconds
    void Update() => BulletLifeTime(); //Update bullets cursrent life time
    void FixedUpdate() => Move(); //Update bullet movement
    /// <summary>
    /// Update the bullets lifetime and destroys it when above max life time
    /// </summary>
    void BulletLifeTime()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= maxLifeTime)
        {
            GameObject.Destroy(gameObject);
        }
    }
    void Move() => transform.Translate(transform.forward * moveSpeed, Space.World); // Move bullet forward
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            default:
                Destroy(gameObject);
                break;
        }
    }
}
