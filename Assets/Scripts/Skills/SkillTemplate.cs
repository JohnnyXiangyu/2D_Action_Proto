using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTemplate : MonoBehaviour
{
    public GameObject playerObject = null;
    public PlayerStatus stats = null;

    // parameters /////////////////////////////////////////////////////
    public float coolDown = 1;
    public string id = "";

    virtual public void OnEquipment(int skillNum) { }
    virtual public void OnRemove(int skillNum) { }
    virtual public void BeforeAttack() { }
    virtual public void AfterAttack() { }
    virtual public void BeforeHurt() { }
    virtual public void AfterHurt() { }
    virtual public void NextFrame() { }


    private void Awake() {
        playerObject = GameController.instance.playerObject;
        stats = playerObject.GetComponent<PlayerStatus>();
    }
}
