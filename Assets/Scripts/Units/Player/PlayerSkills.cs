using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
    /// stores and manages activation of skills (skills themselves will deactivate)
/// </summary>
public class PlayerSkills : MonoBehaviour
{
    // skill prefabs //////////////////////////////////////////////////////////
    public GameObject attack; // bonded to attack button
    public GameObject[] skills;

    // control settings ///////////////////////////////////////////////////////
    public KeyCode attackKey = KeyCode.E;
    public KeyCode[] skillKeys;


    // modify skillset ////////////////////////////////////////////////////////
    public void RemoveSkill(int index) { // remove skill by index
        if (index < skills.Length)
            skills[index] = null;
    }

    public void AddSkill(GameObject newSkill) { // add skill at the first empty slot
        int i = 0;
        foreach (GameObject obj in skills) {
            if (obj == null) {
                skills[i] = newSkill;
            }
            i++;
        }
    }


    // update utilities ///////////////////////////////////////////////////////
    private void CheckInput() { // check input and execute
        
        if (Input.GetKeyDown(attackKey) && gameObject.GetComponent<PlayerStatus>().attackable == 0) {
            if (!attack.activeSelf)
                attack.SetActive(true);
            attack.GetComponent<SkillTemplate>().ActivateSkill(gameObject);
        }

        for (int i = 0; i < skills.Length; i++) {
            if (Input.GetKeyDown(skillKeys[i]) && gameObject.GetComponent<PlayerStatus>().castable == 0) {
                if (!skills[i].activeSelf)
                    skills[i].SetActive(true);
                // TODO: call active
            }
        }
    }


    // system methods /////////////////////////////////////////////////////////
    void Start()
    {
        
    }

    void Update()
    {
        CheckInput();
    }
}
