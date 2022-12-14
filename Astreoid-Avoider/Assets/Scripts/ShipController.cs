using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] float forceMagnitude;
    [SerializeField] float maxVelocity;
    [SerializeField] float rotationSpeed;

    private Camera mainCamera;
    private Rigidbody shipRb;

    private Vector3 movementDirection;


    // Start is called before the first frame update
    void Start()
    {
        shipRb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        KeepShipOnScreen();
        RotateShip();
        
    }

    // her fizik sistemi update oldu�unda calissin. her framede fizik hesaplamak uzun s�rer ve yorar
    // performanstan bagimsiz, fps farketmez
    void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) return;

        //ForceMode.Force surekli guc eklemek icin
        shipRb.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        shipRb.velocity = Vector3.ClampMagnitude(shipRb.velocity, maxVelocity);
    }

    private void RotateShip()
    {
        if (shipRb.velocity == Vector3.zero) return;

        // velocittye gore geeminin yonunu degis, Vector3.back geminin ne y�ne rotate etmesi gerektigini belirtmek icin
        // back geminin ust k�sm� kamerayadogru bakan k�sm� 
        Quaternion targetRotation = Quaternion.LookRotation(shipRb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void KeepShipOnScreen()
    {
        Vector3 newPos = transform.position;

        //ekran�n her kosesinde 01  11  gibi indisler var.
        //                       .5.5
        //                      00  10
        // Her ekranda b�yle baz� ekranlar x -10 g�r�nebilrken baz�s�nda g�runmez bunu dinamik kullanabilmek icin
        Vector3 viewPortPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewPortPos.x > 1)
        {
            newPos.x = -newPos.x + 0.1f;
        }
        else if (viewPortPos.x < 0)
        {
            newPos.x = -newPos.x - 0.1f;
        }

        if (viewPortPos.y > 1)
        {
            newPos.y = -newPos.y + 0.1f;
        }
        else if (viewPortPos.y < 0)
        {
            newPos.y = -newPos.y - 0.1f;
        }
        transform.position = newPos;
    }

    void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(touchPos);

            movementDirection = touchWorldPos - transform.position;
            movementDirection.z = 0;    //ekrandan ��kmas�n 2d gibi g�r��ncek
            movementDirection.Normalize();  // bas�l� tuttukca h�zlanmas�n

        }
        else
        {
            movementDirection = Vector3.zero; //elimizi kaldirdigimizda zamanla yava�lay�p dursun
        }

    }

}
