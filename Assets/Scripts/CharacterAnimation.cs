using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    string idlePosition = "Idle";
    string walkPosition = "Walk";
    Animator animator;
    bool facingRight = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetOrientation(Vector2 mouseDirection)
    {
        // If pointing right and looking left or pointing left and looking right
        if(mouseDirection.x >= 0 && !facingRight || mouseDirection.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Play animation
    public void Move(Vector2 moveVector)
    {
        if(moveVector.Equals(Vector2.zero))
        {
            animator.Play(idlePosition);
        }
        else
        {
            animator.Play(walkPosition);
        }
    }
}
