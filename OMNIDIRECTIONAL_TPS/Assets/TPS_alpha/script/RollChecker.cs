using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollChecker : MonoBehaviour
{
    public PlayerController playerController;
    
    void RollStart()
    {
        playerController.rolling = true;
    }

    void RollEnd()
    {
        playerController.rolling = false;
    }

}
