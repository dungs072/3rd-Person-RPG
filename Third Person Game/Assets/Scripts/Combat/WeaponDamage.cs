using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    private List<Collider> alreadyCollidedWith = new List<Collider>();
    private int currentDamage;
    private float knockBack;
    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }
        if (alreadyCollidedWith.Contains(other)) { return; }
        alreadyCollidedWith.Add(other);
        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(currentDamage);
        }
        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = other.transform.position - myCollider.transform.position;
            forceReceiver.AddForce(direction*knockBack);
        }
    }
    public void SetDamage(int damage,float knockBack)
    {
        currentDamage = damage;
        this.knockBack = knockBack;
    }
}
