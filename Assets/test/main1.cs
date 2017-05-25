using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class main1 : MonoBehaviour
{


	void Start () {
	
	}
	

	void Update () {
	
	}

	public List<int> list = new List<int> ();
	void OnGUI(){

		if (GUILayout.Button ("Test")) {

			StringBuilder sb = new StringBuilder ();
			for (int i = 0; i < list.Count; i++) {
				sb.Append (list[i]+",");
			}
			Debug.Log (sb.ToString());


			for (int i = 0; i < list.Count; i++) {
				for (int j = 0; j < list.Count - 1 - i; j++) {
					if (list [j] > list [j + 1]) {

						int t=list[j];
						list [j] = list [j + 1];
				        list[j+1]=t;
					}
				}
			}

			sb.Remove (0,sb.Length);
			for (int i = 0; i < list.Count; i++) {
				sb.Append (list[i]+",");
			}

			Debug.Log (sb.ToString());
				
		}
	
	
		if (GUILayout.Button ("Test22222")) {

			for(int i=3;i<100;i++){
				if (isPrimeNum (i)) {

					Debug.Log (i);
				}				
			}
		}
	
	}

	public bool isPrimeNum(int n){

		bool isPrime = true;
		for (int i = 2; i < n; i++) {

			if (n % i == 0) {
				isPrime = false;
				break;
			}
		}
		return isPrime;
	}
}
