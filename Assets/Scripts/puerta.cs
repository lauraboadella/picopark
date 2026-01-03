using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class puerta : NetworkBehaviour
{
    public GameObject puertaCerrada;
    public GameObject pantallaWin;

    private NetworkVariable<bool> jugadorEnTrigger = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);


    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return;
        if (other.CompareTag("Player"))
        {
           Players jugador = other.GetComponent<Players>();
            
            

            
            if (jugador != null && jugador.tieneCombustible)
            {
                jugadorEnTrigger.Value = true;
                DesactivarPuertaClientRpc();
            }


        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Players jugador = other.GetComponent<Players>();
        //if (jugador != null && jugador.IsOwner)
        if(other.CompareTag("Player"))
        {
            jugadorEnTrigger.Value = false;
        }
    }

    private void Update()
    {
        if (!IsClient) return;
       
        if (Input.GetKeyDown(KeyCode.W) && jugadorEnTrigger.Value)
        {
            WinServerRpc();
        }
    }





  /* void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Players jugador = other.GetComponent<Players>();
            //if (jugador.tieneCombustible && Input.GetKeyDown(KeyCode.W))
            if (jugador.tieneCombustible) 
            {
                //PantallaWinClientRpc();
                jugadorEnTrigger = true;
            }
        }
    }
  */

    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    void WinServerRpc()
    {
        if(jugadorEnTrigger.Value)
        PantallaWinClientRpc();
    }

    [ClientRpc]
    void DesactivarPuertaClientRpc()
    {
        puertaCerrada.SetActive(false);
    }

    [ClientRpc]
    void PantallaWinClientRpc()
    {
            pantallaWin.SetActive(true);
    }


}