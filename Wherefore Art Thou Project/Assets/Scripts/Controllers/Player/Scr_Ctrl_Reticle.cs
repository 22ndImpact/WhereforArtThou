using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ctrl_Reticle : MonoBehaviour
{
    public Scr_Ctrl_Player playerController;

    public Color minChargeColour;
    public Color maxChargeColour;

    public Color minReloadColour;
    public Color maxReloadColour;

    public float minReticleXScale;
    public float maxReticleXScale;
    public float currentReticleXScale;

    public MeshRenderer meshRenderer;

    void Awake()
    {
        //Links the mesh rendered
        meshRenderer = GetComponent<MeshRenderer>();

        //Sets the default colour
        meshRenderer.material.color = minChargeColour;
    }
}
