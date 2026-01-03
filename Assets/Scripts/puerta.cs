using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class puerta : NetworkBehaviour
{
    public GameObject puertaCerrada;
    public GameObject pantallaWin;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return;
        if (other.CompareTag("Player"))
        {
            Players jugador = other.GetComponent<Players>();
            
            if (jugador != null && jugador.tieneCombustible)
            {
                DesactivarPuertaClientRpc();

            }


        }
    }

    [ClientRpc]
    void DesactivarPuertaClientRpc()
    {
            puertaCerrada.SetActive(false);

    
    }


    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Players>().IsOwner)
        {
            Players jugador = other.GetComponent<Players>();
            if (jugador.tieneCombustible && Input.GetKeyDown(KeyCode.W))
            {
                PantallaWinClientRpc();

            }
        }
    }
    
    [ClientRpc]
    void PantallaWinClientRpc()
    {

            pantallaWin.SetActive(true);
    }


}