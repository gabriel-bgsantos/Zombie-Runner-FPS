using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] int hitDamage = 10;
    [SerializeField] ParticleSystem muzzleFlash;


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
            // TO DO: add some hit effect 
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
}
