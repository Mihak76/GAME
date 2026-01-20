using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    //Shoting
    public bool isShoting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int bulletPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity; 

    //Bullet 
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f; 
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;

    //Loading   
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

   


    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletPerBurst;

        bulletsLeft = magazineSize;
    }

    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            //Holding down left mouse button
            isShoting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single ||
                 currentShootingMode == ShootingMode.Burst)
        {
            isShoting = Input.GetKeyDown(KeyCode.Mouse0);
        }   

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

        if (readyToShoot && isShoting && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletPerBurst;
            FireWeapon();
        }  

        if (AmmoManager.Instance.ammoDisplay != null)
        {
           AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft/bulletPerBurst}/{magazineSize/bulletPerBurst}";
        }
    }

    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();

        readyToShoot = false; 

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Uporabi rotacijo bulletSpawn
        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation
        );

        //Pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection; 

        // Uporabi forward smer metka
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }
    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        //Shoting from the middle of the screen to check where are we pointing at 
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else 
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
