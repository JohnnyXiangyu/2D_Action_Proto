using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // global settings ///////////////////////////////////////////
    public LayerMask[] landscape; // put any layer that might count as ground here


    // movement bodies ///////////////////////////////////////////
    public Rigidbody2D rb = null;
    public Collider2D cd = null;
    public Animator playerAnimator = null;
    public PlayerStatus stats = null;

    // movement parameters ///////////////////////////////////////
    public float unitPerSecond = 1; // movement speed
    public float jumpForce = 10; // force applied when player jumps
    public float dashSpeed = 2;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1;
    private Vector2 movement;
    public bool rightFacing = true;

    // control methods ///////////////////////////////////////////
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode dashKey = KeyCode.Space;

    // action locks //////////////////////////////////////////////
    private bool actionLock = false;
    private bool isDashing = false;
    private float dashStartTime = 0;


    // utilities /////////////////////////////////////////////////
    public bool IsGrounded() {
        bool res = false;
        foreach (LayerMask layer in landscape) {
            res = res | Physics2D.Raycast(
                (Vector2)transform.position + cd.offset * transform.localScale.y, Vector2.down, cd.bounds.extents.y /** transform.localScale.y */+ 0.1f, layer);
        }

        playerAnimator.SetBool("Grounded", res);
        return res;
    }

    //public bool IsOnWall(int x_direction) {
    //    bool res = false;
    //    foreach (LayerMask layer in landscape) {
    //        res = res | Physics2D.Raycast((Vector2)transform.position + cd.offset, Vector2.down, cd.bounds.extents.y + 0.1f, layer);
    //    }

    //    playerAnimator.SetBool("Grounded", res);
    //    return res;
    //}

    public void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        gameObject.GetComponent<Animator>().SetTrigger("Jump");
    }


    // effect interface //////////////////////////////////////////
    public void Lock(GameObject effect) {
        if (stats.effectTakingOver == effect || effect == gameObject) {
            actionLock = true;
        }
    }

    public void Unlock(GameObject effect) {
        if (stats.effectTakingOver == effect || effect == gameObject) {
            actionLock = false;
        }
    }


    // system methods ////////////////////////////////////////////
    private void Awake() {
        movement = new Vector2(0, 0);
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        IsGrounded();

        movement.x = (Input.GetKey(rightKey) ? 1 : 0) - (Input.GetKey(leftKey) ? 1 : 0);
        movement.y = Input.GetKey(upKey) ? 1 : 0;
        // Debug.Log(movement);

        // dodging
        if (Input.GetKeyDown(dashKey) && !isDashing && Time.time - dashStartTime >= dashCooldown && !actionLock) {
            isDashing = true;
            actionLock = true;

            gameObject.GetComponent<PlayerAttack>().Lock(gameObject);
            gameObject.GetComponent<PlayerSkills>().Lock(gameObject);

            gameObject.GetComponent<Animator>().SetTrigger("Dodge");
            dashStartTime = Time.time;
            gameObject.GetComponent<Collider2D>().enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (isDashing) {
            rb.velocity = new Vector2(dashSpeed * (rightFacing ? 1 : -1), 0);

            if (Time.time - dashStartTime >= dashDuration) {
                isDashing = false;
                actionLock = false;

                gameObject.GetComponent<PlayerAttack>().Unlock(gameObject);
                gameObject.GetComponent<PlayerSkills>().Unlock(gameObject);

                gameObject.GetComponent<Collider2D>().enabled = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(0, 0);
            }
        }

        // running 
        if (!actionLock) {
            if (movement.x == 0) {
                gameObject.GetComponent<Animator>().SetBool("Running", false);
            }
            else if (!isDashing) {
                rb.velocity = new Vector2(unitPerSecond * movement.x, rb.velocity.y);
                rightFacing = (movement.x > 0) ? true : false;
                playerAnimator.SetBool("RightFacing", rightFacing);
                playerAnimator.SetBool("Running", true);
            }
        }

        // jumping
        if (movement.y > 0 && IsGrounded() && !isDashing && !actionLock) Jump();
    }
}
