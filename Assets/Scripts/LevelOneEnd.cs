using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class LevelOneEnd : MonoBehaviour
{
    public Animator anim; //Fade out animator

    //On trigger enter start IEnumeraotr ending
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collided object has tag Player srun IEnumerator ending 
        if (collision.tag == "Player")
        {
            StartCoroutine(Ending());
        }
    }

    IEnumerator Ending()
    {
        //Play animiaton ending
        anim.Play("Ending");
        //Wait for 1.23 seconds
        yield return new WaitForSeconds(1.23f);
        //Go to next level
        GameManager.NextLevel();
    }

}
