using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance = null;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    #endregion
    public int score = 0;
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UIManager.Instance.UpdateScore(score);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Prevlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
