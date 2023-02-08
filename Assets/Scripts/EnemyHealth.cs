using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 100;

    public void TakeDamage(int damage) {
        health -= damage;
        if(health < 1) {
            Destroy(gameObject);
        }
    }
}
