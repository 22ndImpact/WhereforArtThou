using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Player : MonoBehaviour
{
    #region Variables
    public enum Polarity
    {
        Blue,
        Pink,
        polarityLength
    }

    public Polarity polarity;

    public Color polarityColour_Blue;
    public Color polarityColour_Pink;

    public enum LifeState
    {
        Alive,
        Dead,
        lifeStateLength
    }

    public LifeState lifeState = LifeState.Alive;
    #endregion

    #region Object References
    [HideInInspector]
    public Scr_Ctrl_Player_Input inputController;
    [HideInInspector]
    public Scr_Ctrl_Player_Movement movementController;
    [HideInInspector]
    public Scr_Ctrl_Player_Shooting shootingController;
    [HideInInspector]
    public Scr_Ctrl_Player_Collisions collisionController;
    #endregion

    void Awake()
    {
        #region Controller Component Initialization
        inputController = GetComponent<Scr_Ctrl_Player_Input>();
        movementController = GetComponent<Scr_Ctrl_Player_Movement>();
        shootingController = GetComponent<Scr_Ctrl_Player_Shooting>();
        collisionController = GetComponent<Scr_Ctrl_Player_Collisions>();
        #endregion

        UpdatePolarity();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateMovement();
        UpdateShooting();
        CheckPolarity();
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

    void CheckPolarity()
    {
        //Checks input to see if a poloarity switch is triggered and the player isnt charging a shot
        if(inputController.getPolaritySwitchDown() && shootingController.shootingState != Scr_Ctrl_Player_Shooting.ShootingState.Charging)
        {
            polarity++;

            if (polarity == Polarity.polarityLength)
                polarity = 0;

            UpdatePolarity();
        }
    }

    void UpdatePolarity()
    {
        

        //updates colour based on polarity
        switch (polarity)
        {
            case Polarity.Blue:
                GetComponent<MeshRenderer>().material.color = polarityColour_Blue;
                shootingController.reticle.maxChargeColour = polarityColour_Blue;
                shootingController.reticle.maxReloadColour = polarityColour_Blue;
                break;
            case Polarity.Pink:
                GetComponent<MeshRenderer>().material.color = polarityColour_Pink;
                shootingController.reticle.maxChargeColour = polarityColour_Pink;
                shootingController.reticle.maxReloadColour = polarityColour_Pink;
                break;
        }
    }

    public void Die()
    {
        lifeState = Scr_Ctrl_Player.LifeState.Dead;
        Debug.Log("You Died");
        Scr_GameDirector.inst.LoadScene("Sce_GameOver");

    }


}
