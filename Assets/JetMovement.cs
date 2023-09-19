using UnityEngine;
public class JetMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f; // Adjust the speed as needed.
    [SerializeField] private float boostForce = 5.0f; 
    private Rigidbody rigidbody;

    private Vector3 lastPositionBeforeReleasingTouch;
    private bool isJoystickTouched;
    private Vector3 boostDirection;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Movement(Vector2 joystickTouchValue)
    {
        float horizontalInput = joystickTouchValue.x;
        float verticalInput = joystickTouchValue.y;
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0); ;
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(newPosition);
        
        lastPositionBeforeReleasingTouch = moveDirection;
        // Check if the joystick is touched.
        if (joystickTouchValue != Vector2.zero)
        {
            rigidbody.MovePosition(newPosition);
            isJoystickTouched = true;
            boostDirection = (Vector3)joystickTouchValue - rigidbody.position;
            
        }
        else if (isJoystickTouched) // Joystick was released.
        {
            // Apply the boost force.
            rigidbody.AddForce(boostDirection * boostForce, ForceMode.Impulse);
            isJoystickTouched = false;
        }
    }
}
