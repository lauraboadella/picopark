using UnityEngine;
using Unity.Netcode;


public class BoxHeadAttach : NetworkBehaviour
{
    private Rigidbody2D rb;
    private FixedJoint2D joint;

    private float despawnTimer = 0f;
    private const float TIME_TO_DESPAWN = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!IsServer) return;

       
        if (joint != null) //contador cuando caja cae en cabeza
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= TIME_TO_DESPAWN)
            {
                NetworkObject.Despawn();
            }
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsServer) return;
        if (joint != null) return;

        if (other.CompareTag("Cabeza"))
        {
            // solo si la caja está cayendo
            if (rb.linearVelocity.y < -0.1f)
            {
                Rigidbody2D playerRb = other.transform.parent.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    AttachToPlayer(playerRb);
                }
            }
        }
    }

    
    private void AttachToPlayer(Rigidbody2D playerRb)
    {
        joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = playerRb;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0f, 1f); 

        despawnTimer = 0f;

    }

   
    void OnJointBreak2D(Joint2D brokenJoint)
    {
        if (!IsServer) return;

        if (brokenJoint == joint)
        {
            joint = null;
        }
    }
}
