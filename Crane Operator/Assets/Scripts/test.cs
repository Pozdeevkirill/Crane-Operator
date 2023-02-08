using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private FadeScreen fade;

    private void Start()
    {
        fade = FindAnyObjectByType<FadeScreen>();
        Debug.Log("Fade_");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Fade");
            fade.FadeIn();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Fade");
            fade.FadeOut();
        }
    }
}
