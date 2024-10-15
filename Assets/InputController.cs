using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 _move;
    public Vector2 Move => _move;
    public bool _jump;
    public bool Jump => _jump;
    //misma funcion solo con una linea de codigo
    //public Vector2 GetMove()
    //{
    //    return _move;
    //}
    private void OnMove(InputValue input)
    {
       _move =  input.Get<Vector2>();
    }

    private void Update()
    {
        Debug.Log(Move);
    }
    private void OnJump()
    {
        _jump = true;
    }
}
