using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    // Events
    public delegate void onPlayerDeathCallback();
    public onPlayerDeathCallback OnPlayerDeath;

    public int MaxHp = 10;

    [SerializeField] private int _currentHp;
    
    public int CurrentHp { get { return _currentHp; } }

    public int Health { get { return _currentHp; } set { _currentHp = value; } }

    void Start()
    {
        _currentHp = MaxHp;
    }

    public void Heal(int amount)
    {
        _currentHp += amount;
        if (_currentHp > MaxHp) _currentHp = MaxHp;
    }
    public void Damage(int amount)
    {
        _currentHp -= amount;
        if (_currentHp < 0) _currentHp = 0;
        if (_currentHp == 0) Die();
    }

    private void Die()
    {
        OnPlayerDeath();
    }
}
