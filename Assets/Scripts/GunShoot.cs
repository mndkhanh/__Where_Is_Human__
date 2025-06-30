using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float range = 100f;
    public float damage = 20f;
    public Camera fpsCam;
    public MuzzleFlash muzzleFlash;
    public GameObject impactEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.PlayFlash();

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); // tâm ngắm
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            }

            if (impactEffect)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
