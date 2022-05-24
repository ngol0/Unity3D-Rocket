using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject crashingEffect;
    [SerializeField] GameObject successEffect;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Update()
    {
        RespondToDebugKey();
    }

    private void RespondToDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            collisionDisable = !collisionDisable; //toggle collision
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) { return;  }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friendly");
                break;
            case "Finish":
                StartCoroutine(OnSuccessMission());
                break;
            default: 
                StartCoroutine(OnFailedMission());
                break;
        }
    }

    void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        if (nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        }
        SceneManager.LoadScene(nextIndex);
        
    }

    IEnumerator OnFailedMission()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        isTransitioning = true;

        //explosion effect
        FailExplosion();

        yield return new WaitForSeconds(2f);

        //load current level
        SceneManager.LoadScene(currentIndex);
    }

    IEnumerator OnSuccessMission()
    {

        isTransitioning = true;

        //explosion effect
        SuccessExplosion();

        yield return new WaitForSeconds(2f);

        //load current level
        LoadNextLevel();
    }

    void SuccessExplosion()
    {
        //explosion effect
        Instantiate(successEffect, transform.position, Quaternion.identity);

        //disable movement
        GetComponent<Movement>().enabled = false;
    }

    void FailExplosion()
    {
        //explosion effect
        Instantiate(crashingEffect, transform.position, Quaternion.identity);

        //disable movement
        GetComponent<Movement>().enabled = false;


    }
}
