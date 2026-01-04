using Unity.Netcode;
using UnityEngine;


public class pinchoTecho : NetworkBehaviour
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

            ActivarGravedadServerRpc();
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
            DesactivarPinchoServerRpc();
        }
    }

    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    void ActivarGravedadServerRpc()
    {
        rb.gravityScale = 3f;
    }

    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    void DesactivarPinchoServerRpc()
    {
        DesactivarPinchoClientRpc();
    }

    [ClientRpc]
    void DesactivarPinchoClientRpc()
    {
        gameObject.SetActive(false);
    }


}
