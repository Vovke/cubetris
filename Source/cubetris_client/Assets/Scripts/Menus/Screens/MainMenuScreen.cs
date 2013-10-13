/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Main menu */
public class MainMenuScreen : MenuScreen
{
	#region Members
	
	#endregion
	
	
	#region Methods
	
	// Called first
	public override void Awake()
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
	
	
	// Show the menu
	// IN - string[] args - arguments for screen logic
	public override void Show( string[] args = null )
	{
		base.Show( args );
		
		Debug.Log( "Screen:" + transform.gameObject.name );
	}
	
	
	// Handles button click
	// IN - GameObject button - reference to pressed button
	public override void ClickHandler( GameObject button )
	{
		Debug.Log( button.name );
		
		switch ( button.name )
		{
			case "SinglePlayerButton":
				ApplicationManager.Instance.MoveToGame( "level1" );
				break;
			
			case "MultiPlayerButton":
				( FindObjectOfType( typeof( PlayerIOConnector ) ) as PlayerIOConnector ).Init();
				break;
			
			case "OptionsButton":
				break;
			
			case "LeaderboardsButton":
				break;
			
			case "ooo":
				break;
			
		}
	}
	
	#endregion
}
