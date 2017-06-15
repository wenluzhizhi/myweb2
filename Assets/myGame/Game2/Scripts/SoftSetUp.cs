using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MyGame.Two
{


	public enum SystemType
	{
		Host,
        Server,
        Client,
	}

	public class SoftSetUp :NetworkBehaviour
	{


   
		[SerializeField]
		private Image startBtn;
		[SerializeField] private Text ScoreLabel;
		[SerializeField] private Slider sliderScore;
		[SerializeField] private Text enemyCountLabel;


		public int currenScore = 0;
		public PlayerController currentPlayer;

		#region  sigletong

		private static SoftSetUp _instance;

		public  static SoftSetUp Instance {
			get {
				if (_instance == null) {
					_instance = GameObject.FindObjectOfType (typeof(SoftSetUp)) as SoftSetUp;
				}
				if (_instance == null) {
					Debug.Log ("cannot get SoftSeteUp instance---" + Time.time);
				}

				return _instance;
			}

		}

		#endregion


		public List<PlayerController> listPlayers = new List<PlayerController> ();
		public GenerateAI generateAI;
		[SyncVar(hook="OnChangeEnemyCount")]
		public int currentSenceEnemyCount;

		[SyncVar(hook="OnChagePlayerCount")]
		public int CurrentGamePlayerCounts;

	

		#region  hook Event
	
		public void OnChangeEnemyCount(int count)
		{
			MainUIController.Instance.SetCurrentSenceEnemyCount (count);
		}

		public void OnChagePlayerCount(int count){
			MainUIController.Instance.SetCurrentOnlinePlayerCount (count);
		}
		#endregion

		public void geneTankAI(){
			if (currentSenceEnemyCount <= 3)
			{
				generateAI.SpawnTankAI (10);
			}
		}




		#region  Players Manager

		private void removeInvalidPlayer(){
			foreach (PlayerController p in listPlayers) {
				if (p == null) {
					listPlayers.Remove (p);
				}
			}
		}
		public void AddPlayer (PlayerController p)
		{
			removeInvalidPlayer ();
			if (listPlayers.Contains (p))
				return;
			listPlayers.Add (p);
			CurrentGamePlayerCounts=listPlayers.Count;
		}

		public void RemovePlayer (PlayerController p)
		{
			removeInvalidPlayer ();
			if (listPlayers.Contains (p))
			{
				listPlayers.Remove (p);
			}
			CurrentGamePlayerCounts=listPlayers.Count;
		}



		public Transform GetFollowPlayer(){
			removeInvalidPlayer ();
			if (listPlayers.Count == 0)
				return null;
			int _childCount = listPlayers.Count;
			int _random = Utils.getRandom1 ()%_childCount;
			if (_random < _childCount) {
				return listPlayers[_random].gameObject.transform;
			}
			return null;
		}
		#endregion 
	}



}