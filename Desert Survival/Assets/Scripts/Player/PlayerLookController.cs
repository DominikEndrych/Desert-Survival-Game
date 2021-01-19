using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLookController : MonoBehaviour
{

    public Transform PlayerCamera;
    public Transform PlayerBody;

    [SerializeField] float sensitivity  = 5f;
    [SerializeField] float cameraSmoothSpeed = 10f;
    [SerializeField] float minimumY     = -70f;
    [SerializeField] float maximumY     = 70f;


    private float bodyRotation;
    private float cameraRotation;

    private void Awake()
    {
        this.LockCursor(true);
    }

    void FixedUpdate()
    {
        if (Inventory.instance.isOpen || ReadingPanel.instance.isOpen) { return; }
        this.MakeRotation();
    }

    private void Update()
    {
        if (Inventory.instance.isOpen || ReadingPanel.instance.isOpen)
        {
            LockCursor(false);
        }
        else
        {
            LockCursor(true);
        }
        //this.LockCursor(!Inventory.instance.isOpen);
    }

    private void MakeRotation()
    {

        bodyRotation += Input.GetAxis("Mouse X") * sensitivity;
        cameraRotation  += Input.GetAxis("Mouse Y") * sensitivity;

        //stop camera from moving out of range
        cameraRotation = Mathf.Clamp(cameraRotation, minimumY, maximumY);

        //quaternions for rotations
        Quaternion cameraTarget = Quaternion.Euler(-cameraRotation, 0, 0);
        Quaternion bodyTarget = Quaternion.Euler(0, bodyRotation, 0);

        //rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTarget, Time.deltaTime * cameraSmoothSpeed);
        PlayerCamera.localRotation = Quaternion.Lerp(PlayerCamera.localRotation, cameraTarget, Time.deltaTime * cameraSmoothSpeed);
        
    }

    private void LockCursor(bool status)
    {
        if (status)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


}
