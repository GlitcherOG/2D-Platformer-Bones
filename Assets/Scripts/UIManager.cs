using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Singleton
    public static UIManager Instance = null;
    public void Awake()
    {
        Instance = this;
    }
    #endregion

    // Update is called once per frame
    public Text scoreText;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score:" + score.ToString();
    }
}
