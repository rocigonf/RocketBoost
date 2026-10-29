using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 1f;

    void OnEnable()
    {
        // Starts the bullet's lifetime countdown
        Invoke(nameof(DeactivateBullet), bulletLifeTime);
    }

    /// <summary>
    /// Deactivates the bullet
    /// </summary>
    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Deactivates the bullet when it collides with another object
        DeactivateBullet();

        // TODO: Particles 
        // TODO: If bullet hits an enemy, reduce its health
        // TODO: If bullet hits player, reduce player's health
        // TODO: If bullet hits obstacle, destroy obstacle
    }
}
