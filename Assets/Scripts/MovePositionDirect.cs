using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{
    private Vector3 movePosition;
    private IMoveVelocity MoveVelocity;

    private void Awake(){
        MoveVelocity = GetComponent<IMoveVelocity>();
    }

    public void SetMovePosition(Vector3 movePosition){
        this.movePosition = movePosition;
    }

    private void Update(){
        Vector3 moveDirection = (movePosition - transform.position).normalized;
        if (Vector3.Distance(movePosition, transform.position) < .0001f) moveDirection = Vector3.zero;
        MoveVelocity.SetVelocity(moveDirection);
    }
}
