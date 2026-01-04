using UnityEngine;
using Unity.Netcode;

public class pinchoTecho : NetworkBehaviour
{
    public float gravedad = 7f;
    private Rigidbody2D rb;
    public GameObject pantallaGameOver;
    public float tiempoEspera = 3f;

    private bool pinchoCae = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer || pinchoCae) return;
        if (other.CompareTag("Player"))
        {
            pinchoCae = true;
            PinchoCaeClientRpc();

        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsServer) return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            GameOver();

        }
    }
    
    [ClientRpc]
    void PinchoCaeClientRpc()
    {
        if (rb != null)
        {
            rb.gravityScale = gravedad;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        }
    }
    
    void GameOver()
    {
        GameOverClientRpc();
        
        if (IsServer)
        {
            Invoke("ReiniciarPartida", tiempoEspera);
        }


    }
    
    [ClientRpc]
    void GameOverClientRpc()
    {
        if (pantallaGameOver != null)
            pantallaGameOver.SetActive(true);
        
    Time.timeScale = 0f;
    }
    

    
    void ReiniciarPartida()
    {
        if (IsServer)
        {
            ReiniciarClientRpc();
        }
    }
    


    [ClientRpc]
    void ReiniciarClientRpc()
    {
        Time.timeScale = 1f;
        
        if (pantallaGameOver != null)
            pantallaGameOver.SetActive(false);


    }
}