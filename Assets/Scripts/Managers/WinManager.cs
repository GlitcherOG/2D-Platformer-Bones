using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    
    //The panel to show you have won
    public GameObject WinPanel;
   

    public void SetWin()
    {
        WinPanel.SetActive(true);
        //Initiate the win and return to menu aftr a few seconds
        StartCoroutine(YouWon());
    }


   
    
    IEnumerator YouWon()
    {
        Debug.Log("You Win");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }
    
}
