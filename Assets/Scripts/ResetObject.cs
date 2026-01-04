using UnityEngine;
using Unity.Netcode;

public class ResetObject : NetworkBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialActive;
    private float initialGravity;

    private Rigidbody2D rb;




    /* private void Start()
     {
         transform.position = transform.position;


         initialPosition = transform.position;

         initialActive = gameObject.activeSelf;

         rb = GetComponent<Rigidbody2D>();
         if (rb != null)
         {
             initialGravity = rb.gravityScale;
         }
     }*/
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

       
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialActive = gameObject.activeSelf;
        if (rb != null)
            initialGravity = rb.gravityScale;
    }

    public void Reset()
    {
        if (IsServer) 
        {


                ResetObjectClientRpc(initialPosition, initialActive, initialGravity);
        }
    }

    [ClientRpc]
    void ResetObjectClientRpc(Vector3 pos, bool active, float gravity)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = gravity;
        }

        transform.position = pos;
        
        gameObject.SetActive(initialActive);
    }
}
