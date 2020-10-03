﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// container object of player status TODO: how to hook this to game manager?
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    // resources //////////////////////////////////////////////////////////////
    public float health = 100;
    public float mana = 100;

    // action locks ///////////////////////////////////////////////////////////
    public int moveable = 0;
    public int attackable = 0;
    public int castable = 0;

    // skill system ///////////////////////////////////////////////////////////
    public string activateSkill = "";
    public float endTime = -1;

    // special effects ////////////////////////////////////////////////////////
    public float armorFactor = 1;
    public Queue<float> shields;
    public bool isFacingRight = true;


    // public methods /////////////////////////////////////////////////////////
    public void GetAttack(float rawDamage) {
        float currentArmorFactor = armorFactor;
        if (shields.Count > 0) {
            currentArmorFactor *= shields.Dequeue();
        }

        health -= rawDamage * currentArmorFactor;
    }

    // system methods /////////////////////////////////////////////////////////
    private void Start() {
        shields = new Queue<float>();
        shields.Clear();
    }

    void Update()
    {
        if (moveable < 0) {
            Debug.Log("movable set to negative! ");
            moveable = 0;
        }
        if (attackable < 0) {
            Debug.Log("attackable set to negative! ");
            moveable = 0;
        }
        if (castable < 0) {
            Debug.Log("castable set to negative! ");
            moveable = 0;
        }

        gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingRight;
    }
}
