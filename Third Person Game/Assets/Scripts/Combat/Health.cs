using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public event Action OnTakeDamage;
    public event Action OnDie;
    private int health;
    private bool isVulnerable;
    public bool IsDead => health == 0;
    private void Start()
    {
        health = maxHealth;
    }
    public void SetVulnerable(bool isVulnerable)
    {
        this.isVulnerable = isVulnerable;
    }
    public void DealDamage(int damageAmount)
    {
        if (health == 0) { return; }
        if(isVulnerable) { return; }
        health = Mathf.Max(health-damageAmount,0);
        OnTakeDamage?.Invoke();
        if (health == 0)
        {
            OnDie?.Invoke();
        }
        print(health);
    }
}
