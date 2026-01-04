using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public Button buttonHost;
    public Button buttonClient;
    public Button buttonReset;

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

    public void resetlevel()
    {
        //PlayerManager.Instance.RestartLevel();
        ReiniciarEscenaMultiplayer();
    }

    private void ReiniciarEscenaMultiplayer()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        // Esto sincroniza la carga de la escena para todos los clientes
        NetworkManager.Singleton.SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
    }

}
