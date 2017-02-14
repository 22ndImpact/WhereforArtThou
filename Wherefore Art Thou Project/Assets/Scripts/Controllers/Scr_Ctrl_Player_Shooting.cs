using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Player_Shooting : MonoBehaviour
{
    #region Variables
    
    public enum ShootingState
    {
        Ready,
        Charging,
        Reloading
    }

    public ShootingState shootingState = ShootingState.Ready;

    public float reloadTime;
    public float reloadTimer;

    public float currentCharge;
    public float minimumCharge;
    public float maximumCharge;
    public float chargeRate;

    #endregion

    #region Object References
    Scr_Ctrl_Player playerController;
    #endregion

    void Awake()
    {
        #region Controller Component Initialization
        playerController = GetComponent<Scr_Ctrl_Player>();
        #endregion
    }

    public void UpdateShootingState()
    {
        switch (shootingState)
        {
            case ShootingState.Ready:

                //If the shot is ready and input is down, change state
                if (playerController.inputController.getShootingInput())
                {
                    shootingState = ShootingState.Charging;
                }

                break;
            case ShootingState.Charging:

                //If the player is holind down shoot
                if(playerController.inputController.getShootingInput())
                {
                    UpdateCharge();
                }
                //If the player isnt holding down shoot
                else
                {
                    //Fire the shot
                    Shoot();
                }

                break;
            case ShootingState.Reloading:
                //The reload timer only reduces when the player is reloading
                UpdateReloadTimer();
                break;
        }
    }

    void Shoot()
    {




        //Sets the reload time
        reloadTimer = reloadTime;

        //Reset Charge
        currentCharge = minimumCharge;

        //Set the state to reloading havign now shot the bullet
        shootingState = ShootingState.Reloading;
    }

    void UpdateReloadTimer()
    {
        // Reduce timer
        reloadTimer =  Mathf.Clamp(reloadTimer - Time.deltaTime, 0, Mathf.Infinity);

        if(reloadTimer <= 0)
        {
            shootingState = ShootingState.Ready;
        }
    }

    void UpdateCharge()
    {
        currentCharge = Mathf.Clamp(currentCharge + (chargeRate * Time.deltaTime), minimumCharge, maximumCharge);
    }
}
