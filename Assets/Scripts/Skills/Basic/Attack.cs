using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// the basic attack is wrapped in a skill template  
/// 1. freeze movement  
/// 2. (TODO) play animation   
/// 3. count CD
/// </summary>
public class Attack : SkillTemplate
{
    // attack parameters ///////////////////////////////////////////
    public float damage = 10;
    public float range = 1.1f;
    public LayerMask enemyLayer;
    
    // lifespan controls ///////////////////////////////////////////
    public float hardStraightTime = 0.5f;
    public float keyFrame = 0.2f;
    private float mainTimer = -1;
    private int attackTimer = 0;

    // user references /////////////////////////////////////////////
    private GameObject userObject = null;
    private PlayerStatus userStatus = null;
    private Rigidbody2D userRB = null;
    private Collider2D userCD = null;


    // user methods ////////////////////////////////////////////////
    public override void ActivateSkill(GameObject newUser) {
        if (mainTimer == -1) {
            mainTimer = 0;
            userObject = newUser;
            userStatus = newUser.GetComponent<PlayerStatus>();
            userRB = newUser.GetComponent<Rigidbody2D>();
            userCD = newUser.GetComponent<Collider2D>();

            // lock behavior
            userStatus.moveable++;
            userStatus.castable++;

            userObject.GetComponent<Animator>().SetTrigger("Attacks");
        }
    }


    // update events ///////////////////////////////////////////////
    public void AttackKeyFrame() {
        attackTimer++;
        Collider2D[] hits = Physics2D.OverlapCircleAll(userRB.position, range, enemyLayer);
        foreach(Collider2D enemy in hits) {
            GameObject enemyObj = enemy.gameObject;
            enemyObj.GetComponent<PlayerStatus>().GetAttack(damage);
        }
    }

    private void ResetToDefault() { // reset skill parameters to default
        userStatus.moveable--;
        userStatus.castable--;
        attackTimer = 0;
        mainTimer = -1;

        gameObject.SetActive(false);
    }


    // system methods //////////////////////////////////////////////
    private void Start() {
        skillName = "ATTACK";
        Debug.Log(Time.fixedDeltaTime);
    }
    void FixedUpdate() {
        mainTimer += Time.fixedDeltaTime;
        if (mainTimer >= keyFrame && attackTimer <= 0) {
            AttackKeyFrame();
        }
        else if (mainTimer >= hardStraightTime)
            ResetToDefault();
    }
}
