using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    // physics object containers ////////////////////////////////
    public CapsuleCollider2D myCollider = null;
    public Rigidbody2D rb = null;

    // parameters shared by player and all enemies //////////////
    public float walkSpeed = 2f;
    public bool moveable = true; // if the character can move
    public float damage = 50;
    public float attackRange = 10;

    public LayerMask blockingLayer;

    public int orientation;

    public void Attack() {
    }
}
