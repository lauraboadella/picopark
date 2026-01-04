using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerManager : NetworkBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs; // para 4 players

    private ResetObject[] objetosReseteables;

    //private bool restarting = false;


    public static PlayerManager Instance;

    public override void OnNetworkSpawn() //cuando se sincroniza con la red
    {
        if (!IsServer) return;

        if (IsServer)
            Instance = this;

        objetosReseteables = GameObject.FindObjectsByType<ResetObject>(FindObjectsSortMode.None);


        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        // cuando se conecta un nuevo cliente  ->  se spawnea un jugador nuevo
    }



    private void OnClientConnected(ulong clientId) //lo llama cuando alguien se conecta
    {
        int index = (int)(clientId % (ulong)playerPrefabs.Length); // selecciona el prefab que toca para cada jugador segun su orden.

        GameObject prefab = playerPrefabs[index]; //usa ese index que acaba de sacar para instanciar el prefab que toca para cada jugador

        //sale en el (0,0,0);
        GameObject player = Instantiate(prefab, Vector3.zero, Quaternion.identity);



        // con esto le das el ownership del network object al cliente que toca para que pueda
        //moverse por ahi y que todos lo vean a tiempo real. true para que sea PLAYER dentro del netcode
        //Y ASIII el network transform y network animator funcionan los dos todo bien sin cosas extrañas!!!!
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }

    public override void OnDestroy()
    {

        if (NetworkManager.Singleton != null)
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }



    //-----


    public void RestartLevel()
    {

        //--- objetos
        if (!IsServer) return;
        Debug.Log("se hace RestartLevel");
        foreach (var obj in objetosReseteables)
        {
            Debug.Log("se hace el foreach");
            obj.Reset();
        }



        //---- players



        // devolver a TODOS AHORA SI al punto inciial
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            Debug.Log("se hace el foreach de reset jugadpres");
            var playerScript = client.PlayerObject.GetComponent<Players>();
            if (client.PlayerObject != null)
            {
                Debug.Log("se hace el resetplayerserverrpc");
                //client.PlayerObject.transform.position = Vector3.zero;
                playerScript.ResetPlayerServerRpc();
            }
        }
    }
}



