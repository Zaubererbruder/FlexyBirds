using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreCounter : MonoBehaviour
    {
        public int Score { get; private set; } = 0;
        public int BestResult { get; private set; } = 0;

        public void AddScore()
        {
            Score++;
        }

        public void RestartScore()
        {
            if (Score > BestResult)
                BestResult = Score;
            Score = 0;
        }

    }
}