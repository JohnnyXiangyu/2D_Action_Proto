using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health = 100.0f;
    public bool isAlive = true;
    
    public void TakeDamage(float damage) {
        if (!isAlive) {
            return;
        }
        
        health -= damage;
        if (health <= float.Epsilon) {
            health = 0;
            isAlive = false;
        }
    }
}
