using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour {

    public GameManager gameManager;

    void OnTriggerEnter()
    {
        gameManager.CompleteLevel();
    }

	// public GameObject youWinText;
    // public float delay;

    // Use this for initialization
    /* void Start()
    {
        youWinText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Demon")
        {
            youWinText.SetActive(true);
           // StartCoroutine(Countdown());
        }
    } */

    /* IEnumerator Countdown()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    } */
}
