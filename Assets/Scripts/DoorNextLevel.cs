using UnityEngine;

public class DoorNextLevel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.m_instance.LoadNextLevel();
        }
    }
}
