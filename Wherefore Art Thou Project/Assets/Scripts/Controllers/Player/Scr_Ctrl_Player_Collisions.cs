using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Player_Collisions : MonoBehaviour
{

    Scr_Ctrl_Player playerController;


    void Awake()
    {
        playerController = GetComponent<Scr_Ctrl_Player>();
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
        //Yes i also know this is gross, gotta go fast
        switch (_other.tag)
        {
            case "Trail":
                //Go up the heirachy and grab the projectile script
                Scr_Ctrl_Projectile projectile = _other.transform.parent.gameObject.GetComponent<Scr_Ctrl_Projectile>();

                //Compare polarities, kill if not the same
                if (projectile.polarity != playerController.polarity)
                {
                    playerController.Die();
                }
                break;
            case "Wall":
                playerController.Die();
                break;
            case "Enemy":
                playerController.Die();
                break;
        }
    }
}
