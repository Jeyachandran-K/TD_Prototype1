using System;
using UnityEngine;

public class StickWeapon : MonoBehaviour
{
    [SerializeField] private float hitForce;
    private Rigidbody stickWeaponRigidbody;

    private void Awake()
    {
        stickWeaponRigidbody =  GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Vector3 hitDir = collision.contacts[0].normal * -1f;
            collision.rigidbody.AddForce(hitDir * hitForce, ForceMode.Impulse);
        }
    }
}
