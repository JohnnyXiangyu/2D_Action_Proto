using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement bodies ///////////////////////////////////////////
    public Rigidbody2D rb = null;
    public Collider2D cd = null;

    // movement parameters ///////////////////////////////////////
    public float unitPerSecond = 1; // movement speed
    private Vector2 movement; // it also records where the character is facing 

    // control methods ///////////////////////////////////////////
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;

    // system methods ////////////////////////////////////////////
    private void Awake() {
        movement = new Vector2(0, 0);
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        if (gameObject.GetComponent<PlayerStatus>().moveable != 0) {
            gameObject.GetComponent<Animator>().SetBool("Running", false);
            return;
        }

        movement.x = (Input.GetKey(rightKey) ? 1 : 0) - (Input.GetKey(leftKey) ? 1 : 0);

        if (movement.magnitude == 0)
            gameObject.GetComponent<Animator>().SetBool("Running", false);
        else {
            gameObject.GetComponent<Animator>().SetBool("Running", true);
            rb.velocity = new Vector2(movement.x * unitPerSecond, rb.velocity.y);

            gameObject.GetComponent<PlayerStatus>().isFacingRight = (movement.x > 0) ? true : false;
        }

    }
}
