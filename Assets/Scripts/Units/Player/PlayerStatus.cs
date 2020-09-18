using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// container object of player status TODO: how to hook this to game manager?
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    // resources //////////////////////////////////////////////////////////////
    public float health = 100;
    public float mana = 100;

    // binary states //////////////////////////////////////////////////////////
    public int moveable = 0;
    public int attackable = 0;
    public int castable = 0;


    // system methods /////////////////////////////////////////////////////////
    void Start()
    {
        
    }

    void Update()
    {
        if (moveable < 0) {
            Debug.Log("movable set to negative! ");
            moveable = 0;
        }
        if (attackable < 0) {
            Debug.Log("attackable set to negative! ");
            moveable = 0;
        }
        if (castable < 0) {
            Debug.Log("castable set to negative! ");
            moveable = 0;
        }
    }
}
