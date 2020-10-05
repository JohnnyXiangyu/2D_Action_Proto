using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : SkillTemplate
{
    // lifecycle controls //////////////////////////////////////////
    public float duration = 2;
    private float startTime = -1000000;
    
    public override void NextFrame() {
        if (startTime < 0) {
            startTime = Time.time;
            playerObject.GetComponent<Animator>().SetTrigger("Block");
            playerObject.GetComponent<PlayerAttack>().Lock(gameObject);
            playerObject.GetComponent<PlayerMovement>().Lock(gameObject);
            playerObject.GetComponent<PlayerSkills>().Lock(gameObject);
        }
        else if (Time.time - startTime >= duration - float.Epsilon) {
            startTime = -1000000;
            playerObject.GetComponent<PlayerAttack>().Unlock(gameObject);
            playerObject.GetComponent<PlayerMovement>().Unlock(gameObject);
            playerObject.GetComponent<PlayerSkills>().Unlock(gameObject);

            Destroy(gameObject);
        }
    }

    public override void BeforeHurt() {
        stats.upcomingDamage = 0;
    }


    // system message //////////////////////////////////////////////
    private void Start() {
        if (!stats.effectTakingOver) {
            stats.effectTakingOver = gameObject;
            // stats.effectsNextFrame.Add(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
