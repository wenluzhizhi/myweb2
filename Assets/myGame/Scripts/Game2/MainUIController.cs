using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace MyGame.Two
{


	public class MainUIController : MonoBehaviour
	{

		#region singleton

		private static MainUIController _instnce;

		public static MainUIController Instance {
			get { 
				if (_instnce == null) {
					_instnce = GameObject.FindObjectOfType (typeof(MainUIController)) as MainUIController;
				}
				if (_instnce == null) {
					Debug.Log ("MianUIController get singleton failed" + Time.time);
				}
				return _instnce;
			}
		}

		#endregion

		#region var

		[SerializeField] private Text playerScoreText;
		[SerializeField] private Text currentEnemyCountText;
		[SerializeField] private Text currentOnlineCountText;
		[SerializeField] private Button startGameButton;
		public SystemType systemType = SystemType.Host;
		#endregion

		#region  mono

		void Start()
		{
			
		    SetPlayerScore (0);
		    SetCurrentSenceEnemyCount (0);

		}

		#endregion
		public void SetPlayerScore(int score){
			playerScoreText.text = "Score: " + score;
		}
		public void SetCurrentSenceEnemyCount(int count)
		{
			currentEnemyCountText.text = "Enemy: " + count;
		}

		public void SetCurrentOnlinePlayerCount(int count){
			currentOnlineCountText.text = "Online: "+count;
		}

		#region  OnClickEvent
		public void OnClickStartGame()
		{
			Debug.Log ("user click the start button "+Time.time);
			if (systemType == SystemType.Server) 
			{
				NetworkManager.singleton.StartServer ();
			}
			if (systemType == SystemType.Host) 
			{
				NetworkManager.singleton.StartHost ();
			}
			if (systemType == SystemType.Client) 
			{
				NetworkManager.singleton.StartClient ();
			}
		}
		#endregion


		public void finishLaunchGame(){
			startGameButton.gameObject.SetActive (false);
			if (SoftSetUp.Instance != null) {
				SetPlayerScore (SoftSetUp.Instance.currenScore);
				SetCurrentSenceEnemyCount (SoftSetUp.Instance.currentSenceEnemyCount);
			}
		}







	}


}