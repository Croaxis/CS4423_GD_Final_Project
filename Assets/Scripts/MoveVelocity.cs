using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float moveSpeed= 1f;
    private Vector3 velocityVector;
    private Rigidbody2D unitRigidbody2D;

    private void Awake(){
        // Create animation handler for movement.
        unitRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector3 velocityVector){
        this.velocityVector = velocityVector;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void FixedUpdate(){
        unitRigidbody2D.velocity = velocityVector * moveSpeed;
    }
}
