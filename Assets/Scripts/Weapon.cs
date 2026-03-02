using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Hotbar Icon")]
    public Sprite icon;

    [Header("State")]
    public bool isActiveWeapon = false;

    public Camera playerCamera;

    // Shooting
    private bool isShoting;
    private bool readyToShoot = true;
    public float shootingDelay = 0.2f;

    // Burst
    public int bulletPerBurst = 3;
    private int burstBulletsLeft;

    // Spread
    public float spreadIntensity;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;

    // Reload
    public float reloadTime = 1.2f;
    public int magazineSize = 12;
    private int bulletsLeft;
    private bool isReloading;

    public enum ShootingMode { Single, Burst, Auto }
    public ShootingMode currentShootingMode;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        burstBulletsLeft = bulletPerBurst;
    }

    private void Update()
    {
        if (!isActiveWeapon || !gameObject.activeSelf || PauseMenu.GameIsPaused)
            return;

        if (currentShootingMode == ShootingMode.Auto)
            isShoting = Input.GetKey(KeyCode.Mouse0);
        else
            isShoting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading)
            Reload();

        if (readyToShoot && isShoting && bulletsLeft > 0 && !isReloading)
        {
            if (currentShootingMode == ShootingMode.Burst)
                burstBulletsLeft = bulletPerBurst;

            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        if (bulletsLeft <= 0)
            return;

        bulletsLeft--;

        if (muzzleEffect != null)
            muzzleEffect.GetComponent<ParticleSystem>().Play();

        if (SoundManager.Instance != null)
            SoundManager.Instance.colt1911_shot.Play();

        readyToShoot = false;
        Invoke(nameof(ResetShot), shootingDelay);

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.forward = shootingDirection;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        UpdateAmmoUI();

        if (currentShootingMode == ShootingMode.Burst)
        {
            burstBulletsLeft--;

            if (burstBulletsLeft > 0 && bulletsLeft > 0)
                Invoke(nameof(FireWeapon), shootingDelay);
        }
    }

    private void Reload()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.reloadingsound.Play();

        isReloading = true;
        Invoke(nameof(ReloadCompleted), reloadTime);
    }

    private void ReloadCompleted()
    {
        if (AmmoManager.Instance == null)
        {
            isReloading = false;
            return;
        }

        int ammoAvailable = AmmoManager.Instance.GetCurrentAmmo();
        int bulletsNeeded = magazineSize - bulletsLeft;

        int bulletsToLoad = Mathf.Min(bulletsNeeded, ammoAvailable);

        bulletsLeft += bulletsToLoad;

        AmmoManager.Instance.RemoveAmmo(bulletsToLoad);

        isReloading = false;

        UpdateAmmoUI();
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    void UpdateAmmoUI()
    {
     {
    if (AmmoManager.Instance == null || AmmoManager.Instance.ammoDisplay == null)
        return;

    AmmoManager.Instance.ammoDisplay.text =
        $"{bulletsLeft}/{AmmoManager.Instance.GetCurrentAmmo()}";
}
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint = Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(100);
        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public void PickUp(Transform weaponHolder)
    {
        transform.SetParent(weaponHolder, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
            rb.useGravity = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        gameObject.SetActive(false);
        isActiveWeapon = true;

        ItemsManager itemsManager = Object.FindAnyObjectByType<ItemsManager>();
        if (itemsManager != null)
            itemsManager.PickUpItem(gameObject, icon);

        UpdateAmmoUI();
    }
}