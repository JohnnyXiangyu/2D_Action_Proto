﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance = null;

    public float playerHealth = 100;
    public float enemyHealth = 100;

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyHealthText;

    public void Attack(GameObject attacker, GameObject victim) {
        if (attacker.tag == "Player") {
            enemyHealth -= attacker.GetComponent<Actor>().damage;
        }
        else {
            playerHealth -= attacker.GetComponent<Actor>().damage;
        }
    }
    
    private void Awake() {
        // singleton
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    { 
        playerHealthText.text = "" + playerHealth;
        enemyHealthText.text = "" + enemyHealth;
    }
}
