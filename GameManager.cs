using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Dijkstra;





public class GameManager : MonoBehaviour {

	public static GameManager _instance;
	
	public static GameManager Instance
	{
		get
		{
			if(_instance == null)
			{
				GameObject go = new GameObject("GameManager");
				go.AddComponent<GameManager>();
			}
			return _instance;
		}
	}


		public static bool PlayIntro;

		public static bool PhysicsEnable;
		public static string PigeonEvent;
		public static int score;
		public static float inc,spreaderX,spreaderZ=0;

		//public bool isDead;

		public static Transform root;	

		void Awake()
		{
			_instance = this;

		}

	//	
	//public static List<GameObject> GardenData = new List<GameObject>();
	//public static List<Vector3> GardenXYZ = new List<Vector3>();
	//public static int UnitNumber = 0;
	//public static GameObject abc ;  // Generic object Sphere


	public static GameObject[] abc;
	public static GameObject[] def;  // Generic object Sphere
	public static List<GameObject> GardenData = new List<GameObject>();
	public static List<Vector3> GardenXYZ = new List<Vector3>();
	public static int UnitNumber = 1;
	public static int UnitNumberDel =0;

		void Start()
		{
			PlayIntro = true;

			
			

			PhysicsEnable = false;
			PigeonEvent = "empty";  // IDLE

			score = 0;
			 

	 

			root = this.transform;
			while (root.parent != null) 
			{
				root = root.parent;
			}






		}
	
		void SuperPing(string pigeon)//,string message)
		{
			SuperEvents.Ping (pigeon);//,message);
		}



		public static void CreateWorld()
		{	
/*

			GardenXYZ.Add( new Vector3(0,score,0));
			

			foreach(GameObject Garden in GardenData)
			{
				UnitNumber =GardenData.Count;
				abc = GameObject.Instantiate(GameObject.Find ("My_Object1"),
				                             GardenXYZ[UnitNumber],
				                             Quaternion.identity) as GameObject;
				//	Debug.Log ("GXYZ = "+GardenXYZ[UnitNumber].ToString());
				abc.transform.position = GardenXYZ[UnitNumber];	
		

			}
			*/
	
		}
	public static void CreateEnemiesInWorld()
	{	
		

		
		spreaderX = 0+ 200* Mathf.Sin(inc);
		spreaderZ = 0+ 200* Mathf.Cos(inc);

		GardenXYZ.Add( new Vector3(score*spreaderX,50,spreaderZ*score));

		score+=10;	

 
		
		foreach(GameObject Garden in GardenData)
		{

			Garden.name = Garden.name;

			UnitNumber = GardenData.Count;
		//	def[UnitNumber]  = Instantiate (Zombie_prefab);
		
			def[UnitNumber] =GameObject.Instantiate(GameObject.Find ("My_Object1.1"),
			                            GardenXYZ[UnitNumber],
			                           Quaternion.identity) as GameObject;




			//	Debug.Log ("GXYZ = "+GardenXYZ[UnitNumber].ToString());
			def[UnitNumber].transform.position = GardenXYZ[UnitNumber];	

			def[UnitNumber].name = "My_Object1.1"+UnitNumber.ToString();
			 
			def[UnitNumber].GetComponent<Renderer>().material.color = Constants.blue;

		}

		 

	}


	public static void ReleaseEnemies()
	{

		//if(	GameObject.Find("My_Object2").name != null)
		GameObject.DestroyImmediate(GameObject.Find("My_Object1.1"));

		//if(	GameObject.Find("My_Object2(Clone)").name != null) 
		GameObject.DestroyImmediate(GameObject.Find("My_Object1.1(Clone)"));

 

	}



	
	public static void ReCreateMyScene()
	{
		int incr = 0;
		foreach(GameObject Garden in GardenData)
		{
			Garden.name = Garden.name;
			if(incr < UnitNumber)
			GardenData.Add(def[incr++]); 
			Debug.Log("Whats going on !!");

		}
		
	//
		
		
		
	}



	
	
	public static void CreatePhysics()
	{
		
		
		if (PhysicsEnable == true) 
		{
			//	def[UnitNumber].AddComponent<Rigidbody>();	
			//	def[UnitNumber].GetComponent<Rigidbody>().AddForce(Vector3.up *100,ForceMode.Force);			
		}	
	}





	public static void ReLoadMenu()
	{
		
		Application.LoadLevel(0);
		
		
		
		
	}
	public static void ReLoadScene()
	{

		Application.LoadLevel(0);




	}




	public static void SpawnAUnit()
	{
		
		//foreach (GameObject Garden in GardenData) {
		UnitNumber = GardenData.Count;
		
		
		
		
		def [UnitNumber] = GameObject.Instantiate (GameObject.Find ("My_Object1.1"),
		                                           GardenXYZ [UnitNumber],
		                                           Quaternion.identity) as GameObject;
		
		
		//	Debug.Log ("GXYZ = "+GardenXYZ[UnitNumber].ToString());
		//	def [UnitNumber].transform.position = GardenXYZ [UnitNumber];	
		
		
		
		def[0].GetComponent<Renderer>().material.color = Constants.blue;
		
		def[UnitNumber].gameObject.name = "My_Object1"+".1"+UnitNumber.ToString();
		
		Debug.Log ("def [UnitNumber] ="+UnitNumber.ToString());


	}
	public static void SpawnAWorldFullOfZombies()
	{
		
		GameManager.FireStringEvents ("0,ReLoadScene"); 
		
		GameManager.FireStringEvents ("1"); 
		
		GameManager.FireStringEvents ("2");  
		
		GameManager.FireStringEvents ("3"); // ENEMIES
		
		GameManager.FireStringEvents ("4");  // ADD DEF

	}
	public static void KillAllZombies()
	{
		GameManager.FireStringEvents ("5");  // delete all enemies
	}


	void OnGUI()
	{

		
	//	if (GUI.Button (new Rect (250, 300, 200, 100), "Add Node") == true) 
//		{
  		 
//		}

	 

 

	}



	public static void FireStringEvents(string pigeon)
		{
			PigeonEvent = pigeon;//.ToString ();

			switch(PigeonEvent)
			{
			/*
			case "0,ReLoadScene":
				root.BroadcastMessage("SuperPing","0,ReLoadScene");
				break;
 
			*/	
				default:
				root.BroadcastMessage("SuperPing",PigeonEvent.ToString());
				break;
				
			} // switch

		}





		void Update()
		{	
		UnitNumber = GardenData.Count;

		inc += 0.5f;


 




   			if(Input.GetKeyDown( KeyCode.Backspace))
			{
					
			 	GameManager.FireStringEvents ("0,ReLoadScene"); // Release Enemies
			 

			} // Input.keydown Space



 		} // Update




}
