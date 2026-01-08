using System;
using Interfaces;
using UnityEngine;

public class BowAndArrowWeapon : MonoBehaviour,IPickable
{
    private new Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        collider.enabled = !transform.parent;
    }

    public Vector3 GetLocalPositionVector()
    {
        return new Vector3(-0.1f,0-0.75f,-0.5f);
    }
}
