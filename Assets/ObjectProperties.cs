using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperties : MonoBehaviour {

    public MaterialType materialType;

    public GameObject impactEffect;
}

public enum MaterialType
{
    Metal,
    Wood,
    Glass,
    Stone
}
