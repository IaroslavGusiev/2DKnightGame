using System;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private int _dragonHitScore = 10;
    [SerializeField] private int _dragonKillScore = 50;
    [SerializeField] private GameController _gameController;

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            if (value != _score)
            {
                _score = value;
                HUD.Instance.SetScore(_score.ToString());
            }
        }
    }

    private void Awake()
    {
        _gameController.OnDragonWasHit += UpdatingScore;
        _gameController.OnDragonWasKilled += UpdatingScore;
    }

    private void UpdatingScore(int score) 
    {
        _score += score;  
    }


}
