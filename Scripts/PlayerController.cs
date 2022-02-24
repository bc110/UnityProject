using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    public Rigidbody rb;
    private Vector3 startLocation;

    private float movementX;
    private float movementY;

    public bool IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startLocation = this.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    void OnCollisionStay (Collision collisionInfo)
    {
        IsGrounded = true;
    }
 
    void OnCollisionExit (Collision collisionInfo)
    {
        IsGrounded = false;
    }

    private void OnJump(InputValue movementValue){
        if(IsGrounded){
            rb.AddForce(new Vector3(0.0f, 400.0f, 0.0f));
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);
        
        movement = rotate(movement,Mathf.Deg2Rad*-Camera.main.transform.localEulerAngles.y);

        rb.AddForce(new Vector3(movement.x,0.0f,movement.y) * speed);
    }

    public static Vector2 rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathBall"){
            this.transform.position = startLocation;
            GetComponent<UiLogic>().deathCount++;
        }
        
        if (other.gameObject.tag == "Goal"){
            Time.timeScale = 0;
            GetComponent<UiLogic>().showEndScreen();
        }
    }

}
