using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BubbleShield : SkillTemplate
{
    public GameObject diedBubble = null;
    public Vector3 positionOffset = new Vector3(0, -0.27f, -1);
    public Vector3 scaleOffset = new Vector3(1.5f, 1.5f, 1);
    
    // effects /////////////////////////////////////////
    public override void BeforeHurt() {
        // the skill will avoid a single hurt
        stats.upcomingDamage = 0;

        // destroy self
        Instantiate(diedBubble, GameController.instance.playerObject.transform.position + positionOffset, Quaternion.identity)
            .transform.localScale = scaleOffset;
        Destroy(gameObject); // why are they still in the list, just to suffer
    }


    // system messages /////////////////////////////////
    private void Start() {
        foreach (GameObject effect in stats.effectsBeforeHurt) {
            if (effect && effect.GetComponent<SkillTemplate>().id == id) {
                Destroy(gameObject);
                return;
            }
        }
         
        stats.effectsBeforeHurt.Add(gameObject);

        gameObject.transform.position = GameController.instance.playerObject.transform.position + positionOffset;
        gameObject.transform.localScale = scaleOffset;
    }

    private void Update() {
        gameObject.transform.position = GameController.instance.playerObject.transform.position + positionOffset;
        gameObject.transform.localScale = scaleOffset;
    }
}
