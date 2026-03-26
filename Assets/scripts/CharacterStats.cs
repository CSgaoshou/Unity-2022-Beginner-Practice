using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;

    public System.Action onHealthChanged;

    [SerializeField]private int currentHealth;

    public int CurrentHealth => currentHealth;

    void Start()
    {
        currentHealth = maxHealth.GetValue();

        damage.AddModifier(0);
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        onHealthChanged?.Invoke();

        if (currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Entity entity = GetComponent<Entity>();
        if (entity != null && entity.isDead)
            return;

        entity?.Die();
    }
}
