using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerOrientation : MonoBehaviour
{
    bool facingRight = true;

    void Update()
    {
        Vector2 mouseDirection = GetOrientation();
        // If pointing right and looking left or pointing left and looking right
        if (mouseDirection.x >= 0 && !facingRight || mouseDirection.x < 0 && facingRight)
        {
            Flip();
        }
    }

    public Vector2 GetOrientation()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Mouse origin at the center of the screen
        mousePosition.x -= Screen.width / 2;
        mousePosition.y -= Screen.height / 2;

        return new Vector2(mousePosition.x, mousePosition.y).normalized;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
