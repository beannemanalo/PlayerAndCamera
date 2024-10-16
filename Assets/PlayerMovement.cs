using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController _characterController;
    InputController _input;
    GroundChecker _groundChecker;
    public float Speed = 1;
    public float JumpSpeed = 10;
    public float AirControl;
    private Vector3 _lastVelocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<InputController>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private bool ShouldJump()
    {
        return _input.Jump && _groundChecker.Grounded;                 //SALTARRRR
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
        float smoothFactor = _groundChecker.Grounded? 1 : AirControl * Time.deltaTime;


        vel.x = Mathf.Lerp(_lastVelocity.x, direction.x * Speed, smoothFactor);    
        vel.y = _lastVelocity.y;
        vel.z = Mathf.Lerp(_lastVelocity.z, direction.z * Speed, smoothFactor);
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
