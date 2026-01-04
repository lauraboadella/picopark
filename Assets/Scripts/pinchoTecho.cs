using UnityEngine;

public class pinchoTecho : MonoBehaviour
{
    private Rigidbody2D rb;
    

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            rb.gravityScale = 1f; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerManager.Instance.RestartLevel();
        }
    }
}
