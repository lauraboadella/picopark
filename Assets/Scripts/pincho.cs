using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;


public class pincho : MonoBehaviour
{
    public GameObject pantallaGameOver;
    public float tiempoEspera = 3f;
    
    void Start()
    {

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameOver();
        }


    }
    
    void GameOver()
    {
        if (pantallaGameOver != null)
            pantallaGameOver.SetActive(true);
        


        Time.timeScale = 0f;
        Invoke("ReiniciarPartida", tiempoEspera);

    }
    
    void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
}