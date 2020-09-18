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
public class Attack : SkillTemplate
{
    // attack parameters ///////////////////////////////////////////
    public float damage = 10;
    public float range = 1.1f;
    public LayerMask enemyLayer;
    
    // lifecycle controls //////////////////////////////////////////
    public float[] hardStraightTime;
    public float[] keyFrame;
    public float comboInterval = 0.2f;
    public int maxCombo = 3;

    private float mainTimer = -1;
    private bool hasAttacked = false;
    private int comboIndex = 0;
    private int nextCombo = 0;

    // user references /////////////////////////////////////////////
    private GameObject userObject = null;
    private PlayerStatus userStatus = null;
    private Rigidbody2D userRB = null;
    // private Collider2D userCD = null;


    // user methods ////////////////////////////////////////////////
    public override void ActivateSkill(GameObject newUser) {
        if (mainTimer == -1) {
            mainTimer = 0;
            userObject = newUser;
            userStatus = newUser.GetComponent<PlayerStatus>();
            userRB = newUser.GetComponent<Rigidbody2D>();
            // userCD = newUser.GetComponent<Collider2D>();

            // lock behavior
            userStatus.moveable++;
            userStatus.castable++;

            userObject.GetComponent<Animator>().SetTrigger("Attacks");
        }
        else if (mainTimer > hardStraightTime[comboIndex] * 0.2f) {
            if (comboIndex + 1 < maxCombo) {
                nextCombo++;
            }
        }
    }


    // update events ///////////////////////////////////////////////
    public void AttackKeyFrame() {
        hasAttacked = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(userRB.position, range, enemyLayer);
        foreach(Collider2D enemy in hits) {
            GameObject enemyObj = enemy.gameObject;
            enemyObj.GetComponent<PlayerStatus>().GetAttack(damage);
        }
    }

    private void ResetToDefault() { // reset skill parameters to default
        userStatus.moveable--;
        userStatus.castable--;

        mainTimer = -1;
        hasAttacked = false;
        comboIndex = 0;
        nextCombo = 0;

        gameObject.SetActive(false);
    }


    // system methods //////////////////////////////////////////////
    private void Start() {
        skillName = "ATTACK";
        Debug.Log(Time.fixedDeltaTime);
    }
    void FixedUpdate() {
        mainTimer += Time.fixedDeltaTime;

        if (mainTimer >= keyFrame[comboIndex] && !hasAttacked) {
            AttackKeyFrame();
        }
        else if (mainTimer >= hardStraightTime[comboIndex]) {
            if (nextCombo == comboIndex + 1) {
                comboIndex++;
                mainTimer = 0;
                userObject.GetComponent<Animator>().SetTrigger("Attacks");
                hasAttacked = false;
            }
            else {
                ResetToDefault();
            }
        }
    
    }
}
