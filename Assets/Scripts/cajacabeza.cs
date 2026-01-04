using UnityEngine;
using Unity.Netcode;


[RequireComponent(typeof(Rigidbody2D), typeof(NetworkObject))]
public class cajacabeza : NetworkBehaviour
{
    private Rigidbody2D rb;


    
    private bool isAttached = false;
    private Transform attachedPlayerHead;
    private Vector3 attachOffset = new Vector3(0f, 1f, 0f);



    private float despawnTimer = 0f;
    private const float timeToDespawn = 10f;
<<<<<<< Updated upstream
    private NetworkVariable<bool> isAttachedNetwork = new NetworkVariable<bool>(
     false,
     NetworkVariableReadPermission.Everyone,
     NetworkVariableWritePermission.Server
 );


=======
       private NetworkVariable<bool> isAttachedNetwork = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );
>>>>>>> Stashed changes


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



<<<<<<< Updated upstream



=======
>>>>>>> Stashed changes
    void Start()
    {
        isAttachedNetwork.OnValueChanged += OnAttachedChanged;

<<<<<<< Updated upstream

    }


=======
    }

>>>>>>> Stashed changes
    void Update()
    {
        if (!IsServer) return;

<<<<<<< Updated upstream

  
=======
        // Si est  pegada, sigue la cabeza del jugador
>>>>>>> Stashed changes
        if (isAttached && attachedPlayerHead != null)
        {
            transform.position = attachedPlayerHead.position + attachOffset;
            UpdatePositionClientRpc(attachedPlayerHead.position + attachOffset);

<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            
=======
            // Solo si la caja est  cayendo
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        // Quitar f sica para evitar conflictos al estar pegada
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

<<<<<<< Updated upstream

        isAttachedNetwork.Value = true;
        AttachToPlayerClientRpc(playerHead.GetComponent<NetworkObject>().NetworkObjectId);


    }




    private void OnAttachedChanged(bool oldValue, bool newValue)
=======
        isAttachedNetwork.Value = true;
        AttachToPlayerClientRpc(playerHead.GetComponent<NetworkObject>().NetworkObjectId);

    }


    private void OnAttachedChanged(bool oldValue, bool newValue)
    {
        if (newValue && !IsServer)
        {
            isAttached = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    
    }



    [ClientRpc]
    private void AttachToPlayerClientRpc(ulong playerNetworkId)
    {
        if (IsServer) return;
        NetworkObject playerNetObj = FindNetworkObject(playerNetworkId);
        if (playerNetObj != null)
        {
            attachedPlayerHead = playerNetObj.transform;
            isAttached = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }



    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 newPosition)
    {
        if (IsServer) return;
        
        if (isAttached)
        {
            transform.position = newPosition;
        }
    }



    private NetworkObject FindNetworkObject(ulong networkObjectId)
    {
        NetworkObject[] allObjects = FindObjectsOfType<NetworkObject>();
        foreach (NetworkObject obj in allObjects)
        {
            if (obj.NetworkObjectId == networkObjectId)
            {
                return obj;
            }
        }


        return null;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        isAttachedNetwork.OnValueChanged -= OnAttachedChanged;
        
    }



/*
    // Opcional: si quieres soltar la caja
    public void Detach()
>>>>>>> Stashed changes
    {
        if (newValue && !IsServer)
        {
            isAttached = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

    }

<<<<<<< Updated upstream





    [ClientRpc]
    private void AttachToPlayerClientRpc(ulong playerNetworkId)
    {
        if (IsServer) return;
        NetworkObject playerNetObj = FindNetworkObject(playerNetworkId);
        if (playerNetObj != null)
        {
            attachedPlayerHead = playerNetObj.transform;
            isAttached = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }






    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 newPosition)
    {
        if (IsServer) return;

        if (isAttached)
        {
            transform.position = newPosition;
        }
    }






    private NetworkObject FindNetworkObject(ulong networkObjectId)
    {
        NetworkObject[] allObjects = FindObjectsOfType<NetworkObject>();
        foreach (NetworkObject obj in allObjects)
        {
            if (obj.NetworkObjectId == networkObjectId)
            {
                return obj;
            }
        }




        return null;
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
        isAttachedNetwork.OnValueChanged -= OnAttachedChanged;

    }




=======
    */

    
>>>>>>> Stashed changes
}
