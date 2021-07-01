using System;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _score;
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


    private void OnEnable()
    {

        GameController.OnDragonWasHit += UpdatingScore;
        GameController.OnDragonWasKilled += UpdatingScore;
    }

    private void OnDisable()
    {
        GameController.OnDragonWasHit -= UpdatingScore;
        GameController.OnDragonWasKilled -= UpdatingScore;
    }

   private void UpdatingScore(int score) 
    {
        Score += score;
    }
}
