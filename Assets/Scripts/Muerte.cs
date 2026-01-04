using UnityEngine;
using Unity.Netcode;

public class Muerte : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {

                
                PlayerManager.Instance.RestartLevel(); 
            }

        
    }
}
