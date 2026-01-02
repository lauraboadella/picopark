using UnityEngine;
using Unity.Netcode;

public class camara : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, -10);



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    Players[] jugadores = FindObjectsOfType<Players>();

        foreach (Players jugador in jugadores)
        {
            if (jugador.IsOwner)
            {
                transform.position = jugador.transform.position + offset;
                break;


            }

    }
    }

    
}