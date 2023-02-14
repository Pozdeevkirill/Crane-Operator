using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private FadeScreen fadeVR;
    [SerializeField]
    private FadeScreen fadeCamera;
    [SerializeField]
    private float restartDuration;


    [SerializeField]
    private GameObject kran;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    private void RestartGame()
    {
        Destroy(kran);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
