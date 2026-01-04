using UnityEngine;
using Unity.Netcode;


[RequireComponent(typeof(Rigidbody2D), typeof(NetworkObject))]
public class cajacabeza : NetworkBehaviour
{
    private Rigidbody2D rb;


    // Variables para la caja pegada
    private bool isAttached = false;
    private Transform attachedPlayerHead;
    private Vector3 attachOffset = new Vector3(0f, 1f, 0f);


    // Timer para despawn
    private float despawnTimer = 0f;
    private const float timeToDespawn = 10f;
    private NetworkVariable<bool> isAttachedNetwork = new NetworkVariable<bool>(
     false,
     NetworkVariableReadPermission.Everyone,
     NetworkVariableWritePermission.Server
 );




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }






    void Start()
    {
        isAttachedNetwork.OnValueChanged += OnAttachedChanged;


    }


    void Update()
    {
        if (!IsServer) return;


        // Si est  pegada, sigue la cabeza del jugador
        if (isAttached && attachedPlayerHead != null)
        {
            transform.position = attachedPlayerHead.position + attachOffset;
            UpdatePositionClientRpc(attachedPlayerHead.position + attachOffset);


        }


        // Timer para despawn
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


        // Solo detecta la cabeza
        if (other.CompareTag("Cabeza"))
        {
            // Solo si la caja est  cayendo
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


        // Quitar f sica para evitar conflictos al estar pegada
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;


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

    [ServerRpc]
    public void PushServerRpc(Vector2 force)
    {
        rb.AddForce(force); // Aplica la fuerza solo en el servidor
    }



}
