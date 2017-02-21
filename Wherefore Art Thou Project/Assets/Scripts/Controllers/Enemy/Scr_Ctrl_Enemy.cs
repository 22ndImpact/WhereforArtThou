using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Enemy : MonoBehaviour
{

    public Scr_Ctrl_Player.Polarity polarity;

    public Material polarityColour_Blue;
    public Material polarityColour_Pink;

    public Scr_Ctrl_Player.LifeState lifeState;

    public enum EnemyState
    {
        Idle,
        Moving,
        enemyStateLength
    }

    public int idleStateWeight;
    public int moveStateWeight;

    public EnemyState enemyState;

    public float movementSpeed;
    public Vector3 direction;

    public float stateChangeTimer;
    public float stateChangeTimeMin;
    public float stateChangeTimeMax;

    public float shootTimer;
    public float shootTimerMin;
    public float shootTimerMax;

    public float projectileChargeMin;
    public float projectileChargeMax;

    public GameObject projectilePrefab;

    public Scr_Ctrl_Level currentLevel;

	// Use this for initialization
	void Start ()
    {
        //Initialize(Scr_Ctrl_Player.Polarity.Blue);
        //Initialize(Scr_Ctrl_Player.Polarity.Pink);
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMovement();
        UpdateStateTimers();

        if(currentLevel == null)
        {
            //For  some reason the first enemy created ni the loop wouldnt register the current level, dirty fix bois
            currentLevel = Scr_GameDirector.inst.currentLevel;
        }
    }

    void UpdateStateTimers()
    {
        //Changing movement state timer
        if(stateChangeTimer > 0)
        {
            stateChangeTimer -= Time.deltaTime;
        }
        else
        {
            stateChangeTimer = Random.Range(stateChangeTimeMin, stateChangeTimeMax);
            ChangeState();
        }

        //Changing shoot timer
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            shootTimer = Random.Range(shootTimerMin, shootTimerMax);
            Shoot();
        }
    }

    void ChangeState()
    {
        int randomStateInt = Random.Range(0, (idleStateWeight + moveStateWeight));

        if(randomStateInt < idleStateWeight)
        {
            enemyState = EnemyState.Idle;
        }
        else
        {
            enemyState = EnemyState.Moving;
        }

        direction = Vector3.zero;

        switch (enemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Moving:
                //50/50 chance to move horizontal or vertical
                if(Random.Range(0,2) == 0)
                {
                    //50/50 chance to move left or right
                    if (Random.Range(0, 2) == 0)
                    {
                        direction.x = 1;
                    }
                    else
                    {
                        direction.x = -1;
                    }
                }
                else
                {
                    //50/50 chance to move up or down
                    if (Random.Range(0, 2) == 0)
                    {
                        direction.z = 1;
                    }
                    else
                    {
                        direction.z = -1;
                    }
                }
                break;
        }
    }

    void UpdateMovement()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

        this.gameObject.transform.position += (movementSpeed * direction * Time.deltaTime);
    }

    public void Initialize(Scr_Ctrl_Player.Polarity _polarity)
    {

        Debug.Log(currentLevel);

        currentLevel = Scr_GameDirector.inst.currentLevel;

        currentLevel.enemyList.Add(this);

        shootTimer = Random.Range(shootTimerMin, shootTimerMax);

        lifeState = Scr_Ctrl_Player.LifeState.Alive;

        polarity = _polarity;

        UpdatePolarity();
    }

    void UpdatePolarity()
    {
        //updates colour based on polarity
        switch (polarity)
        {
            case Scr_Ctrl_Player.Polarity.Blue:
                GetComponent<MeshRenderer>().material = polarityColour_Blue;
                break;
            case Scr_Ctrl_Player.Polarity.Pink:
                GetComponent<MeshRenderer>().material = polarityColour_Pink;
                break;
        }
    }

    public void Die()
    {
        currentLevel.UpdateCUrrentEnemies(-1);
        lifeState = Scr_Ctrl_Player.LifeState.Dead;
        Destroy(this.gameObject);
    }

    void Shoot()
    {
        float chargeAmount = Random.Range(projectileChargeMin, projectileChargeMax);
        //Changes the colour of the reticle based charge percentage
        Color projectileColour = Color.Lerp(Color.white, GetComponent<MeshRenderer>().material.color, chargeAmount);

        //Creates the new projectile object
        GameObject newProjectile = Instantiate<GameObject>(projectilePrefab);

        //Sets the new projectiles initial values
        newProjectile.GetComponent<Scr_Ctrl_Projectile>().Initialization(transform.position, transform.rotation, projectileColour, chargeAmount, polarity);
    }

    void OnTriggerEnter(Collider _other)
    {
        //Yes i also know this is gross, gotta go fast
        switch (_other.tag)
        {
            case "Trail":
                //Go up the heirachy and grab the projectile script
                Scr_Ctrl_Projectile projectile = _other.transform.parent.gameObject.GetComponent<Scr_Ctrl_Projectile>();

                //Compare polarities, kill if not the same
                if (projectile.polarity != polarity)
                {
                    Die();
                }
                break;
            case "Wall":
                //Swap directoin when hitting a wall
                direction *= -1;
                break;
            case "Enemy":
                Die();
                break;
        }
    }
}
