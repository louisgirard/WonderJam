using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    float xInput, yInput;
    new Rigidbody2D rigidbody;
    Animator playerAnimator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        Vector2 moveVector = new Vector2(xInput, yInput).normalized;

        // Update Position
        if (!moveVector.Equals(Vector2.zero))
        {
            rigidbody.MovePosition(rigidbody.position + moveVector * speed * Time.fixedDeltaTime);
            playerAnimator.SetTrigger("walk");
        }
        else
        {
            playerAnimator.SetTrigger("stopWalk");
        }
    }
}
