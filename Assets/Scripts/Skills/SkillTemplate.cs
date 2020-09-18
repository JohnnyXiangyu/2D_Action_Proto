using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTemplate : MonoBehaviour
{
    // public parameters /////////////////////////////////////////
    public string skillName;
    public string description;
    public GameObject user = null;

    // lifecycle /////////////////////////////////////////////////
    public float mainTimer = -1;

    // public methods ////////////////////////////////////////////
    virtual public void ActivateSkill(GameObject newUser) {
        // pass
    }
}
