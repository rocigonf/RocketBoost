using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int currentAmmo = 10;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private bool infiniteAmmo = false;

    [Header("Shooting settings")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private int bulletDamage = 1;

    private Transform bulletSpawnPoint;
    private ObjectPool bulletPool;

    private float lastShotTime = 0f;

    void Start()
    {
        bulletPool = GetComponent<ObjectPool>();
        bulletSpawnPoint = transform.Find("ShootPoint");
    }

    /// <summary>
    ///  Checks if the player can shoot based on the bullet rate and ammo availability
    /// </summary>
    /// <returns></returns>
    public bool CanShoot()
    {
        // Check if enough time has passed since the last shot and if there is ammo available or infinite ammo is enabled
        return (Time.time - lastShotTime >= fireRate) && (infiniteAmmo || currentAmmo > 0);
    }

    /// <summary>
    /// Shoot a bullet from the object pool and apply
    /// </summary>
    public void Shoot()
    {
        if (CanShoot())
        {
            // Updates time since last shot
            lastShotTime = Time.time;

            // Reduces the ammo
            if (!infiniteAmmo) currentAmmo--;

            // Gets a bullet from the bullet pool and locates the bullet spawn position and rotation
            GameObject bullet = bulletPool.GetObjectFromPool();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;

            // Set the bullet's speed and movement
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.linearVelocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }
}
