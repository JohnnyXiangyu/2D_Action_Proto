using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the system that controls all dropped skills
/// </summary>
public class SkillPickupSystem : MonoBehaviour
{
    // singleton instance ///////////////////////////
    public static SkillPickupSystem instance = null;
    
    // system methods ///////////////////////////////
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
