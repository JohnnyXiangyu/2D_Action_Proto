using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    // control settings ////////////////////////////////////////
    public KeyCode skillKey1 = KeyCode.Q;
    public KeyCode skillKey2 = KeyCode.A;

    // lifecycle controls //////////////////////////////////////
    public bool actionLock = false;

    // game objects ////////////////////////////////////////////
    public PlayerStatus stats = null;

    // effect interface ////////////////////////////////////////
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


    // skill behavior //////////////////////////////////////////
    private void CastSkill_1() {
        GameController.instance.CastSkill_1();
    }

    private void CastSkill_2() {
        GameController.instance.CastSkill_2();
    }


    // system messages /////////////////////////////////////////
    private void Update() {
        if (actionLock)
            return;

        if (Input.GetKeyDown(skillKey1)) {
            CastSkill_1();
        }
        if (Input.GetKeyDown(skillKey2)) {
            CastSkill_2();
        }
    }
}
