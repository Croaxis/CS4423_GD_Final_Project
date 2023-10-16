using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Vector3 movePosition;

    private void Awake(){
        movePosition = transform.position;
    }

    public void SetMovePosition(Vector3 movePosition){
        this.movePosition = movePosition;
    }

    void Update(){
        Vector3 moveDirection = (movePosition - transform.position).normalized;
        if(Vector3.Distance(movePosition, transform.position) < 1f){
            moveDirection = Vector3.zero;
        }
        //GetComponent<IMoveVelocity>().SetVelocity(moveDirection);
    }
}
