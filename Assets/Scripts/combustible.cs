using UnityEngine;
using Unity.Netcode;

public class combustible : NetworkBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return;
        
        
        if (other.CompareTag("Player"))
        {
            Players player = other.GetComponent<Players>();
            if (player != null)
                player.tieneCombustible = true;
            


            desactivarCombustibleClientRpc();
    }
    }
    
    [ClientRpc]
    void desactivarCombustibleClientRpc()
    {
        gameObject.SetActive(false);

    }
}