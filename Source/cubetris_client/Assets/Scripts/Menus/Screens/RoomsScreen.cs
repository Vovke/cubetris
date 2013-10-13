/*----------------------------------------------*/
/*           	Slice A Cig 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using PlayerIOClient;

public class RoomsScreen : MenuScreen 
{
	#region Members
	
	
	
	
	#endregion


	#region Methods
	
	// Called first
	void Awake()
	{
		base.Awake();
	}
	
	
	// Called when object with the script attached becomes active
	void OnEnable()
	{
		
	}
	
	
	// Called when object with the script attached become inactive
	void OnDisable()
	{
		
	}
	
	
	// Use this for initialization
	void Start() 
	{

	}
	
	
	// Update is called once per frame
	void Update() 
	{
	
	}
	
	
	public override void Show( string[] args = null )
	{
		base.Show( args );
		
		// Retrieve available rooms
		ApplicationManager.Instance.m_connection.RetrieveRooms();
	}
	
	
	// Handles button click
	// IN - GameObject button - reference to pressed button
	public override void ClickHandler( GameObject button )
	{
		Debug.Log( button.name );
		
		switch ( button.name )
		{
			case "CreateSimpleButton":
				ApplicationManager.Instance.m_connection.CreateRoom();
				break;
		}
	}
	
	#endregion
	
	
	#region Events
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
