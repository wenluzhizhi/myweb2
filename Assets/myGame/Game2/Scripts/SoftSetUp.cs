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

	[SerializeField] private Text ScoreLabel;
	[SerializeField] private Slider sliderScore;


	public int currenScore=50;

	public PlayerController currentPlayer;
	#region  sigletong

	private static SoftSetUp _instance;
	public  static SoftSetUp Instance
	{
		get{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType (typeof(SoftSetUp)) as SoftSetUp;
			return _instance;
		}

	}

	#endregion

	public string playerNetid="";
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




	public void CurrentPlayerScore(int k)
	{
		currenScore = k;
		sliderScore.value = (float)k / 100.0f;
		ScoreLabel.text = currenScore+"";
	}
}
