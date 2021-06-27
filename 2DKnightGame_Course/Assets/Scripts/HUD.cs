using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    static private HUD _instance;
    public static HUD Instance 
    {
        get 
        {
            return _instance;
        }
    }

    [SerializeField] private Text _scoreText;
    [SerializeField] private Slider _healthBar;
    public Slider HealthBar 
    {
        get 
        {
            return _healthBar;
        }
        set 
        {
            _healthBar = value;
        }
    }

    [SerializeField] private GameObject _inventoryWindow;
    [SerializeField] private InventoryUIButton inventoryItemPrefab;
    [SerializeField] private Transform inventoryContainer;

    public Text _damageValue;
    public Text _speedValue;
    public Text _healthValue;
    public Text DamageValue 
    {
        get { return _damageValue; }
        set { _damageValue = value; }
    }
    public Text SpeedValue 
    {
        get { return _speedValue; }
        set { _speedValue = value; }
    }
    public Text HealthValue 
    {
        get { return _healthValue; }
        set { _healthValue = value; }
    }

    [SerializeField] private GameObject _winPopUp;
    [SerializeField] private GameObject _losePopUp;
    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private GameObject _optionsMenu;



    private void Awake()
    {
        _instance = this;
        _losePopUp.SetActive(false);
        _winPopUp.SetActive(false);
        _inGameMenu.SetActive(false);
        _optionsMenu.SetActive(false);
    }

    private void Start()
    {
        LoadInventory();
        GameController.Instance.OnUpdateHeroParameters += HandleOnUpdateHeroParameters;
        GameController.Instance.StartNewLevel();
    }

    public void SetScore(string scoreValue) 
    {
        _scoreText.text = scoreValue;
    }

    public void ShowWindow(GameObject window) 
    {
        _inGameMenu.SetActive(true);
        _inventoryWindow.SetActive(true);
        window.GetComponent<Animator>().SetBool("Open", true);
        GameController.Instance.State = GameState.Pause;
    }
    public void HideWindow(GameObject window) 
    {
        window.GetComponent<Animator>().SetBool("Open", false);
        GameController.Instance.State = GameState.Play;
        _inGameMenu.SetActive(false);
        _inventoryWindow.SetActive(false);
    }
    public void HideInGameMenu(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Open", false);
        GameController.Instance.State = GameState.Play;
        _inGameMenu.SetActive(false);
    }
    public void ShowOptionsMenu(GameObject window) 
    {
        _optionsMenu.SetActive(true);
        window.GetComponent<Animator>().SetBool("Open", true);
        GameController.Instance.State = GameState.Pause;
    }
    public void HideOptionsMenu(GameObject window) 
    {
        window.GetComponent<Animator>().SetBool("Open", false);
        GameController.Instance.State = GameState.Play;
        _optionsMenu.SetActive(false);
    }

    public InventoryUIButton AddNewInventoryItem(InventoryItem itemData) 
    {
        InventoryUIButton newItem = Instantiate(inventoryItemPrefab) as InventoryUIButton;
        newItem.transform.SetParent(inventoryContainer);
        newItem.ItemData = itemData;
        return newItem;
    }

    public void UpdateCharacterValues(float newHealth, float newSpeed, float newDamage) 
    {
        _healthValue.text = newHealth.ToString();
        _damageValue.text = newDamage.ToString();
        _speedValue.text = newSpeed.ToString();
    }

    public void ButtonNext() 
    {
        GameController.Instance.LoadNextLevel();
    }

    public void ButtonRestart() 
    {
        GameController.Instance.RestartLevel();
    }

    public void ButtonMenu() 
    {
        GameController.Instance.LoadMainMenu();
    }

    public void ShowLevelWonWindow() 
    {
        _winPopUp.SetActive(true);
        ShowWindow(_winPopUp);
    }

    public void ShowLosewindow() 
    {
        _losePopUp.SetActive(true);
        ShowWindow(_losePopUp);
    }

    public void LoadInventory() 
    {
        InventoryUsedCallback callback = new InventoryUsedCallback(GameController.Instance.InventoryItemUsed);
        for (int i = 0; i < GameController.Instance.Inventory.Count; i++)
        {
            InventoryUIButton newItem = AddNewInventoryItem(GameController.Instance.Inventory[i]);
            newItem.Callback = callback;
        }
    }

    private void HandleOnUpdateHeroParameters(HeroParameters parameters) 
    {
        HealthBar.maxValue = parameters.MaxHealth;
        HealthBar.value = parameters.MaxHealth;
        UpdateCharacterValues(parameters.MaxHealth, parameters.Speed, parameters.Damage);
    }

    private void OnDestroy()
    {
        GameController.Instance.OnUpdateHeroParameters -= HandleOnUpdateHeroParameters;
    }

    public void SetSoundVolume(Slider slider) 
    {
        GameController.Instance.AudioManager.SFXVolume = slider.value;
    }

    public void SetMusicVolume(Slider slider)
    {
        GameController.Instance.AudioManager.MusicVolume = slider.value;
    }
}
