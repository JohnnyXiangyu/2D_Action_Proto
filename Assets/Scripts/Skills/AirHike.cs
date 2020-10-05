using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHike : SkillTemplate
{
    public int usageLimit = 1;
    private int timeUsedInAir = 0;
    private KeyCode jumpKey;
    private KeyCode originalSkillKey;
    
    public override void NextFrame() {
        if (playerObject.GetComponent<PlayerMovement>().IsGrounded()) {
            Destroy(gameObject);
        }
        else if (timeUsedInAir < usageLimit && Input.GetKeyDown(jumpKey)) {
            playerObject.GetComponent<PlayerMovement>().Jump();
            timeUsedInAir++;
        }
    }

    public override void OnEquipment(int skillNum) {
        jumpKey = GameController.instance.playerObject.GetComponent<PlayerMovement>().upKey;

        // the object is not yet instantiated
        if (skillNum == 1) {
            originalSkillKey = GameController.instance.playerObject.GetComponent<PlayerSkills>().skillKey1;
            GameController.instance.playerObject.GetComponent<PlayerSkills>().skillKey1 = jumpKey;
        }
        else if (skillNum == 2) {
            originalSkillKey = GameController.instance.playerObject.GetComponent<PlayerSkills>().skillKey2;
            GameController.instance.playerObject.GetComponent<PlayerSkills>().skillKey2 = jumpKey;
        }
    }

    public override void OnRemove(int skillNum) {
        if (skillNum == 1) {
            GameController.instance.playerObject.GetComponent<PlayerSkills>().skillKey1 = originalSkillKey;
        }
        else if (skillNum == 2) {
            GameController.instance.playerObject.GetComponent<PlayerSkills>().skillKey2 = originalSkillKey;
        }
    }

    // system message ///////////////////////////////////////////
    private void Start() {
        jumpKey = GameController.instance.playerObject.GetComponent<PlayerMovement>().upKey;

        foreach (GameObject effect in stats.effectsNextFrame) {
            if (effect && effect.GetComponent<SkillTemplate>().id == id) {
                Destroy(gameObject);
                return;
            }
        }

        stats.effectsNextFrame.Add(gameObject);
    }
}
