using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController _characterController;
    InputController _input;
    public float Speed = 1;
    public float JumpSpeed = 10;
    private Vector3 _lastVelocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    private bool ShouldJump()
    {
        return _input.Jump;                 //SALTARRRR
    }

    private void Jump(ref Vector3 vel)
    {
        vel.y = JumpSpeed;
    }

    private void Move()
    {
        //esto es simple move
        Vector3 direction = new Vector3(_input.Move.x, 0, _input.Move.y);
        //_characterController.SimpleMove(direction * Speed);
        Vector3 vel = new Vector3();
        vel.x = direction.x * Speed;
        vel.y = _lastVelocity.y;
        vel.z = direction.z * Speed;
        //not simple move

        vel.y = GetGravity();     //Aplicas una vel, guardas
        if (ShouldJump())                   //SALTARRCH
            Jump(ref vel);

        _characterController.Move(vel * Time.deltaTime);


        //turn around
        if(direction.magnitude > 0)
        {
            Vector3 target = transform.position + direction;
            transform.LookAt(target);
        }
        _lastVelocity = vel;      //la ultima vel del frame(?
    }

    private float GetGravity()
    {
        return _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
    }
}
