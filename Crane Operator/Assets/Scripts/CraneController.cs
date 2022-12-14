using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class CraneController : MonoBehaviour
{
    [SerializeField] private Transform Joystick;
    [SerializeField] private float joyMove = 10f;

    [SerializeField] private Material standartButton_material;
    [SerializeField] private Material selectButton_material;

    [SerializeField] private Renderer gripBtn;
    [SerializeField] private Renderer bumperBtn;
 
    [SerializeField] float rotateSpeed = 1;
    [SerializeField] private SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("platformer", "Move");
    [SerializeField] private SteamVR_Action_Single gripAction = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");
    [SerializeField] private SteamVR_Action_Boolean bumperAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "bumper");

    public Vector3 movement;
    public float grip;
    public bool bumper;

    private SteamVR_Input_Sources hand;
    private Interactable interactable;
    // Start is called before the first frame update
    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand)
        {
            hand = interactable.attachedToHand.handType;
            Vector2 m = moveAction[hand].axis;
            movement = new Vector3(m.x, 0, m.y);
            Joystick.localPosition = movement * joyMove;
            grip = gripAction[hand].axis;
            bumper = bumperAction[hand].state;

            if (bumper)
                bumperBtn.material = selectButton_material;
            else
                bumperBtn.material = standartButton_material;

            if (grip != 0)
                gripBtn.material = selectButton_material;
            else
                gripBtn.material = standartButton_material;
        }
        else
        {
            movement = Vector2.zero;
            grip = 0f;
            bumper = false;
            gripBtn.material = standartButton_material;
            bumperBtn.material = standartButton_material;
            Joystick.localPosition = movement * joyMove;
        }    
    }
}
