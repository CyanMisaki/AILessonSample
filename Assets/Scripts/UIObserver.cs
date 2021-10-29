using System;
using UnityEngine.UI;

public class UIObserver : IEnemy
{
    private readonly Text _power;
    private readonly Text _money;
    private readonly Text _health;
    private readonly Text _pursuit;

    public UIObserver(Text power, Text money, Text health, Text pursuit)
    {
        _power = power;
        _money = money;
        _health = health;
        _pursuit = pursuit;
    }

    public void Update(PlayerData playerData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                if(_health!=null)
                    _health.text = $"Player health: {playerData.Health}";
                break;
            case DataType.Power:
                if(_power!=null)
                    _power.text = $"Player power: {playerData.Power}";
                break;
            case DataType.Money:
                if(_money!=null)
                    _money.text = $"Player money: {playerData.Money}";
                break;
            case DataType.Pursuit:
                if(_pursuit!=null)
                    _pursuit.text = $"Pursuit level: {playerData.PursuitCounter}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
        }
    }
}