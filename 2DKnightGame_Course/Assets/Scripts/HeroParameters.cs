using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroParameters 
{
    #region Private_variables
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _damage = 20;
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _experience = 0;
    #endregion
    public float MaxHealth 
    {
        get 
        {
            return _maxHealth;
        }
        set 
        {
            _maxHealth = value;
        }
    }
    public float Damage 
    {
        get 
        {
            return _damage;
        }
        set 
        {
            _damage = value;
        }
    }
    public float Speed 
    {
        get 
        {
            return _speed; 
        }
        set 
        {
            _speed = value;
        }
    }
    public int Experience 
    {
        get
        {
            return _experience;
        }
        set 
        {
            _experience = value;
            CheckLevelExperience();
        }
    }

    private int _nextExperienceLevel = 100;
    private int _previousExperienceLevel = 0;
    private int _level = 1;

    private void CheckLevelExperience() 
    {
        if (_experience > _nextExperienceLevel)
        {
            _level++;
            int addition = _previousExperienceLevel;
            _previousExperienceLevel = _nextExperienceLevel;
            _nextExperienceLevel += addition;
            switch (Random.Range(0, 3))
            {
                case 0: _maxHealth++;
                    break;
                case 1: _damage++;
                    break;
                case 2: _speed++;
                    break;
            }
            GameController.Instance.LevelUp();
        }
    }
}
