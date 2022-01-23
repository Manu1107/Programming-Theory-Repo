using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    private int health;
    private int healthMax;
    public HealthSystem(int health, int healthMax)
    {
        this.health = health; 
        this.healthMax = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
