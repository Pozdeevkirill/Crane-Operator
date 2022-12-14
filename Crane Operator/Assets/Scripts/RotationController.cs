using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CraneController))]
public class RotationController : MonoBehaviour
{
    [SerializeField] Transform objectRotate; // Объект который будем вращать
    [SerializeField] Transform axisRotate; // Точка во круг которой будет вращение
    [SerializeField] float rotationSpeed = 1; //Скорость вращения
    [SerializeField] private bool debug = false;

    private CraneController craneController; // контроллер


    // Start is called before the first frame update
    private void Awake()
    {
        craneController = GetComponent<CraneController>();
    }

    private void Update()
    {

        if (debug)
        {
            float rotateDir = 0;
            if (Input.GetKey(KeyCode.A))
            {
                rotateDir = 1;
                objectRotate.RotateAround(axisRotate.position, new Vector3(0, rotateDir * -1, 0), rotationSpeed * rotateDir);
                
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotateDir = -1;
                objectRotate.RotateAround(axisRotate.position, new Vector3(0, rotateDir, 0), rotationSpeed * rotateDir);
                
            }
        }


        float rotateDirection = craneController.movement.x;
        //objectRotate.RotateAround(axisRotate.position,new Vector3(0,rotateDirection,0), rotationSpeed * rotateDirection);
        if (rotateDirection > 0)
        {
            objectRotate.RotateAround(axisRotate.position, new Vector3(0, rotateDirection, 0), rotationSpeed * rotateDirection);
        }else {
            objectRotate.RotateAround(axisRotate.position, new Vector3(0, rotateDirection * -1, 0), rotationSpeed * rotateDirection);
        }

        //player.Rotate(new Vector3(0, rotateDirection * rotationSpeed, 0));
    }
}
