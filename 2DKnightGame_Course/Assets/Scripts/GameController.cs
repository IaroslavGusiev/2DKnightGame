using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum GameState { Play, Pause}
public delegate void InventoryUsedCallback(InventoryUIButton item);
public delegate void UpdateHeroParametersHandler(HeroParameters parameters);

public class GameController : MonoBehaviour
{
    private GameState _state;
    static private GameController _instance;
    public static GameController Instance 
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameController = Instantiate(Resources.Load("Prefabs/GameController")) as GameObject;
                _instance = gameController.GetComponent<GameController>();
            }

            return _instance;
        }

    }
    [SerializeField] private int _dragonHitScore = 10;
    [SerializeField] private int _dragonKillScore = 50;
    [SerializeField] private int _dragonKillExperience = 50;
    
    [SerializeField] private List<InventoryItem> _inventory;
    public List<InventoryItem> Inventory 
    {
        get 
        {
            return _inventory;
        }
        set 
        {
            _inventory = value;
        }
    }
    private Knight _knight;
    public Knight Knight 
    {
        get 
        {
            return _knight;
        }
        set 
        {
            _knight = value;
        }
    }

    [SerializeField] private Audio _audioManager;
    public Audio AudioManager 
    {
        get 
        {
            return _audioManager;
        }
        set 
        {
            _audioManager = value;
        }
    }

    [SerializeField] private HeroParameters _hero;
    public HeroParameters Hero 
    {
        get 
        {
            return _hero;
        }
        set 
        {
            _hero = value;
        }
    }

    public event UpdateHeroParametersHandler OnUpdateHeroParameters;

    public static event Action<int> OnDragonWasHit;
    public static event Action<int> OnDragonWasKilled;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
        _state = GameState.Play;
        _inventory = new List<InventoryItem>();
        InitializeAudioManager();
        AudioManager.PlayMusic(false);
    }

    public void StartNewLevel()
    {
        //HUD.Instance.SetScore(Score.ToString());
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(_hero);
        }
        State = GameState.Play;   
    }

    public void Hit(IDestructable victim) 
    {
        if (victim.GetType() == typeof(Dragon))
        {
            OnDragonWasHit?.Invoke(_dragonHitScore);
            AudioManager.PlaySound("Digital_Sword");
        }
        if (victim.GetType() == typeof(Knight))
        {
            HUD.Instance.HealthBar.value = victim.Health;
        }
    }

    public void Killed(IDestructable victim) 
    {
        if (victim.GetType() == typeof(Dragon))
        {
            OnDragonWasKilled?.Invoke(_dragonKillScore);
            _hero.Experience += _dragonKillExperience;
            Destroy((victim as MonoBehaviour).gameObject);
        }
        if (victim.GetType() == typeof(Knight))
        {
            GameOver();
        }
    }

    public GameState State 
    {
        get 
        {
            return _state;
        }
        set 
        {
            if (value == GameState.Play)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
            _state = value;
        }
    }

    public void AddNewInventoryItem(InventoryItem itemData)
    {
        InventoryUIButton newUiItem = HUD.Instance.AddNewInventoryItem(itemData);
        InventoryUsedCallback callback = new InventoryUsedCallback(InventoryItemUsed);
        newUiItem.Callback = callback;
        _inventory.Add(itemData);

    }

    public void InventoryItemUsed(InventoryUIButton item)
    {
        switch (item.ItemData.CrystallType)
        {
            case CrystallType.Red:
                _hero.Damage += item.ItemData.Quantity / 10f;
                break;
            case CrystallType.Green:
                _hero.MaxHealth += item.ItemData.Quantity / 10f;
                break;
            case CrystallType.Blue:
                _hero.Speed += item.ItemData.Quantity / 10f;
                break;
            default:
                Debug.LogError("Wrong Crystall");
                break;
        }
        _inventory.Remove(item.ItemData);
        AudioManager.PlaySound("Pop_up2");
        Destroy(item.gameObject);
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(_hero);
        }
    }

    public void LoadNextLevel() 
    {
        AudioManager.PlaySound("Item2");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void RestartLevel() 
    {
        AudioManager.PlaySound("Item2");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void LoadMainMenu() 
    {
        AudioManager.PlaySound("Item2");
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void PrincessFound() 
    {
        HUD.Instance.ShowLevelWonWindow();
        AudioManager.PlaySound("Click");
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene("Win Scene", LoadSceneMode.Single);
            Time.timeScale = 1;
        }
    }

    public void GameOver() 
    {
        HUD.Instance.ShowLosewindow();
        AudioManager.PlaySound("Die_1");
    }

    public void InitializeAudioManager() 
    {
        _audioManager.SourceSFX = gameObject.AddComponent<AudioSource>();
        _audioManager.SourceMusic = gameObject.AddComponent<AudioSource>();
        _audioManager.SourceRandomPitchSFX = gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioListener>();
    }

    public void LevelUp() 
    {
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(_hero);
            AudioManager.PlaySound("Warning_Loop2");
        }
    }

    public void TestConflict() 
    {
        Debug.Log("Test");
    }
}
