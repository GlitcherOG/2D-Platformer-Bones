using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{

    
    //The panel to show you have won
    public GameObject WinPanel;
    private Image winImage;

    public void SetWin()
    {
        //apply a scale timer to the win panel
        ScaleObject();
        //get the image of the win panel
        winImage = WinPanel.GetComponent<Image>();
        // and set alpha to 1
        winImage.color = new Color(winImage.color.r, winImage.color.g, winImage.color.b, 1f);
        //Initiate the win and return to menu aftr a few seconds
        YouWon();
        //set Time scale to 0 so game is no longer interactive
        Time.timeScale = 0;
        
    }


    IEnumerator ScaleObject()
    {
        // duration in seconds
        float scaleDuration = 3;
        // start scale of the object
        Vector3 actualScale = WinPanel.transform.localScale;
        // target scale of the object 
        Vector3 targetScale = new Vector3(1f, 1f, 1f);  

        for (float t = 0; t < 1; t += Time.deltaTime / scaleDuration)
        {
            //change the scale of the object using Lerp
            WinPanel.transform.localScale = Vector3.Lerp(actualScale, targetScale, t);
            yield return null;
        }
    }

    IEnumerator YouWon()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }
}
