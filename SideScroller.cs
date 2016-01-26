using UnityEngine;
using System.Collections;

public class SideScroller : MonoBehaviour {
	public Vector3 playerpos,playerpos2,sinker;
	public Vector3 playerscale,playerscale2,temp_scale,temp_scale2;

	public bool Facing_Right,StandingStill;

 
	public bool once;
	public bool sink;
	public bool resume;

	public int shroud_collision_count,prev_shroud_collision_count;
	public double timepass_to_shrink;
	public float ground_height;

	public   Texture2D   borderTexture;
	
	Color rgb_color;
	GUIStyle generic_style;



	public enum Size
	{
		growing,
		shrinking,
		stable
		
	};
	Size Character_mode;

	// Use this for initialization
	void Start () {

 
		resume = true;
		Character_mode = Size.stable;
		ground_height = 20.0f;
		timepass_to_shrink = 0;
		sink = false;
		StandingStill= true;
		Facing_Right = true;
		once = true;	
		shroud_collision_count = 0; 
		prev_shroud_collision_count = 0;


	//	 
	 //	MovementManager.currentObj = GameObject.Find("convict");
	 //	MovementManager.currentTarg = GameObject.Find("My_Object1");

		borderTexture = (Texture2D)Resources.Load("test") as Texture2D;
	 





		transform.gameObject.AddComponent<Rigidbody>();	

		transform.GetComponent<Rigidbody> ().mass = 1.0001f;
		transform.GetComponent<Rigidbody> ().drag = 0.01f;
		transform.GetComponent<Rigidbody> ().useGravity= true;
		transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
 


		GameObject.Find("My_Object4").AddComponent<Rigidbody>();	
		
		GameObject.Find("My_Object4").GetComponent<Rigidbody> ().mass = 1.0001f;
	 	GameObject.Find("My_Object4").GetComponent<Rigidbody> ().drag = 0.01f;
		GameObject.Find("My_Object4").GetComponent<Rigidbody> ().useGravity= false;
		GameObject.Find("My_Object4").GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionY;



	//	rb = GameObject.Find("My_Object2").GetComponent<Rigidbody>();

		playerpos = new Vector3 (GameObject.Find ("My_Object2").transform.position.x,
		                         GameObject.Find ("My_Object2").transform.position.y,
		                         GameObject.Find ("My_Object2").transform.position.z);

	//	GameObject.Find("My_Object2").GetComponent<Rigidbody>().AddForce(Vector3.up*100000);
	
		playerpos2 = playerpos;
		playerscale = new Vector3(1.50f,1.50f,1.50f);
		playerscale2 = new Vector3(4.5f,4.5f,4.5f);
		temp_scale = new Vector3(1.5f,1.5f,1.5f);
		temp_scale2 = new Vector3(1.5f,1.5f,1.5f);

		sinker = GameObject.Find ("My_Object4").transform.position;
	}

	 
//                     1 1.5  3  4.5 
//

	void OnGUI()
	{
		GUI.Label(new Rect(300,100,100,100),shroud_collision_count.ToString());


		generic_style = new GUIStyle();
		GUI.skin.box  = generic_style;
 


		GUI.Box (new Rect (870,-30,280,280),borderTexture,GUIStyle.none );
		GUI.Box (new Rect (900,10,220,220),GameObject.Find("Follower_Camera").GetComponent<Camera>().targetTexture ,GUIStyle.none);







		if(GUI.Button(new Rect(100,100,100,100),"Grow"))
		{
			if(shroud_collision_count<4)
			{


		 	shroud_collision_count++;
			Character_mode = Size.growing;
			}

		}

		if(GUI.Button(new Rect(100,200,100,100),"Shrink"))
		  	{
			if(shroud_collision_count>0)
			{
				shroud_collision_count--;

				Character_mode = Size.shrinking;
			}
			}

		 
		
		if (GUI.Button (new Rect (550, 100, 200, 100), "Spawn a Unit") == true) 
		{
			
			
			GameManager.SpawnAUnit();
			
		}
		
		
		
		
		//GUI.Label(new Rect(900,400,400,400), "Units deleted = "+UnitNumberDel.ToString() );
		
//		GUI.Label(new Rect(900,200,400,400), "Units = "+UnitNumber.ToString() );
		
		
		if(GUI.Button (new Rect (50, 0, 300, 100), "Spawn a World with zombies") == true) 
		{
			GameManager.SpawnAWorldFullOfZombies();
		}
		
		if (GUI.Button (new Rect (50, 300, 200, 100), "Kill all zombie") == true) 
		{
			
			GameManager.KillAllZombies();
			
		}


	}


	void simulate_time()
	{
		//while (!Input.GetMouseButtonDown(0))
		{
			timepass_to_shrink++;
		}
	}






	void simulate_gravity()
	{
		if (sinker.y > 0 && sink == true) {
			
			sinker.y -= 10f;
		//	GameObject.Find ("My_Object2").transform.position = sinker;
			
		}
		
		if (sinker.y > -20 && sink == true) {
			Debug.Log ("less than -20");
			playerpos.y = ground_height;
			sinker = playerpos;
			//GameObject.Find ("My_Object2").transform.position = sinker;
		}
		
		
		
		if (playerpos.y > 1000) {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 100, ForceMode.Force);
		}
		
		this.GetComponent<Rigidbody> ().AddForce (Vector3.up * -150, ForceMode.Force);
		
		
		
		
		StandingStill = false;
		
		
		if (sink) {
			playerpos.y -= 4.6f;
			
			
			GameObject.Find ("My_Object2").GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			
			GameObject.Find ("My_Object2").GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			GameObject.Find ("My_Object2").GetComponent<Rigidbody> ().mass = 1.0001f;
			GameObject.Find ("My_Object2").GetComponent<Rigidbody> ().useGravity = true;
			
			
			//	GameObject.Find("My_Object4").transform.position = playerpos;
		}
		
		//GameObject.Find ("Character").transform.localScale /= playerscale2.x ;

	}



	void stable()
	{
	// 	temp_scale = new Vector3(1,1,1);
	 //	temp_scale2 = new Vector3(5,5,5);
 

		if (shroud_collision_count > prev_shroud_collision_count && timepass_to_shrink > 0 ) {
			
			Character_mode = Size.growing;
		}


		  if (
			(temp_scale.x - shroud_collision_count) > 4.0 && (temp_scale.x - shroud_collision_count) < 6.0
		
			&& shroud_collision_count < 4 && shroud_collision_count > 0) {
			
			Character_mode = Size.shrinking;
		}


		if( timepass_to_shrink > 100 )
		{
			Character_mode = Size.shrinking;

		}




	//	GameObject.Find ("Character").transform.localScale = new Vector3 (1,1, 1);
	}


	void shrink()  //doesnt work ?
	{
				
			

		temp_scale2 = playerscale2;
 
 
		if (shroud_collision_count > 1) {
			temp_scale2.x =  0.99f*shroud_collision_count;
			temp_scale2.y =  0.99f*shroud_collision_count;
			temp_scale2.z =  0.99f*shroud_collision_count;
		 
		}


		GameObject.Find ("Character").transform.localScale =( playerscale2 );


	 //	if ((temp_scale2.x - shroud_collision_count) > 0.0 && (temp_scale2.x - shroud_collision_count) < 0.5) 
		{
			//if(playerscale.x < shroud_collision_count)
			playerscale2 = temp_scale2;


			GameObject.Find ("Character").transform.localScale =( temp_scale2 );// new Vector3 (1, 1, 1);	
		 

		 //	timepass_to_shrink =1;
			} 
	 
  
		if((temp_scale.x - shroud_collision_count) > 0.5  && (temp_scale.x - shroud_collision_count) < 6.0 )
		{
			temp_scale= temp_scale2;
			Character_mode = Size.stable;
		//// 	timepass_to_shrink =1;


			playerscale2 = temp_scale2;

			Debug.Log ("--------------------------LOW ------------------------------------");

		}

	}


	void grow()
	{
  	

		temp_scale = playerscale;
		
		temp_scale *=  shroud_collision_count;

		GameObject.Find ("Character").transform.localScale = (temp_scale);


		//if ((temp_scale.x - shroud_collision_count) > 0.0 && (temp_scale.x - shroud_collision_count) < 0.5) 
			{

			// LEVEL REACHED
			GameObject.Find ("Character").transform.localScale = (temp_scale);// * shroud_collision_count);
			 
 	
				
			}
			//else
			//GameObject.Find ("Character").transform.localScale = (playerscale);
		if( (temp_scale.x - shroud_collision_count) > 0.1f  && (temp_scale.x - shroud_collision_count) < 0.3f )
			{

		///	timepass_to_shrink =1;
			Character_mode = Size.stable;
			temp_scale2= temp_scale;



			playerscale = temp_scale;

			Debug.Log ("---------------------------- caught ----------------------------------");
			prev_shroud_collision_count = shroud_collision_count;
			
			}

			
			 

	}




	void simulate_size ()
		{

 
 
	 	Debug.Log ("timepass_to_shrink" + timepass_to_shrink.ToString ());
			//Debug.Log ("shroud_collision_count" + shroud_collision_count.ToString ());
	 

		switch (Character_mode) 
		{
			
		case Size.stable:
			Debug.Log ("stabilized = true ");
			stable();
			break;

		case Size.growing:
			Debug.Log("growing to "+temp_scale.x.ToString());
			grow();
			break;

		case Size.shrinking:
			Debug.Log ("shrinking to "+temp_scale2.x.ToString());
			shrink();
			break;

		default:
			 


			break;

		}





		//if(Input.GetMouseButtonDown(1)== true)



		

		 playerpos=GameObject.Find("My_Object2").transform.position;
		 playerpos2 = playerpos;

		
		//playerpos2.x = playerpos.x + 1000 ;
		//playerpos2.y = playerpos.y + 100 ;
		//playerpos2.z = playerpos.z ;

		
		if (playerpos.y < ground_height) 
		{
			
			playerpos.y = ground_height+20;
			//playerpos2.y = ground_height+ 10; // CREATE PAUSE PROBLEM ??
		}
		


		 GameObject.Find("Camera").transform.position = playerpos;


	}









	// Update is called once per frame

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "My_Object1" ||col.gameObject.name == "My_Object1.1" ||col.gameObject.name == "My_Object1.2" && shroud_collision_count <= 3)  // sprout
		{

			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();

		 	Destroy (col.gameObject);


			prev_shroud_collision_count = shroud_collision_count; 
			shroud_collision_count+=1;

			//playerpos.y += 3*playerscale.x;

		//	timepass_to_shrink =0;


			playerpos.y = ground_height;
		//	if(shroud_collision_count>=3) shroud_collision_count=3;

		//	GameObject.Find("Character").transform.position = playerpos;


	//		GameObject.Find("Character").transform.localScale *=  playerscale.x;

		}

		if (col.gameObject.name == "My_Object4")  // pipe
		{

//			GameObject.Find("My_Object4").transform.position = sinker;
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		//	shroud_collision_count = 0;
		 //	sink = true;

		}


		
		



	}

	void setup_direction_and_camera()
	{

		
		//GameObject.Find ("Camera").transform.position = GameObject.Find ("Character").transform.position;
		//GameObject.Find ("Camera").transform.LookAt (GameObject.Find ("Character").transform.position);//new Vector3(-100000,0,0));
		playerpos2.x = playerpos.x;
		playerpos2.y = playerpos.y +100;
	//	playerpos2.z = playerpos.z *1000;



		GameObject.Find ("Camera").transform.position = playerpos2;//GameObject.Find ("Character").transform.position;
 		GameObject.Find ("Follower_Camera").transform.LookAt (GameObject.Find ("My_Object2").transform.position);//new Vector3(-100000,0,0));





		if (Facing_Right == false ) {
			
			this.GetComponent<Rigidbody> ().AddForce (Vector3.right* -5000,ForceMode.Impulse);
			
			GameObject.Find ("My_Object2").transform.LookAt(new Vector3(-100000,0,0));
			
			if (Facing_Right == false && StandingStill == false && once == true) 
			{
				
				this.GetComponent<Rigidbody> ().AddForce (Vector3.right* -5000,ForceMode.Impulse);
				
				//	GameObject.Find("Camera").transform.Rotate(0,180,0);
					GameObject.Find ("My_Object2").transform.LookAt(new Vector3(-100000,0,0));
				once = false;
				//playerpos.z += 40;
				//this.GetComponent<Rigidbody> ().AddForce (-Vector3.right * 100000);
				//	this.GetComponent<Rigidbody> ().AddForce (Vector3.right* -5000,ForceMode.Impulse);
			}
			
			GameObject.Find("Camera").GetComponent<Camera>().enabled = true;
			
			////		GameObject.Find("CameraOpp").GetComponent<Camera>().enabled = true;
			
			
			//	GameObject.Find ("CameraOpp").transform.position = new Vector3 (-2000, 60, 1000);
			//	GameObject.Find("CameraOpp").transform.Rotate(0,0,-180);
			//	GameObject.Find ("CameraOpp").transform.LookAt(GameObject.Find("My_Object2").transform.position);
			
			
			
			StandingStill=true;
		}
		if(Facing_Right== true)
		{
			this.GetComponent<Rigidbody> ().AddForce (Vector3.right* 5000,ForceMode.Impulse);
			GameObject.Find ("My_Object2").transform.LookAt(new Vector3(100000,0,0));
			
			if(Facing_Right== true && StandingStill== false && once ==true )
			{
				
				this.GetComponent<Rigidbody> ().AddForce (Vector3.right* 5000,ForceMode.Impulse);
				//	GameObject.Find("Camera").transform.Rotate(0,-180,0);
				//GameObject.Find("My_Object2").transform.Rotate(0,-180,0);
				GameObject.Find ("My_Object2").transform.LookAt(new Vector3(100000,0,0));
				once = false;
				//				this.GetComponent<Rigidbody> ().AddForce (Vector3.right * 100000);
				
				//	this.GetComponent<Rigidbody> ().AddForce (Vector3.right* 5000,ForceMode.Impulse);
				//	playerpos.z -= 40;
			}
			
			///		GameObject.Find("CameraOpp").GetComponent<Camera>().enabled = false;
			GameObject.Find("Camera").GetComponent<Camera>().enabled = true;
			//	GameObject.Find ("Camera").transform.position = new Vector3 (-2000, 60, 1000);
			//	GameObject.Find("Camera").transform.Rotate(0,0,180);
			//	GameObject.Find ("Camera").transform.LookAt(GameObject.Find("My_Object2").transform.position);
			
			
			
			
			
			StandingStill= true;
			
			
		}
	}


	void accept_input ()
	{
		Vector3 temp;
		temp = GameObject.Find ("Camera").transform.position;
		
		//temp.y += 100;
		
		GameObject.Find ("Camera").transform.position = temp;
		GameObject.Find ("Camera").transform.LookAt (GameObject.Find ("My_Object2").transform.position);
		
		//	GameObject.Find("CameraOpp").transform.LookAt(GameObject.Find("My_Object2").transform.position);
		
		
		
		if (Input.GetKeyDown (KeyCode.W) == true) {

			playerpos2.y =  ground_height+  30;
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 3000, ForceMode.Force);
			
		}
		
		if (Input.GetKeyDown (KeyCode.A) == true) {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.forward * -1000, ForceMode.Force);
			StandingStill = false;
			Facing_Right = false;
			
			playerpos2.x -= 400;
			
			
			//	GameObject.Find ("Camera").transform.position = playerpos2;
			
			once = false;
			
		}
		
		
		if (Input.GetKeyDown (KeyCode.D) == true) {
			this.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 1000, ForceMode.Force);
			StandingStill = false;
			Facing_Right = true;
			
			playerpos2.x += 400;
			
			
			//	GameObject.Find ("Camera").transform.position = playerpos2;
			
			
			once = false;
			
			
		}
		
		
		
		if (Input.GetKeyUp (KeyCode.W) == true) {
			
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 1000, ForceMode.Force);
			GameObject.Find ("convict").GetComponent<Animation>().Play("Take 001");
			
			StandingStill = true;
		}
		
		if (Input.GetKeyUp (KeyCode.A) == true) {
			//	StandingStill= true;
			//	once = true;
		}
		
		
		if (Input.GetKeyUp (KeyCode.D) == true) {
			//	StandingStill= true;
			//	once = true;
		}

		if (Input.GetKeyUp (KeyCode.Escape) == true) {
	

			GameManager.FireStringEvents ("0,ReLoadScene"); // Reload Scene file
			
			GameManager.FireStringEvents ("1"); 
			
			GameManager.FireStringEvents ("2"); 
 

			GameManager.FireStringEvents ("3"); 
			
			GameManager.FireStringEvents ("4"); 

			//GameManager.FireStringEvents ("5"); 


		}
		
		if (Input.GetKeyUp (KeyCode.Space) == true) {
			playerpos.y = ground_height;
			GameManager.FireStringEvents ("3"); 
			resume = !resume;

			if(!resume)
			GameObject.Find ("convict").GetComponent<Animation>().Stop();
			else
			{
				GameObject.Find ("convict").GetComponent<Animation>().Play("Take 001");
			}
		//	GameObject.Find ("My_Object2").transform.position = playerpos;


			shroud_collision_count = 1; 

		


			
		}
	}


	void Update () {
		//GameObject.Find ("Character").transform.position = playerpos;

		//playerpos = GameObject.Find("Character").transform.position;

		playerpos.x = 0; 	
		//playerpos.y = ground_height+20;

		timepass_to_shrink = 0;



		simulate_gravity();
		
		simulate_time();
		simulate_size();

 

		accept_input ();


		
		setup_direction_and_camera();
	

		 
 


	





	}
}
