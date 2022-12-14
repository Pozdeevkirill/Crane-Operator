using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CraneController))]
public class ChangeLenghtController : MonoBehaviour
{
    [SerializeField] private Rigidbody objectMove;
    [SerializeField] private float moveSpeed = 1.5f; // Скорость движения standart = 1.5
    [SerializeField] private Transform endPostion;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float lenght = 10;

    [SerializeField] private bool debug = false;

    private CraneController craneController; // контроллер


    // Start is called before the first frame update
    void Start()
    {
        //startPosition = objectMove.transform;
        craneController = GetComponent<CraneController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = craneController.movement.z;


        //MoveDirection < 0 - Движение в перед
        //MoveDirection > 0 - Дивжение назад
        

        if (moveDirection != 0)
        {
            if (moveDirection < 0)
            {
                objectMove.velocity = objectMove.transform.up * moveDirection * moveSpeed;

            }
            else if (moveDirection > 0)
            {
                objectMove.velocity = objectMove.transform.up * moveDirection * moveSpeed;
            }
        }
        else
        {
            objectMove.velocity = new(0, 0, 0);
        }

        if (debug)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = -1;
                objectMove.velocity = objectMove.transform.up * moveDirection * moveSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDirection = 1;

                objectMove.velocity = objectMove.transform.up * moveDirection * moveSpeed;
                //objectMove.transform.Translate(objectMove.transform.forward * Time.deltaTime * moveDirection * moveSpeed);
            }
            else
            {
                objectMove.velocity = new(0, 0, 0);
                moveDirection = 0;
            }
        }
    }

}
