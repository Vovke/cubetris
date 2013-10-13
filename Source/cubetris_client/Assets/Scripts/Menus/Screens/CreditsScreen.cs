/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;

/* Credits screen */
public class CreditsScreen : MenuScreen
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
	
	#endregion
}
