using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class InputNumberPanel : MonoBehaviour
{

    public Slider slider;
    [SerializeField] TMP_InputField inputField;


    public void Update()
    {
        inputField.readOnly = true;
        inputField.text = slider.value.ToString();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetInput(inputField.text);
            ResetSlider();
        }

    }

    public int Number
    {
        get
        {
            int n = number;
            number = 0;
            gotInput = false;
            return n;

        }
    }
    public bool gotInput = false;

    private int number = 0;


    public void GetInput(string input)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            number = Convert.ToInt32(input);
            gotInput = true;
            gameObject.SetActive(false);
        }
        
    }

    public void SetMaxValue(int maxValue)
    {
        slider.maxValue = maxValue;
    }

    public void ResetSlider()
    {
        slider.value = 0;    
    }
}
