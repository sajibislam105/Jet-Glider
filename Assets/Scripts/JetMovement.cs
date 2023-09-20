using System.Collections;
using UnityEngine;
using Zenject;

public class JetMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f; // Adjust the speed as needed.
    [SerializeField] private float boostSpeed = 20.0f;
    
    [Inject] private Rigidbody rigidbody;

    private bool isJoystickTouched;
    private bool dashing = true;
    private readonly float dashingTime = 0.5f;
    private readonly float dashingCoolDown = 0.3f;

    private Vector3 lastValue;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ParticleSystem particleSystem;
    private void Awake()
    {
        //rigidbody = GetComponent<Rigidbody>();
        trailRenderer.emitting = false;
        particleSystem.Play();
    }
    public void Movement(Vector2 joystickTouchValue)
    {
        float horizontalInput = joystickTouchValue.x;
        float verticalInput = joystickTouchValue.y;
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 newPosition = rigidbody.position + moveDirection * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(newPosition);
        
        // Check if the joystick is touched.
        if (joystickTouchValue != Vector2.zero)
        {
            rigidbody.MovePosition(newPosition);
            isJoystickTouched = true;
            lastValue = moveDirection;
        }
        else if (isJoystickTouched) // Joystick was released.
        {
            particleSystem.Stop();
            Debug.Log("Started Boost And Dashing");
            StartCoroutine(Dash(lastValue));
            isJoystickTouched = false;
        }
    }

    private IEnumerator Dash(Vector2 joystickTouchLastValue) 
    {
        dashing = true;
        trailRenderer.emitting = true;
        Vector3 moveDirection = new Vector3(joystickTouchLastValue.x, joystickTouchLastValue.y, 0);
        //Vector3 newPosition = rigidbody.position + moveDirection * (boostForce );
        
        rigidbody.velocity = boostSpeed * moveDirection;
        Debug.Log($"Move Direction {moveDirection}");
        
        yield return new WaitForSeconds(dashingTime);
        rigidbody.velocity = Vector3.zero;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashingCoolDown);
        particleSystem.Play();
        dashing = true;
    }
}
