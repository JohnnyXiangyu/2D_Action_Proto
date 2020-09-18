using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Enemy : Actor {
    public float attackDelay = 0.5f; // in seconds
    public float attackKeyFrame = 0.3f; // in seconds

    private Vector2 movement; // it also records where the character is facing
    private float attackTimer = -1;
    private int attackFreq = 0;

    [SerializeField] private string state = "idle";

    private void Awake() {
        movement = new Vector2(0, 0);
    }


    // Update is called once per frame
    void Update() {
    }

    private void FixedUpdate() {
        switch (state) {
            case "idle":
            case "moving":
                // reset other states (if there are), trigger idle animation
                if (movement.magnitude != 0) {
                    state = "moving";
                    rb.MovePosition(rb.position + movement * walkSpeed * Time.fixedDeltaTime);
                    orientation = (movement.x > 0) ? 1 : -1;
                }
                else {
                    state = "idle";
                }
                break;
            case "attack":
                // find enemy in close range, deal damage
                if (attackTimer == -1) {
                    attackTimer = 0;
                }
                else {
                    attackTimer += Time.fixedDeltaTime;
                    if (attackTimer >= attackKeyFrame && attackFreq == 0) {
                        Attack();
                        attackFreq++;
                    }
                    if (attackTimer >= attackDelay) {
                        state = "idle"; // finish attack, return to normal state
                        attackTimer = -1;
                        attackFreq = 0;
                    }
                }
                break;
            default:
                // the hell is this doing?
                Debug.Log("error in input processing");
                break;
        }
    }
}