using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class boton : MonoBehaviour
{
    public GameObject botonAbierto;
    public GameObject botonCerrado;
    
    public bool botonActivado = false;

    public enum TipoBoton
    {
        Plataforma,
        Morir
    }
    public TipoBoton tipoboton;

    public GameObject plataforma;
    private Animator animPlataforma;

    public GameObject pinchosmortales;
    //public pincho pincho;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animPlataforma = plataforma.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivarBoton(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivarBoton(false);
        }

    }


    void ActivarBoton(bool mover)
    {
        botonActivado = mover;

        if (botonAbierto != null) botonAbierto.SetActive(!mover);
        if (botonCerrado != null) botonCerrado.SetActive(mover);

        switch (tipoboton)
        {
            case TipoBoton.Plataforma:
                if (animPlataforma != null)
                    animPlataforma.SetBool("Mover", mover);
                break;

            case TipoBoton.Morir:
                //PlayerManager.Instance.RestartLevel();

                    ReiniciarEscenaMultiplayer();

                    break;
        }
    }

    private void ReiniciarEscenaMultiplayer()
    {
        string currentScene = SceneManager.GetActiveScene().name; 
        // Esto sincroniza la carga de la escena para todos los clientes
        NetworkManager.Singleton.SceneManager.LoadScene(currentScene, LoadSceneMode.Single); 
    } 


}

