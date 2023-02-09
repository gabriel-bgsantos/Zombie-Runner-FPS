using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] int hitDamage = 10;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect; // gameObject because it needs to be instantiated

    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    void ProcessRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) { // origin, direction, hit, range
            Debug.Log("I hit this thing: " + hit.transform.name);
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(hitDamage);
        }
        else { 
            return;
        }
    }

    void PlayMuzzleFlash() {
        muzzleFlash.Play();
    }

    void CreateHitImpact(RaycastHit hit) {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); //hit.point = location, quaternion(where the particle will look at)
        Destroy(impact, 1);
    }
}
