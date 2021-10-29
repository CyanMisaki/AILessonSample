using System;
using UnityEngine;
using Random = System.Random;

public class Enemy : IEnemy
{
    private const float KHealth = 0.2f;
    private const float KPower = 0.8f;
    private const float KGreatPower = 1.2f;
    private const int enemyStartPower = 1;
    
    private string _title;

    private int _playerMoney;
    private int _playerPower;
    private int _playerHealth;
    private int _playerPursuit;

    public Action<Enemy> OnUpdate;
    
    private readonly Random _random = new Random();

    public Enemy(string title)
    {
        _title = title;
    }

    public int Power => _random.Next(0, 10) > 7 ? Mathf.FloorToInt(enemyStartPower+(_playerPower*KGreatPower)+(_playerHealth*KHealth)) : Mathf.FloorToInt(enemyStartPower+(_playerPower*KPower)+(_playerHealth*KHealth));

    public void Update(PlayerData playerData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _playerHealth = playerData.Health;
                break;
            case DataType.Power:
                _playerPower = playerData.Power;
                break;
            case DataType.Money:
                _playerMoney = playerData.Money;
                break;
            case DataType.Pursuit:
                _playerPursuit = playerData.PursuitCounter;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
        }

        Debug.Log($"Enemy {_title}: {dataType} parameter has been changed");
        OnUpdate?.Invoke(this);
    }
    
}