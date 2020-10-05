using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// stores and manages activation of skills (skills themselves will deactivate)
/// </summary>
public class PlayerAttack : MonoBehaviour {
    // game object ////////////////////////////////////////////////////////////
    public Animator playerAnimator = null;
    public PlayerStatus stats = null;

    // effects ////////////////////////////////////////////////////////////////
    public List<GameObject> effectsBeforeAttack;
    public List<GameObject> effectsAfterAttack;

    // properties /////////////////////////////////////////////////////////////
    public float range;
    public float damage;
    public LayerMask enemyLayer;
    public float attackCoolDown;

    // control settings ///////////////////////////////////////////////////////
    public KeyCode attackKey = KeyCode.E;

    // lifecycle control //////////////////////////////////////////////////////
    private float last_attack_time = 0;
    private bool actionLock = false;
    public float upcomingRange = 0;
    public float upcomingDamage = 0;


    // effect interface ///////////////////////////////////////////////////////
    public void Lock(GameObject effect) {
        if (effect == stats.effectTakingOver || effect == gameObject) {
            actionLock = true;
        }
    }

    public void Unlock(GameObject effect) {
        if (effect == stats.effectTakingOver || effect == gameObject) {
            actionLock = false;
        }
    }


    // attack behavior ////////////////////////////////////////////////////////
    public void AttackKeyFrame() {
        if ((last_attack_time == 0 || Time.time - last_attack_time >= attackCoolDown) && !actionLock) {
            last_attack_time = Time.time;

            upcomingDamage = damage;
            upcomingRange = range;

            foreach (GameObject effect in effectsBeforeAttack) {
                if (effect.activeSelf)
                    effect.GetComponent<SkillTemplate>().BeforeAttack();
                else {
                    effectsBeforeAttack.Remove(effect);
                    Destroy(effect);
                }
            }

            Rigidbody2D userRB = gameObject.GetComponent<Rigidbody2D>();

            Collider2D[] hits = Physics2D.OverlapCircleAll(userRB.position, upcomingRange, enemyLayer);
            foreach (Collider2D enemy in hits) {
                GameObject enemyObj = enemy.gameObject;
                if ((enemyObj.transform.position.x - gameObject.transform.position.x) 
                    * (gameObject.GetComponent<PlayerMovement>().rightFacing ? 1 : -1) > 0)
                    enemyObj.GetComponent<EnemyStats>().TakeDamage(upcomingDamage);
            }

            playerAnimator.SetTrigger("Attack");

            foreach (GameObject effect in effectsBeforeAttack) {
                if (effect.activeSelf)
                    effect.GetComponent<SkillTemplate>().BeforeAttack();
                else {
                    effectsBeforeAttack.Remove(effect);
                    Destroy(effect);
                }
            }
        }
    }


    // update utilities ///////////////////////////////////////////////////////
    private void CheckInput() { // check input and execute
        if (Input.GetKeyDown(attackKey)) {
            AttackKeyFrame();
        }
    }


    // system methods /////////////////////////////////////////////////////////
    void Update() {
        CheckInput();
    }
}
