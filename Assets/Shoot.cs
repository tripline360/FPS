
using UnityEngine;
using System.Collections;
public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject impactEffectStone;

    public int maxAmmo = 10;
    public float reloadTime = 1f;
    private int currentAmmo;

    public bool autoFire = false;

    private float nextTimeToFire = 0f;

    void Start ()
    {
        currentAmmo = maxAmmo;
    }



    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if (autoFire)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        currentAmmo = currentAmmo - 1;
       

        muzzleFlash.Play();


        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

            }

            bool playDefaultImpactEffect = false;

            ObjectProperties properties = hit.transform.GetComponent<ObjectProperties>();
            if (properties != null)
            {
                //if (properties.materialType == MaterialType.Stone)
                //{
                //    GameObject impactGO = Instantiate(impactEffectStone, hit.point, Quaternion.LookRotation(hit.normal));
                //    Destroy(impactGO, 2f);
                //}

                if (properties.impactEffect != null)
                {
                    GameObject impactGO = Instantiate(properties.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2f);
                }
                else
                {
                    playDefaultImpactEffect = true;
                }
            }
            else
            {
                playDefaultImpactEffect = true;
            }

            if (playDefaultImpactEffect == true)
            {
                // Play a default impact.
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 2f);
            

        }

        
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");

        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }
}
