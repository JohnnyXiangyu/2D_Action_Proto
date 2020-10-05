using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // parameters ////////////////////////////////////////////////////
    public float health = 100f;

    // effects ///////////////////////////////////////////////////////
    public List<GameObject> effectsNextFrame = new List<GameObject>(); // will execute in lateUpdate
    public GameObject effectTakingOver = null; // will execute after effectsNextFrame, and is a singleton
    public List<GameObject> effectsBeforeHurt = new List<GameObject>();
    public List<GameObject> effectsAfterHurt = new List<GameObject>();
    // public List<GameObject> effectsLasting = new List<GameObject>();

    // temperory numbers for each damage /////////////////////////////
    public float upcomingDamage = 0;


    // interfaces ////////////////////////////////////////////////////
    public void TakeDamage(float rawDamage) {
        upcomingDamage = rawDamage;

        foreach (GameObject effect in effectsBeforeHurt) {
            if (effect)
                effect.GetComponent<SkillTemplate>().BeforeHurt();
        }

        health -= upcomingDamage;
        if (health <= 0) {
            health = 0;
            Die();
        }

        foreach (GameObject effect in effectsAfterHurt) {
            if (effect)
                effect.GetComponent<SkillTemplate>().AfterHurt();
        }
    }

    public void GetHeal(float rawHealing) {
        // add effects here if needed
        health += rawHealing;
    }

    public void Die() {
        Debug.Log("player dies");
        // TODO: communicate with gamecontroller
    }


    // system message ////////////////////////////////////////////////
    private void Start() {
        GameController.instance.playerObject = gameObject;
    }

    private void LateUpdate() {
        foreach (GameObject effect in effectsNextFrame) {
            if (effect) {
                effect.GetComponent<SkillTemplate>().NextFrame();
            }
        }

        // sanitize immediate effects
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject effect in effectsNextFrame) {
            if (effect) {
                temp.Add(effect);
            }
        }
        effectsNextFrame = temp;

        if (effectTakingOver) {
            effectTakingOver.GetComponent<SkillTemplate>().NextFrame();
        }
        else {
            effectTakingOver = null;
        }
    }
}
