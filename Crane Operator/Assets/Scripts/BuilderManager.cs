using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BuilderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject House;
    [SerializeField]
    [Tooltip("Объекты зданий, показывающие результат строительства, шаг за шагом")]
    private List<GameObject> objectsSteps;

    [SerializeField]
    [Tooltip("кол-во необходимых грузов для каждого шага.\nКол-во шагов должно быть равно кол-ву шагов")]
    private List<int> CargoCountInStep;

    private int currentCargoCount = 0;

    private int currentStep = 0;

    [SerializeField]
    private bool isCargo;
    [SerializeField]
    private bool isHook;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private float stepDelay;

    //Temp variable
    private Cargo currentCargo;

    private List<GameObject> objectToDelete = new();

    private FadeScreen fade;

    private bool ready;

    private void Start()
    {
        fade = FindObjectOfType<FadeScreen>();

        if (CargoCountInStep.Count != objectsSteps.Count)
            Debug.LogError("Указано неверное кол-во шагов!");
    }

    private void Update()
    {

        if (isCargo && !isHook && currentCargo != null && !currentCargo.isGrabbing)
        {
            currentCargoCount++;
            currentCargo.GetComponentInParent<Cargo>().canGrab = false;
            objectToDelete.Add(currentCargo.gameObject);
            currentCargo = null;
        }

        if (currentCargoCount == CargoCountInStep[currentStep] && !ready)
        {
            Debug.Log("CHeck");
            ready = true;
            StepReady();
        }
    }

    public void StepReady()
    {
        fade.FadeDuration = fadeDuration;
        fade.Fade(0, 2);
        StartCoroutine(Timer(stepDelay));
    }

    private void ReadyToNextLevel()
    {
        ready = false;
        currentStep++;
        fade.Fade(2, 0);
    }

    public IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Timer");
        objectsSteps[currentStep].SetActive(false);
        if(objectsSteps.Count - 1 > currentStep)
        {
            objectsSteps[currentStep + 1].SetActive(true);
            foreach (var obj in objectToDelete)
            {
                Destroy(obj);
            }
        }
        ReadyToNextLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cargo>(out var cargo))
        {
            isCargo = true;
            currentCargo = cargo;
        }

        if (other.TryGetComponent<HookController>(out var hook))
        {
            isHook = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Cargo>(out var cargo))
        {
            isCargo = false;
            currentCargo = null;
        }

        if (other.TryGetComponent<HookController>(out var hook))
        {
            isHook = false;
        }
    }
}
