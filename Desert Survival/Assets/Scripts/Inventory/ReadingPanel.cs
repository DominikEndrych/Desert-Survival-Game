using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadingPanel : MonoBehaviour
{
    public static ReadingPanel instance;

    public bool isOpen;

    [SerializeField] GameObject UI;
    [SerializeField] TextMeshProUGUI textArea;

    private void Awake()
    {
        if (instance != null) { return; }

        instance = this;
    }

    private void Start()
    {
        textArea.text = null;
    }

    public void Open(string text)
    {
        textArea.text = text;
        UI.gameObject.SetActive(true);
        isOpen = true;
    }

    public void Close()
    {
        textArea.text = null;
        UI.SetActive(false);
        isOpen = false;
    }
    
}
