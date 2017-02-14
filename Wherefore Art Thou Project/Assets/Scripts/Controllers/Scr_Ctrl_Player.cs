using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Player : MonoBehaviour
{
    #region Variables

    #endregion

    #region Object References
    [HideInInspector]
    public Scr_Ctrl_Player_Input inputController;
    [HideInInspector]
    public Scr_Ctrl_Player_Movement movementController;
    [HideInInspector]
    public Scr_Ctrl_Player_Shooting shootingController;
    #endregion

    void Awake()
    {
        #region Controller Component Initialization
        inputController = GetComponent<Scr_Ctrl_Player_Input>();
        movementController = GetComponent<Scr_Ctrl_Player_Movement>();
        shootingController = GetComponent<Scr_Ctrl_Player_Shooting>();
        #endregion
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateMovement();
        UpdateShooting();
	}

    void UpdateMovement()
    {
        //Updates the player characters position
        movementController.UpdatePlayerPosition(inputController.getMovementInputVector());

        //Updates the player rotation to face mouse position
        movementController.UpdatePlayerRotation(inputController.getMouseWorldPosition());
    }

    void UpdateShooting()
    {
        shootingController.UpdateShootingState();
    }
}
