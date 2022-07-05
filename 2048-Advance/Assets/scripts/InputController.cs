using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    InputSystem input;
    public Vector2 mousePos;
    public Vector2 mousePressPos;
    public Vector2 mouseReleasePos;
    public bool md = false;

    private void Awake()
    {
        input = new InputSystem();
        input.Touch.MD.performed += context => OnPressed();
        input.Touch.MU.performed += context => OnReleased();
        input.Touch.Position.performed += context => OnHold(context.ReadValue<Vector2>());
    }

    //private void Update()
    //{
    //    mousePos = input.Touch.Position.ReadValue<Vector2>();
    //    if (md) OnHold(mousePos);
    //}

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }


    private void OnPressed()
    {
        md = true;
        mousePressPos = mousePos;
        print("MD");
    }

    private void OnReleased()
    {
        md = false;
        mouseReleasePos = mousePos;
        print("MU");
    }

    private void OnHold(Vector2 pos)
    {
        mousePos = pos;
        if(md)print(pos.ToString());
        
    }
}
