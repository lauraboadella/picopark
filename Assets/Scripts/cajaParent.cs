using UnityEngine;

public class cajaParent : MonoBehaviour
{

    /*

    private Rigidbody2D rb;
    private Transform target = null;

    private Vector3 offset = new Vector3(0, 1f, 0);

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();



    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && target == null)
        {
            target = collision.transform;
            rb.isKinematic = true;
        
        }
    }
    
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 ponerEnCabeza = target.position + offset; //posicion jugador + offset vertical
            transform.position = Vector3.Lerp(transform.position, ponerEnCabeza, 10f * Time.deltaTime);

        }
    }
    
    void Update()
    {
        if (target != null && Input.GetKeyDown(KeyCode.E))
        {
            target = null;
            rb.isKinematic = false;
        }
    }




    */
}