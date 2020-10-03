using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Authentication;
using UnityEngine;

/// <summary>
/// the basic attack is wrapped in a skill template  
/// 1. freeze movement  
/// 2. (TODO) play animation   
/// 3. count CD
/// </summary>
public class Attack : SkillTemplate {
    // attack parameters ///////////////////////////////////////////
    public float[] damage;
    public float[] range;
    public LayerMask enemyLayer;

    // lifecycle controls //////////////////////////////////////////
    public float[] hardStraightTime;
    public float[] keyFrame;
    private bool hasAttacked = false;

    // combo system ////////////////////////////////////////////////
    public float comboInterval = 0.2f;
    public float maxCombo = 3;
    private int comboCount = 0;

    // user references /////////////////////////////////////////////
    private GameObject userObject = null;
    private PlayerStatus userStatus = null;
    private Rigidbody2D userRB = null;


    // user methods ////////////////////////////////////////////////
    public override void ActivateSkill(GameObject newUser) {
        if (mainTimer == -1) {            
            mainTimer = 0;
            userObject = newUser;
            userStatus = newUser.GetComponent<PlayerStatus>();
            userRB = newUser.GetComponent<Rigidbody2D>();

            // lock behavior
            userStatus.moveable++;
            userStatus.castable++;
            userStatus.attackable++;

            if (userStatus.activateSkill == skillName && Time.time - userStatus.endTime <= comboInterval) {
                comboCount++;
            }
            else {
                comboCount = 0;
            }

            if (comboCount >= maxCombo)
                comboCount = 0;

            userStatus.endTime = -1;
            userStatus.activateSkill = skillName;
            userObject.GetComponent<Animator>().SetTrigger("Attacks" + comboCount);
        }
    }


    // update events ///////////////////////////////////////////////
    public void AttackKeyFrame() {
        hasAttacked = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(userRB.position, range[comboCount], enemyLayer);
        foreach(Collider2D enemy in hits) {
            GameObject enemyObj = enemy.gameObject;
            enemyObj.GetComponent<PlayerStatus>().GetAttack(damage[comboCount]);
        }
    }

    private void ResetToDefault() { // reset skill parameters to default
        userStatus.moveable--;
        userStatus.castable--;
        if (userStatus.attackable > 0) userStatus.attackable--;

        mainTimer = -1;
        hasAttacked = false;

        userStatus.endTime = Time.time;

        userObject = null;
        userRB = null;
        userStatus = null;

        gameObject.SetActive(false);
    }


    // system methods //////////////////////////////////////////////
    private void Start() {
    }

    void FixedUpdate() {
        mainTimer += Time.fixedDeltaTime;

        if (mainTimer >= keyFrame[comboCount] && !hasAttacked) {
            AttackKeyFrame();
        }
        else if (mainTimer >= hardStraightTime[comboCount] - comboInterval && mainTimer <= hardStraightTime[comboCount]) {
            userStatus.attackable--;
        }
        else if (mainTimer >= hardStraightTime[comboCount]) {
            ResetToDefault();
        }
    }
}
