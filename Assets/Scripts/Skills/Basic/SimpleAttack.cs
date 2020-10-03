using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SimpleAttack : SkillTemplate
{
    // attack parameters ///////////////////////////////////////////
    public float damage;
    public float range;
    public LayerMask enemyLayer;
    public float timeUntilHit;
    public float coolDown;

    // user references /////////////////////////////////////////////
    public GameObject userObject = null;
    public PlayerStatus userStatus = null;
    public Rigidbody2D userRB = null;

    // utils ///////////////////////////////////////////////////////
    private float startTime;
    private int attackCount = 0;


    // attack behavior /////////////////////////////////////////////
    public void AttackKeyFrame() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(userRB.position, range, enemyLayer);
        foreach (Collider2D enemy in hits) {
            GameObject enemyObj = enemy.gameObject;
            enemyObj.GetComponent<PlayerStatus>().GetAttack(damage);
        }

        userObject.GetComponent<Animator>().SetTrigger("Attacks0");

        Debug.Log("attack");
    }


    // system messages //////////////////////////////////////////////
    private void Awake() {
        startTime = Time.time;
    }

    private void Start() {
        userObject = user;
        userStatus = userObject.GetComponent<PlayerStatus>();
        userRB = userObject.GetComponent<Rigidbody2D>();

        userStatus.attackable++;
        userStatus.moveable++;
        userStatus.castable++;
    }

    private void Update() {
        if (Time.time - startTime >= timeUntilHit && attackCount < 1) {
            attackCount++;
            AttackKeyFrame();
        }
        if (Time.time - startTime >= coolDown) {
            userStatus.attackable--;
            userStatus.moveable--;
            userStatus.castable--;

            Destroy(gameObject);
        }
    }
}
