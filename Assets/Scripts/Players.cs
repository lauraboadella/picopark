using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class Players : NetworkBehaviour
{


    public float velocidad = 5f;
    public float fuerzaSalto = 12f;


    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    public bool tieneCombustible = false;



    //para que se gire en todas partes hay que hacerlo con networkvariableeeee
    private NetworkVariable<bool> isFlipped = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );


    // private bool puedeSaltar = true;
    // private float ultimoSalto = 0f;




    public override void OnNetworkSpawn() //cuando se inicia la conexion
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer.enabled = true;
        animator.enabled = true;

        spriteRenderer.flipX = isFlipped.Value;
        isFlipped.OnValueChanged += OnFlipChanged;


    }
    //fin de cuando se inicia la conexion




    public override void OnDestroy()
    {
        base.OnDestroy();
        isFlipped.OnValueChanged -= OnFlipChanged;
    }



    void Update()
    {
        if (!IsOwner) return;


        if (Input.GetKeyDown(KeyCode.K)) // tecla de prueba muerte
        {
            DieServerRpc();
        }



        float move = Input.GetAxisRaw("Horizontal");


        //movimiento del player
        transform.position += Vector3.right * move * velocidad * Time.deltaTime;

        // flip con lo de network variable para que se pase de cada player a todas las pantallas
        if (move != 0)
        {
            bool newFlip = move > 0;
            if (newFlip != isFlipped.Value)
                isFlipped.Value = newFlip;
        }


        //salto
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);

        }


        UpdateAnimationServerRpc(move != 0);// hay que mandar los cambios de animacion al server porque ahi es donde funciona el network animator
    }

    [ServerRpc]
    void UpdateAnimationServerRpc(bool andando)
    {
        //.Play no va


        animator.SetBool("Andar", andando);
        //usando la bool en cada animator CON EL MISMO NOMBRE en cada aniamtor tb es la unica forma de que se hagan bien
        //porque networkaniamtor no admite lo .play solo .setbool .settrigger etc
    }

    void OnFlipChanged(bool oldValue, bool newValue)
    {
        spriteRenderer.flipX = newValue;
    }

    //----- cosas para el reset de personaje al morir alguien

    [ServerRpc]
    private void DieServerRpc()
    {
        Debug.Log("va dieserverrpc en player");
        PlayerManager.Instance.RestartLevel();
    }


    //TODOS pueden hacer el reset de TODOS
    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void ResetPlayerServerRpc()
    {
        // ejecutat en servidor para todosss los players
        ResetPlayerClientRpc();
    }



    [ClientRpc]
    private void ResetPlayerClientRpc()
    {
        // ejecutar en todos los clientas
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        animator.Play("Idle");
        isFlipped.Value = false;
        spriteRenderer.flipX = false;
    }



/*
  void OnTriggerEnter2D(Collider2D other)
    {
        //if (!IsOwner) return; 
        
        if (other.CompareTag("Combustible"))
        {
            tieneCombustible = true;
            other.gameObject.SetActive(false);


        }


    }*/
}