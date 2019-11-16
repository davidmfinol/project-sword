using UnityEngine;
using System;

public class HealthModule : MonoBehaviour
{
    public event Action onDeath;
    public event Action onHealthIncremented;
    public event Action onHealthDecremented;
    public delegate void OnHealthChanged(float value);
    public event OnHealthChanged onHealthChanged;

    public float maxHealth;
    public float currentHealth;
    private float oldCurrentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        oldCurrentHealth = currentHealth;
    }

    void OnEnable()
    {
        onHealthChanged += IncrementHealth;
        onHealthChanged += DecrementHealth;
        onHealthChanged += IncrementMaxHealth;
        onHealthChanged += DecrementMaxHealth;
    }

    void OnDisable()
    {
        onHealthChanged -= IncrementHealth;
        onHealthChanged -= DecrementHealth;
        onHealthChanged -= IncrementMaxHealth;
        onHealthChanged -= DecrementMaxHealth;
    }

    void Update()
    {
        if (!oldCurrentHealth.Equals(currentHealth))
        {
            ToggleOnHealthChanged(currentHealth - oldCurrentHealth);
            HealthControl();
        }
    }

    void HealthControl()
    {
        if (currentHealth <= 0f)
        {
            OnDeath();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void ToggleOnHealthChanged(float change)
    {
        if (onHealthChanged != null)
        {
            onHealthChanged(change);
        }

        if (change < 0)
        {
            if (onHealthDecremented != null)
            {
                onHealthDecremented();
            }
        }
        else if (change > 0)
        {
            if (onHealthIncremented != null)
            {
                onHealthIncremented();
            }
        }

        oldCurrentHealth = currentHealth;
    }

    void OnDeath()
    {
        if (onDeath != null)
        {
            onDeath();
        }
    }

    public void ResetHealth() => currentHealth = maxHealth;

    public void IncrementHealth(float value) => currentHealth += value;

    public void DecrementHealth(float value) => currentHealth -= value;

    public void IncrementMaxHealth(float value) => maxHealth += value;

    public void DecrementMaxHealth(float value) => maxHealth -= value;
}