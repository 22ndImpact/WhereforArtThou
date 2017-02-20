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
    public float currentChargePercentage;
    #endregion

    #region Object References
    Scr_Ctrl_Player playerController;
    public Scr_Ctrl_Reticle reticle;
    public GameObject projectilePrefab;


    #endregion

    void Awake()
    {
        #region Controller Component Initialization
        playerController = GetComponent<Scr_Ctrl_Player>();
        #endregion

        // Records the starting scale of the targeting reticle
        reticle.minReticleXScale = reticle.gameObject.transform.localScale.x;

        // Sets the current charge to the minimum charge
        currentCharge = minimumCharge;
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

        UpdateReticle();


    }

    void Shoot()
    {
        //Creates the new projectile object
        GameObject newProjectile = Instantiate<GameObject>(projectilePrefab);
        //Sets the new projectiles initial values
        newProjectile.GetComponent<Scr_Ctrl_Projectile>().Initialization(reticle.transform.position, this.gameObject.transform.rotation, reticle.meshRenderer.material.color, currentChargePercentage, playerController.polarity);

        //Resets the reload time
        reloadTimer = reloadTime;

        //Reset Charge
        currentCharge = minimumCharge;

        //Set the state to reloading havign now shot the bullet
        shootingState = ShootingState.Reloading;
    }

    void UpdateReloadTimer()
    {
        if (reloadTimer > 0)
        {
            // Reduce timer
            reloadTimer = Mathf.Clamp(reloadTimer - Time.deltaTime, 0, Mathf.Infinity);
            //Changes the colour of the reticle based reload time percentage
            reticle.meshRenderer.material.color = Color.Lerp(reticle.minReloadColour, reticle.maxReloadColour, reloadTimer / reloadTime);
        }
        else
        {
            //Change the state to shooting
            shootingState = ShootingState.Ready;
        }
    }

    void UpdateCharge()
    {
        //Increases the current charge based on charge rate
        currentCharge = Mathf.Clamp(currentCharge + (chargeRate * Time.deltaTime), minimumCharge, maximumCharge);

        //Changes the colour of the reticle based charge percentage
        reticle.meshRenderer.material.color = Color.Lerp(reticle.minChargeColour, reticle.maxChargeColour, currentChargePercentage);
    }

    void UpdateReticle()
    {
        //Determines the current percent charged
        currentChargePercentage = (currentCharge - minimumCharge) / (maximumCharge - minimumCharge);
        //Calculating the current reticle scale based off charge percent
        reticle.currentReticleXScale = reticle.minReticleXScale + ((reticle.maxReticleXScale - reticle.minReticleXScale) * currentChargePercentage);


        //Applying the calculated scale to the reticle transform
        reticle.gameObject.transform.localScale = new Vector3(reticle.currentReticleXScale, reticle.gameObject.transform.localScale.y, reticle.transform.localScale.z);
    }
}
