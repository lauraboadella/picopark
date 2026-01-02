using UnityEngine;
using Unity.Netcode;


public class boton : MonoBehaviour
{
    public GameObject botonAbierto;
    public GameObject botonCerrado;
    
    public bool botonActivado = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }
    
    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!botonActivado && other.CompareTag("Player"))
        {
            ActivarBoton();
        }


    }
    

    void ActivarBoton()
    {
        botonActivado = true;
        
        if (botonAbierto != null) botonAbierto.SetActive(false);
        if (botonCerrado != null) botonCerrado.SetActive(true);
        

    }
}