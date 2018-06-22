﻿
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject impactEffectStone;
    public int ammo = 10;

    public bool autoFire = false;

    private float nextTimeToFire = 0f;



    // Update is called once per frame
    void Update()
    {
        if (autoFire == true)
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
        if (ammo <= 0)
        {
            return;
        }

        ammo = ammo - 1;
       

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
}
