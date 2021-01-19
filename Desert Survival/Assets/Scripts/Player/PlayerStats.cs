using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public PlayerMovementController movementController;

    //thirst values
    [Header("Thirst")]
    public StatusBar thirstBar;
    public StatusBar hungerBar;
    public StatusBar healthBar;

    [SerializeField] float thirstDecrease = 0.2f;
    [SerializeField] float hungerDecrease = 0.15f;

    [Header("Temperature")]
    [SerializeField] float bodyTemperature;

    private float healthDecrease = 0f;


    private void Start()
    {
        thirstBar.minDecrease = 0f;

        bodyTemperature = 36f;

        StartCoroutine(HealthRoutine(5f));
    }
    void Update()
    {
        ChengeThirstDecreaseOnRun();
        ChangeHealthStatus();
    }

    //changing thirst maximum decrease when player is running
    private void ChengeThirstDecreaseOnRun()
    {
        if (movementController.isRunning)
        {
            thirstBar.maxDecrease = thirstDecrease * 2 ;
            hungerBar.maxDecrease = hungerDecrease * 2;
        }
        else
        {
            thirstBar.maxDecrease = thirstDecrease;
            hungerBar.maxDecrease = hungerDecrease;
        }
    }

    public void ChangeValues(float water, float food, float health)
    {
        thirstBar.IncreaseSlider(water);
        hungerBar.IncreaseSlider(food);
        healthBar.IncreaseSlider(health);
    }

    private void ChangeHealthStatus()
    {
        if(thirstBar.slider.value <= 0 && hungerBar.slider.value <= 0)
        {
            healthDecrease = 0.8f;
        }
        else if(thirstBar.slider.value <= 0 && hungerBar.slider.value > 0)
        {
            healthDecrease = 0.4f;
        }
        else if(thirstBar.slider.value > 0 && hungerBar.slider.value <= 0)
        {
            healthDecrease = 0.3f;
        }
        else
        {
            healthDecrease = 0;
        }
    }

    private IEnumerator HealthRoutine(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            healthBar.slider.value -= healthDecrease;
        }
    }


}
