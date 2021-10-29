using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
    private string _title;
    
    private int _moneyCounter;
    private int _healthCounter;
    private int _powerCounter;
    private int _pursuitCounter;

    private List<IEnemy> _enemyListeners = new List<IEnemy>();

    public PlayerData(string title)
    {
        _title = title;
    }

    public int PursuitCounter
    {
        get => _pursuitCounter;
        set
        {
            if (_pursuitCounter != value)
                _pursuitCounter = value;
            Notify(DataType.Pursuit);
        }
    }
    public int Money
    {
        get => _moneyCounter;
        set
        {
            if(_moneyCounter != value)
                _moneyCounter = value;
            Notify(DataType.Money);
        }
    }
    public int Health
    {
        get => _healthCounter;
        set
        {
            if(_healthCounter != value)
                _healthCounter = value;
            Notify(DataType.Health);
        }
    }
    public int Power
    {
        get => _powerCounter;
        set
        {
            if(_powerCounter != value)
                _powerCounter = value;
            Notify(DataType.Power);
        }
    }

    public void Subscribe(IEnemy enemy)
    {
        if(!_enemyListeners.Contains(enemy))
            _enemyListeners.Add(enemy);
    }
    
    public void UnSubscribe(IEnemy enemy)
    {
        _enemyListeners.Remove(enemy);
    }
    
    private void Notify(DataType dataType)
    {
        foreach (var enemy in _enemyListeners)
        {
            enemy.Update(this, dataType);
        }
    }

    public override string ToString()
    {
        return _title;
    }
}

public class Health : PlayerData
{
    public Health() : base("Health")
    {
        
    }
}

public class Money : PlayerData
{
    public Money() : base("Money")
    {
        
    }
}

public class Power : PlayerData
{
    public Power() : base("Power")
    {
        
    }
}

public class Pursuit : PlayerData
{
    public Pursuit() : base("Pursuit")
    {
        
    }
}