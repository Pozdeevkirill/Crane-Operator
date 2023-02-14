using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    [SerializeField]
    //Объекты каждого шага
    //д.б. на 1 больше чем cargoInStepCount
    private List<GameObject> objectSteps = new();

    [SerializeField]
    //кол-во грузов на каждом шагу
    private List<int> cargoInStepCount = new();

    [SerializeField]
    private float fadeDuration = 30;


    //Текущее кол-во грузов в области 
    private int currentCargoCount;
    //Завершен ли шаг
    private bool stepReady;
    //Завершена ли стройка
    private bool buildReady;
    //ТОлько что разгруженный груз
    private Cargo currentCargo;

    //Переходный экран
    [SerializeField]
    private FadeScreen fade;
    [SerializeField]
    private FadeScreen fadeCamera;

    [SerializeField] private bool isCargo;
    [SerializeField] private bool isHook;

    [SerializeField]
    private GameObject Firework;
    //Разгруженные грузы
    public List<GameObject> cargoInArea = new();

    private int currentStep;


    private void Update()
    {  
        if(currentCargo != null && isCargo && !currentCargo.isGrabbing && !isHook)
        {
            UnloadCargo();
        }
    }
    
    //Метод разгрузки груза
    private void UnloadCargo()
    {
        //Убираем возможность поднятия груза
        currentCargo.canGrab = false;
        //Добавляем груз в список грузов
        cargoInArea.Add(currentCargo.gameObject);
        //Удаляем его из списка
        currentCargo = null;
        //Обновляем счетчик груза
        currentCargoCount++;
        //Запускаем проверку на готовность шага
        StartCoroutine(StepReady());
    }

    //Запуск следующего шага
    private void NextStep()
    {
        //Обнуляем текущее кол-во груза в области
        currentCargoCount = 0;

        //Обновляем указатель на текущий шаг
        currentStep++;
    }

    //Метод окончания шага
    private IEnumerator StepReady()
    {
        //Если шаг готов
        if (currentCargoCount == cargoInStepCount[currentStep])
        {
            //Очистка зоны разгрузки
            ClearUnloadArea();

            StartCoroutine(Timer());

            yield return new WaitForSeconds(2f);
            //Обновление визуальной части строительства
            UpdateVisual();
            //Если это последний шаг
            if (objectSteps.Count - 2 != currentStep)
            {
                NextStep();
            }
            else
                EndGame();
        }
        //Иначе выходим из метода
        else
        {
            yield return null;
        }
    }

    //Метод очистки зоны разгрузки
    private void ClearUnloadArea()
    {
        foreach (var cargo in cargoInArea)
        {
            Destroy(cargo);
        }
        cargoInArea = new();
    }

    //Обновление визуальной части шага
    private void UpdateVisual()
    {
        //Меняем визуал
        objectSteps[currentStep].gameObject.SetActive(false);
        objectSteps[currentStep + 1].gameObject.SetActive(true);
    }

    private IEnumerator Timer()
    {
        fade.Fade(0, 2);
        fadeCamera.Fade(0, 2);
        yield return new WaitForSeconds(fadeDuration);
        fade.Fade(2, 0);
        fadeCamera.Fade(2, 0);
    }

    private void EndGame()
    {
        Debug.Log("Строительство окончено!");
        Firework.SetActive(true);
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
