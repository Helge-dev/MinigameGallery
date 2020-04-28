using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    const float moveSpeed = 0.25f, //movement Speed
                fallSpeed = -0.01f; //movement Speed
    const float maxLifeTime = 4f; //Lifetime of object in seconds
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
    void Move() => transform.Translate(transform.forward * moveSpeed + new Vector3(0, fallSpeed,0), Space.World); // Move bullet forward
    
}
