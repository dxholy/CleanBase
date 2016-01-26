using UnityEngine;
using System.Collections;

public class SuperEvents : MonoBehaviour {

	public static bool Once;

	// Use this for initialization
	void Start () {
	
	
		Once= true;



			}















	
	public static void Ping (string pigeon)//,string message)
	{
		
		switch (GameManager.PigeonEvent.ToString()) 
		{
			
		case "0,ReLoadScene":
			GameManager.ReLoadScene();
			Debug.Log("System Idle");
			break;

		case "1":
			GameManager.CreateWorld();
			Debug.Log("created world");
			break;

		case "2":
			GameManager.PhysicsEnable= true;
			Debug.Log("System Physics Enable");
			GameManager.CreatePhysics();
			Debug.Log("created physics");
			break;

		case "3":
			GameManager.CreateEnemiesInWorld();
			Debug.Log("Creating enemies");
			break;

		case "4":
			GameManager.ReCreateMyScene();
			Debug.Log("Loading A Saved Scene");
			break;
		
		case "5":
			GameManager.ReleaseEnemies();
			Debug.Log("Releasing Enemies");
			break;


  
			
		case "11":

 
			break;

			
		case "12":
			Debug.Log("System Idle");
			break;
			
		case "13":
			Debug.Log("System Idle");
			break;


			
		case "101,MouseLeftClick":

			Debug.Log("wow");
			break;
			
		case "102,MouseRightClick":
			Debug.Log("wow");
			break;
			
		case "103,MouseMiddleClick":
			Debug.Log("wow");
			break;
			



			
			
		default :
			Debug.Log("...... nothing particular");
			break;
		}
		Debug.Log (pigeon);
		
		
		
	}
	 

	
	// Update is called once per frame
	void Update () {
	
		




		/*

		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.FireStringEvents ("0,ReLoadScene"); // Reload Scene file
		
			GameManager.FireStringEvents ("1"); // CREATE WORLD

			GameManager.FireStringEvents ("2"); // Create Physics 


		}

*/

	}
}
