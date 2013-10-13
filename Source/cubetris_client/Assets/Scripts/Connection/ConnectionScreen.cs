/*----------------------------------------------*/
/*           	Slice A Cig 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


// Screen to display during async connection requests
public class ConnectionScreen : MonoBehaviour 
{
	private enum State
	{
		HIDDEN,				// Screen hidden
		CONNECTING,			// Performing sync connection
		MESSAGE				// Message after response
	}
	
	#region Members
	
	private State			m_state;						// Current screen state
	private float			m_timer;						// State timer
	
	private bool			m_showMessage;					// Flag which indicates if message should be displayed
	private bool			m_showErrorOnly;				// Message is displayed only after an error response
	private bool			m_receivedError;				// True upon receiving an error
	private bool			m_exitUponError;				// If true the app will exis after user closes the message
	
	private UILabel			m_connectionMessage;			// Message to display during connection state
	private UILabel			m_finishTitle;					// Title of finish message
	private UILabel			m_finishDescription;			// Description of finish message
	private UILabel			m_finishButtonLabel;			// Finish message button text
	private UISprite		m_background;					// Background 
	private UISprite		m_connectionImage;				// Animating image to display during connection
	private UIButton		m_finishButton;				
	
	
	#endregion


	#region Methods
	
	// Called first
	private void Awake()
	{
		// Cache dynamic visual components for further usage
		m_connectionMessage = UnityHelpers.FindChildDeep( transform, "ConnectingMessage" ).GetComponent< UILabel >();
		m_finishTitle = UnityHelpers.FindChildDeep( transform, "FinishTitle" ).GetComponent< UILabel >();
		m_finishDescription = UnityHelpers.FindChildDeep( transform, "FinishDescription" ).GetComponent< UILabel >();
		m_finishButtonLabel = UnityHelpers.FindChildDeep( transform, "FinishButtonLabel" ).GetComponent< UILabel >();
		m_background = UnityHelpers.FindChildDeep( transform, "Background" ).GetComponent< UISprite >();
		m_connectionImage = UnityHelpers.FindChildDeep( transform, "LoadingIndicator" ).GetComponent< UISprite >();
		m_finishButton = UnityHelpers.FindChildDeep( transform, "FinishButton" ).GetComponent< UIButton >();
	}
	
	
	// Called when object with the script attached becomes active
	void OnEnable()
	{
		
	}
	
	
	// Called when object with the script attached become inactive
	private void OnDisable()
	{
		
	}
	
	
	// Use this for initialization
	private void Start() 
	{
		SetState( State.HIDDEN );
	}
	
	
	// Update is called once per frame
	private void Update() 
	{
		// Update screen according to screen state
		switch( m_state )
		{
			case State.HIDDEN:
				break;
			
			case State.CONNECTING:
				// Connecting animation
				m_connectionImage.transform.Rotate( new Vector3( 0, 0, 1 ) );
				break;
			
			case State.MESSAGE:
				break;
		}
	}
	
	
	// Set new state
	private void SetState( State state )
	{
		// Reset timer and set new state
		m_state = state;
		m_timer = 0;
		
		// New state logic
		switch( m_state )
		{
			case State.HIDDEN:
				// Hide everything
				NGUITools.SetActive( m_connectionMessage.gameObject, false );
				NGUITools.SetActive( m_finishTitle.gameObject, false );
				NGUITools.SetActive( m_finishDescription.gameObject, false );
				NGUITools.SetActive( m_finishButton.gameObject, false );
				NGUITools.SetActive( m_background.gameObject, false );
				NGUITools.SetActive( m_connectionImage.gameObject, false );
				break;
			
			case State.CONNECTING:
				// Show connection state visuals
				NGUITools.SetActive( m_connectionMessage.gameObject, true );
				NGUITools.SetActive( m_finishTitle.gameObject, false );
				NGUITools.SetActive( m_finishDescription.gameObject,false );
				NGUITools.SetActive( m_finishButton.gameObject, false);
				NGUITools.SetActive( m_background.gameObject, true);
				NGUITools.SetActive( m_connectionImage.gameObject, true );
				break;
			
			case State.MESSAGE:
				if ( m_showMessage && m_receivedError )
				{
					// Show message state visuals and hide others
					NGUITools.SetActive( m_connectionMessage.gameObject, false );
					NGUITools.SetActive( m_finishTitle.gameObject, true );
					NGUITools.SetActive( m_finishDescription.gameObject,true );
					NGUITools.SetActive( m_finishButton.gameObject, true );
					NGUITools.SetActive( m_background.gameObject, true);
					NGUITools.SetActive( m_connectionImage.gameObject, false );
				}
				else
				{
					SetState( State.HIDDEN );
				}
				break;
		}
	}
	
	
	// Click handler
	// IN - GameObject button - reference to pressed button
	private void OnButtonClick( GameObject button )
	{
		if ( m_exitUponError )
		{
			Application.Quit();
			return;
		}
		
		// Hide visuals
		SetState( State.HIDDEN );
	}
	
	
	// Public interface to show screen
	// ALL parameters are optional
	public void Show( string connectionMessage = "Connecting", bool showErrorOnly = false, bool showMessage = false, string finishTitle = "", string finishMessage = "",
		string finishButton = "OK" )
	{
		m_connectionMessage.text = connectionMessage;
		m_showErrorOnly = showErrorOnly;
		m_showMessage = showMessage;
		m_finishTitle.text = finishTitle;
		m_finishDescription.text = finishMessage;
		m_finishButtonLabel.text = finishButton;
		
		SetState( State.CONNECTING );
	}
	
	
	// Public interface to hide the screen
	// IN - bool error - true if error response
	// IN - bool exit - true if app should exit ( critical errors )
	public void Hide( bool error = false, bool exit = false )
	{
		m_receivedError = error;
		m_exitUponError = exit;
		
		SetState( State.MESSAGE );
	}
	
	
	#endregion
	
	
	#region Events
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
