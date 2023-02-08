using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(CraneController))]
public class ChageLenghtRope : MonoBehaviour
{
    [SerializeField] private RopeControllerSimple controller;
    [SerializeField] private float speed;

    [SerializeField] private bool debug;

    private CraneController craneController;


    private void Start()
    {
        craneController = GetComponent<CraneController>();
    }

    private void Update()
    {
        int direction = 0;

        if (debug)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction = 1;
            }
            else if(Input.GetKey(KeyCode.UpArrow))
                direction = -1;
        }
        else
        {
            if (craneController.grip > 0)
                direction = 1;
            else if (craneController.bumper)
                direction = -1;
            else
                direction = 0;
        }
        

        if (direction == 1)
        {
            controller.ChangeLenght(speed, direction);
        }
            
        else if (direction == -1)
        {
            controller.ChangeLenght(speed, direction);
        }
    }
}
