using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTemplate : MonoBehaviour
{
    // public parameters /////////////////////////////////////////
    public string skillName;
    public GameObject user = null;

    // public methods ////////////////////////////////////////////
    virtual public void ActivateSkill(GameObject newUser) {
        // pass
    }
}
