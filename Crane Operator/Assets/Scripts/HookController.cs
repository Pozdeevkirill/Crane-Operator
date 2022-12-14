using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private CraneController controller;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private ConfigurableJoint joint;

    [SerializeField] private bool debug;

    
    public GameObject collisionGO;


    private void Update()
    {

        if (collisionGO is not null)
        {
            if (controller.bumper)
                Debug.Log("Click");
            if (controller.bumper && joint.connectedBody is null)
            {
                Debug.Log("Connected");
                Connetct(collisionGO.GetComponentInParent<Rigidbody>());
            }

            if (controller.grip > 0 && joint.connectedBody is not null)
            {
                Debug.Log("Disconnect");
                Disconnect();
            }
        }

        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.E) && joint.connectedBody is null)
            {
                Connetct(collisionGO.GetComponentInParent<Rigidbody>());
            }
            else if (Input.GetKeyDown(KeyCode.E) && joint.connectedBody is not null)
            {
                Disconnect();
            }
        }
    }

    private void Connetct(Rigidbody objectToConnetct)
    {
        joint.connectedBody = objectToConnetct;
        joint.connectedAnchor = collisionGO.transform.localPosition;
    }

    private void Disconnect()
    {
        joint.connectedBody = null;
        collisionGO = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "grabble")
        {
            Debug.Log("enter grabble");
            collisionGO = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "grabble")
        {
            Debug.Log("exit grabble");
            collisionGO = null;
        }
    }
}
