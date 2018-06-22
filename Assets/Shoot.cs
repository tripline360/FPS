
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;

    public float range = 100f;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    public GameObject impactEffectStone;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
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
