using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum SystemType {
    Host,Server,Client,
}
public class SoftSetUp :NetworkBehaviour
{

    public SystemType systemType = SystemType.Host;
    [SerializeField]
    private NetworkManager networkManager;
    [SerializeField]
    private Image startBtn;
    void Start()
    {
        networkManager = this.gameObject.GetComponent<NetworkManager>();
        
       
    }


    public void OnClickStartGame()
    {
        if (networkManager.isNetworkActive)
        {
            Debug.Log("游戏正在运行...");
            return;
        }
        if (systemType == SystemType.Server)
        {
            networkManager.StartServer();
        }
        else if (systemType == SystemType.Client)
        {
            networkManager.StartClient();
        }
        else if (systemType == SystemType.Host)
        {
            networkManager.StartHost();
        }
        startBtn.gameObject.SetActive(false);

    }



    public override void OnStartClient()
    {
        base.OnStartClient();
    }


    public override void OnStartServer()
    {
        base.OnStartServer();
    }
}
