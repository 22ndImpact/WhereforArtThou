using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Player_Input : MonoBehaviour
{
    // Returns a normalized vector of horizontal and vertical input vectors
    public Vector3 getMovementInputVector()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0,  Input.GetAxis("Vertical")).normalized;
    }

    // Returns the position in the world space on a X/Z axis
    public Vector3 getMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Resets Y to 0 as the game is played on a X/Z axis
        mouseWorldPosition.y = 0;
        return mouseWorldPosition;
    }

    // Returns the state of the shooting button
    public bool getShootingInput()
    {
        return Input.GetMouseButton(0);
    }

    public bool getPolaritySwitchDown()
    {
        return Input.GetMouseButtonDown(1);
    }
}
