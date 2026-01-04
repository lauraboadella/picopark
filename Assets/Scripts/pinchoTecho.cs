using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            rb.gravityScale = 3f; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerManager.Instance.RestartLevel();
        }
        if (collision.collider.CompareTag("Suelo"))
        {
            gameObject.SetActive(false);
        }
    }




    private void ReiniciarEscenaMultiplayer()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        // Esto sincroniza la carga de la escena para todos los clientes
        NetworkManager.Singleton.SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
    }
}
