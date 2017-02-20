using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_ProjectileHead : MonoBehaviour
{

    Scr_Ctrl_Projectile projectile;

    void Awake()
    {
        projectile = transform.parent.GetComponent<Scr_Ctrl_Projectile>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider _other)
    {
        Debug.Log("Hit something");
        #region Hitting other projectile trails
        //If it is a projectile and isnt its own head.
        if (_other.tag == "Wall")
        {
            projectile.StopProjectile();
        }
        #endregion
    }
}
