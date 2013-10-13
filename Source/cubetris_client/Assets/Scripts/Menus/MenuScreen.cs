/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Base menu screen - menus should inherit from it */
public class MenuScreen : MonoBehaviour 
{
	
	#region Members
	
	private UIPanel								m_panel;							// Reference to display panel (NGUI)
	
	#endregion
	
	
	#region Methods
	
	// Called first
	public virtual void Awake()
	{
		// Find the display panel
		m_panel = transform.GetComponent( typeof( UIPanel ) ) as UIPanel;
		
		// Cache all the buttons and attach message to them in order to use Screen ClickHandler
		List< MonoBehaviour > buttons = new List< MonoBehaviour >();    
 		buttons.AddRange( GetComponentsInChildren< UIButton >() );
   		buttons.AddRange( GetComponentsInChildren< UIImageButton >() );
  		foreach ( MonoBehaviour button in buttons )
  		{
   			UIButtonMessage message = button.gameObject.GetComponent< UIButtonMessage >();
    		if ( message == null )
   			{
    			message = button.gameObject.AddComponent< UIButtonMessage >();
   			}
   
   			message.target = gameObject;
   			message.trigger = UIButtonMessage.Trigger.OnClick;
   			message.functionName = "ClickHandler";
   		}
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
	public virtual void Show( string[] args = null )
	{
		// Enable all objects
		transform.gameObject.SetActiveRecursively( true );
		
		// Refresh panel display
		m_panel.Refresh();
	}
	
	
	// Hides the screen
	public virtual void Hide()
	{
		// Disable all objects
		transform.gameObject.SetActiveRecursively( false );
	}
	
	
	// Handles button click
	// IN - GameObject button - reference to pressed button
	public virtual void ClickHandler( GameObject button )
	{
		
	}
	
	
	#endregion
}
