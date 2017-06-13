using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MyGame.Two{


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
	[SerializeField] private Text enemyCountLabel;


	public int currenScore=0;
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




	public List<PlayerController> listPlayers = new List<PlayerController> ();
		public GenerateAI generateAI;
	
	[SyncVar]
	private int currentSenceEnemyCount=0;
	public int CurrentSenceEnemyCount
	{
		get{return currentSenceEnemyCount; }
		set
		{ 
			currentSenceEnemyCount = value;
			if (currentSenceEnemyCount <= 0) {
				Debug.Log ("enemy kill to zero.....");
			}
			enemyCountLabel.text = "Enemy: " + currentSenceEnemyCount;
				if (generateAI != null&&currentSenceEnemyCount<=3) {
					generateAI.generateAI (10);
				}
		}
	}
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

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();

	}
	public void CurrentPlayerScore(int k)
	{
		currenScore = k;
		sliderScore.value = (float)k / 100.0f;
		ScoreLabel.text = "Score: "+currenScore+"";
	}



	public void AddPlayer(PlayerController p)
	{
		if (listPlayers.Contains (p))
			return;
		listPlayers.Add (p);

		
	}

	public void RemovePlayer(PlayerController p){
		if (listPlayers.Contains (p)) {
			listPlayers.Remove (p);
		}
	}
}



}