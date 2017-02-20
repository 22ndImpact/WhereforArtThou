using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Projectile : MonoBehaviour
{
    #region Variables

    public Scr_Ctrl_Player.Polarity polarity;

    public GameObject projectileHead;
    MeshRenderer projHeadRenderer;

    public GameObject projectileTrail;
    Transform projTrailTransform;

    public GameObject projectileTrailMesh;
    MeshRenderer projTrailRenderer;

    public float speed;

    public float baseDuration;
    public float durationTimer;

    Color currentColour;

    float distanceTraveled;

    Vector3 previousPosition;

    public float chargePercent;

    public List<Transform> CollisionIgnore;

    #endregion

    void Awake()
    {
        projHeadRenderer = projectileHead.GetComponent<MeshRenderer>();

        projTrailTransform = projectileTrail.transform;

        projTrailRenderer = projectileTrailMesh.GetComponent<MeshRenderer>();
    }

    //Uses rotation to aim projectile
    public void Initialization(Vector3 _position, Quaternion _rotation, Color _colour, float _chargePercent, Scr_Ctrl_Player.Polarity _polarity)
    {
        //Sets the rotation given by the instantiator
        transform.localRotation = _rotation;
        transform.position = _position;
        //Sets the head Colour
        projHeadRenderer.material.color = _colour;
        //Sets the trail colour
        projTrailRenderer.material.color = _colour;
        //Starts the duratoin timer
        durationTimer = baseDuration * _chargePercent;
        //Set the polarity of the shot
        polarity = _polarity;
        chargePercent = _chargePercent;
    }

    //Uses target instead of rotation
    public void Initialization(Vector3 _position, Transform _target, Color _colour, float _chargePercent, Scr_Ctrl_Player.Polarity _polarity)
    {
        Debug.Log("target position" + _target.position);

        transform.LookAt(_target.transform.position);

        Debug.Log("projectile rotation" + transform.localRotation.eulerAngles);

        transform.position = _position;
        //Sets the head Colour
        projHeadRenderer.material.color = _colour;
        //Sets the trail colour
        projTrailRenderer.material.color = _colour;
        //Starts the duratoin timer
        durationTimer = baseDuration * _chargePercent;
        //Set the polarity of the shot
        polarity = _polarity;
        chargePercent = _chargePercent;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateMovement();
        UpdateTrail();
	}

    void UpdateMovement()
    {
        //If the proctile is active
        if(durationTimer > 0)
        {
            //Adjusts the position of the projectile every frame
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            
            //Reducethe duratoin time
            durationTimer -= Time.deltaTime;
        }
        else
        {
            StopProjectile();
        }
    }

    void UpdateTrail()
    {
        #region Scale Adjustment
        //Extract the scale variable
        Vector3 TempScale = projTrailTransform.localScale;

        //Determine the distance traveled since last frame and increase the scale by that much
        float distanceTraveled = (transform.position - previousPosition).magnitude;
        //Takes the scale of the object into account too, to allow scalign of game objects
        TempScale.z += distanceTraveled / transform.localScale.x;

        //Apply the tempScale back to the object
        projTrailTransform.localScale = TempScale;
        #endregion
    }

    void LateUpdate()
    {
        //Sets the previous positoin to enable distance tracking
        previousPosition = transform.position;
    }

    public void DestroySelf()
    {

    }

    public void StopProjectile()
    {
        //Turn off the projectile head
        projectileHead.SetActive(false);
        durationTimer = 0;
    }
}
