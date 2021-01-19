using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//interface from player status bars
public class StatusBar : MonoBehaviour
{
    public Slider slider;
    public bool decreaseOverTime = false;
    public float decreaseInterval;

    public float minDecrease { get; set; }
    public float maxDecrease { get; set; }

    public void Start()
    {
        if (decreaseOverTime) { StartCoroutine(DecreaseRoutine()); }
    }

    public void DecreaseSlider(float amount)
    {
        slider.value -= amount;
    }

    public void IncreaseSlider(float amount)
    {
        slider.value += amount;
    }

    private IEnumerator DecreaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseInterval);

            float decreaseAmount = Random.Range(minDecrease, maxDecrease);
            this.DecreaseSlider(decreaseAmount);
        }
    }

}
