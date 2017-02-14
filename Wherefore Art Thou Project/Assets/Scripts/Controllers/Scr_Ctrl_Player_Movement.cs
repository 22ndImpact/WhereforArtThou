using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Player_Movement : MonoBehaviour
{
    #region Variables
    public float movementSpeed;
    #endregion

    // Used to adjust the position of the player
    public void UpdatePlayerPosition (Vector3 _inputVector)
    {
        this.gameObject.transform.position += (movementSpeed * _inputVector * Time.deltaTime);
	}

    // Used to adjust the rotation of the player based on mouse position
    public void UpdatePlayerRotation (Vector3 _mouseWorldPosition)
    {
        this.gameObject.transform.LookAt(_mouseWorldPosition);
    }
}
