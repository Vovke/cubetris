/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


// Splash screen for displaying logo and company information
public class SplashScreen : MenuScreen
{
	#region Members
	
	public const float							m_showTimer = 2;						// Total time to display the splash
	private float								m_currentShowTime = 0;					// Active display time
	
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
		if ( m_currentShowTime >= m_showTimer )
		{
			// Show time have passed, move to next screen
			MenuManager.Instance.ShowScreen( "MainMenuScreen" );
		}
		else
		{
			m_currentShowTime += Time.deltaTime;
		}
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
