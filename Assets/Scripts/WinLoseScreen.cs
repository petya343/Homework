using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{
    private Movement playerMovement;
    private GameObject winPanel;
    private GameObject losePanel;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<Movement>();
        winPanel = transform.Find("Win screen").gameObject;
        losePanel = transform.Find("Lose screen").gameObject;
    }

    private void Update()
    {
        if(playerMovement.KeysCounter() == 3)
        {
            winPanel.SetActive(true);
            StartCoroutine(LevelEnded());
        }

        if(playerMovement.LivesCounter() == 0)
        {
            losePanel.SetActive(true);
            StartCoroutine(LevelEnded());
        }
    }
    private IEnumerator LevelEnded()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
