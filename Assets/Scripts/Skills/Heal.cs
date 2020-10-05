using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SkillTemplate
{

    // parameter ////////////////////////////////////////////////////////
    public float healAmount = 10;
    
    public override void NextFrame() {
        // heal the player
        stats.GetHeal(healAmount);
        Destroy(gameObject);
    }

    // system message ///////////////////////////////////////////////////
    private void Start() {
        foreach (GameObject effect in stats.effectsNextFrame) {
            if (effect && effect.GetComponent<SkillTemplate>().id == id) {
                Destroy(gameObject);
                return;
            }
        }
        
        stats.effectsNextFrame.Add(gameObject);
    }
}
