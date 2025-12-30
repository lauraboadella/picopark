using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public Button buttonHost;
    public Button buttonClient;

    void Start()
    {

    }

    public void connecthost()
    {
        NetworkManager.Singleton.StartHost();

    }


    public void connectclient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
