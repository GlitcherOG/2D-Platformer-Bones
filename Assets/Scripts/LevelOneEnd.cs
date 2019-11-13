using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class LevelOneEnd : MonoBehaviour
{
    public float fadetime;
    public GameObject endingScene;
    public Animator anim1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Ending());
        }
    }

    IEnumerator Ending()
    {
        anim1.Play("Ending");
        yield return new WaitForSeconds(2f);
        GameManager.NextLevel();
    }

}
