using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightWindow : MonoBehaviour
{
    [SerializeField] private Text _moneyCountText;
    [SerializeField] private Text _healthCountText;
    [SerializeField] private Text _powerCountText;
    
    [SerializeField] private Text _enemyPowerCountText;

    [SerializeField] private Button _addMoney;
    [SerializeField] private Button _minusMoney;
    [SerializeField] private Button _addPower;
    [SerializeField] private Button _minusPower;
    [SerializeField] private Button _addHealth;
    [SerializeField] private Button _minusHealth;
    
    [SerializeField] private Button _startFight;
    
    [SerializeField] private Text _pursuitCounter;
    [SerializeField] private Button _addPursuit;
    [SerializeField] private Button _minusPursuit;
    
    [SerializeField] private Button _skipBattle;

    private int _allCountPlayerMoney;
    private int _allCountPlayerHealth;
    private int _allCountPlayerPower;

    private Enemy _enemy;
    private Health _playerHealth;
    private Money _playerMoney;
    private Power _playerPower;
    private Pursuit _playerPursuit;

    private const float KEnemyEscape = 0.2f;

    private UIObserver _uiObserver;
    private void Start()
    {
        ButtonSubscribe();
        
        _playerHealth = new Health();
        _playerMoney = new Money();
        _playerPower = new Power();
        _playerPursuit = new Pursuit();

        _enemy = new Enemy("Bird");
        _enemy.OnUpdate += OnEnemyUpdate;
        SubscribeEnemy(_enemy);

        _uiObserver = new UIObserver(_powerCountText, _moneyCountText, _healthCountText, _pursuitCounter);
        SubscribeEnemy(_uiObserver);
        
        DisableSkipButton(false);
    }

    private void DisableSkipButton(bool state)
    {
        _skipBattle.enabled = !state;
        _skipBattle.image.color = state ? Color.gray : Color.green;
    }

    private void OnEnemyUpdate(Enemy obj)
    {
        _enemyPowerCountText.text = $"Enemy Power: {obj.Power}";
    }

    private void SubscribeEnemy(IEnemy enemy)
    {
        _playerHealth.Subscribe(enemy);
        _playerMoney.Subscribe(enemy);
        _playerPower.Subscribe(enemy);
        _playerPursuit.Subscribe(enemy);
    }

    private void UnSubscribeEnemy(IEnemy enemy)
    {
        _playerHealth.UnSubscribe(enemy);
        _playerMoney.UnSubscribe(enemy);
        _playerPower.UnSubscribe(enemy);
        _playerPursuit.UnSubscribe(enemy);
    }
    private void ButtonSubscribe()
    {
        _addMoney.onClick.AddListener(() => ChangeMoney(true));
        _minusMoney.onClick.AddListener(() => ChangeMoney(false));

        _addHealth.onClick.AddListener(() => ChangeHealth(true));
        _minusHealth.onClick.AddListener(() => ChangeHealth(false));

        _addPower.onClick.AddListener(() => ChangePower(true));
        _minusPower.onClick.AddListener(() => ChangePower(false));
        
        _addPursuit.onClick.AddListener(() => ChangePursuit(true));
        _minusPursuit.onClick.AddListener(() => ChangePursuit(false));

        _startFight.onClick.AddListener(Fight);
        _skipBattle.onClick.AddListener(SkipBattle);
    }

    private void SkipBattle()
    {
        Debug.Log($"Fight complete. Enemy Escaped");
    }

    private void ChangePursuit(bool value)
    {
        _playerPursuit.PursuitCounter += value ? 1 : -1;

        IsBattleSkipPossible();
    }

    private void IsBattleSkipPossible()
    {
        if (_playerPursuit.PursuitCounter < 5 || _playerPower.Power > _enemy.Power + (_enemy.Power * KEnemyEscape))
        {
            DisableSkipButton(false);
        }
        else
        {
            DisableSkipButton(true);
        }
    }


    private void ChangePower(bool value)
    {
        _playerPower.Power += value ? 1 : -1;
        IsBattleSkipPossible();
    }

    private void ChangeHealth(bool value)
    {
        _playerHealth.Health += value ? 1 : -1;
        IsBattleSkipPossible();
    }

    private void ChangeMoney(bool value)
    {
        _playerMoney.Money += value ? 1 : -1;
    }

    private void Fight()
    {
        var result = _playerPower.Power < _enemy.Power ? "You Loooose..." : "You WIN!!!";
        Debug.Log($"Fight complete. Result = {result}");
    }

    private void OnDestroy()
    {
        _addMoney.onClick.RemoveAllListeners();
        _minusMoney.onClick.RemoveAllListeners();
        
        _addHealth.onClick.RemoveAllListeners();
        _minusHealth.onClick.RemoveAllListeners();
        
        _addPower.onClick.RemoveAllListeners();
        _minusPower.onClick.RemoveAllListeners();
        
        _addPursuit.onClick.RemoveAllListeners();
        _minusPursuit.onClick.RemoveAllListeners();
        
        _startFight.onClick.RemoveAllListeners();
        _skipBattle.onClick.RemoveAllListeners();
        
        UnSubscribeEnemy(_enemy);
    }
    
    
}