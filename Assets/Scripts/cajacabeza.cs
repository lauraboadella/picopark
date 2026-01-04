using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody2D), typeof(NetworkObject))]
public class BoxHeadAttach : NetworkBehaviour
{
    private Rigidbody2D rb;

    // Variables para la caja pegada
    private bool isAttached = false;
    private Transform attachedPlayerHead;
    private Vector3 attachOffset = new Vector3(0f, 1f, 0f);

    // Timer para despawn
    private float despawnTimer = 0f;
    private const float timeToDespawn = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsServer) return;

       
        if (isAttached && attachedPlayerHead != null)
        {
            transform.position = attachedPlayerHead.position + attachOffset;
        }

       
        if (isAttached)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= timeToDespawn)
            {
                NetworkObject.Despawn();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return;
        if (isAttached) return;

        
        if (other.CompareTag("Cabeza"))
        {
            
            if (rb.linearVelocity.y < -0.1f)
            {
                AttachToPlayer(other.transform);
            }
        }
    }

    private void AttachToPlayer(Transform playerHead)
    {
        attachedPlayerHead = playerHead;
        isAttached = true;
        despawnTimer = 0f;

        
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    
}
