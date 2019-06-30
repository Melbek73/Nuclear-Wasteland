using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    // Global Variables

    // Debug
    public const bool ShowDebug = true;
    public static void DebugAfterTime(ref float nextActionTime, float periodInSec, object message)
    {
        if (Globals.ShowDebug)
        {
            nextActionTime += Time.deltaTime;
            
            // All 2 seconds show camera bounds
            if (nextActionTime >= periodInSec)
            {
                // reset
                nextActionTime = nextActionTime % periodInSec;

                Debug.Log(message);
            }
        }
    }

    // Player
    public const float Player_JumpForce = 6;
    public const float Player_Velocity = 3;
    public const int Player_Health = 100;

    // World

    // Platform
    public const float Platform_YAxis = -2.211f;
}
