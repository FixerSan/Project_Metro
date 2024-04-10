using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    public PlayerController controller;
    public Dictionary<Define.PlayerMovementType, int> movementLevels = new Dictionary<Define.PlayerMovementType, int>();
    public float moveAxis;

    public PlayerMovement(PlayerController _controller)
    {
        controller = _controller;
        movementLevels.Add(Define.PlayerMovementType.Walk, 1);

        Managers.Input.jumpAction += Jump;
    }


    public void Update()
    {
        Move();
    }
    
    public bool CheckMove()
    {
        if (Managers.Input.MoveAxis.x != 0f)
        {
            controller.ChangeState(Define.PlayerState.Move);
            return true;
        }

        return false;
    }

    public void Move()
    {
        if (movementLevels[Define.PlayerMovementType.Walk] == 1)
            controller.rb.MovePosition(new Vector3(controller.transform.position.x + Managers.Input.MoveAxis.x * controller.status.CurrentSpeed * Time.deltaTime, controller.transform.position.y, controller.transform.position.z));
    }

    public bool CheckStop()
    {
        if (Managers.Input.MoveAxis.x == 0f)
        {
            controller.ChangeState(Define.PlayerState.Idle);
            return true;
        }

        return false;
    }

    public void Jump()
    {
        Debug.Log("มกวม");
    }
}
