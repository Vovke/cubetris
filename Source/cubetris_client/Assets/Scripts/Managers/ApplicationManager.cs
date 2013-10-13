/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Manages the application general states
 * using Singleton pattern */
public class ApplicationManager : MonoBehaviour 
{
	#region Members
	
	private const string								APP_VERSION = "0.0.2";			// X.Y.Z - X = Major, Y = Minor, Z = Build		
	
	private static ApplicationManager 					m_instance;						// Reference to self instance
	public GameObject									m_gameObjects;					// Parent of game required objects
	public PlayerIOConnector 							m_connection;
	public const bool									DEBUG_MODE = false;
	
	#endregion
	
	
	#region Methods
	
	// Static getter
	public static ApplicationManager Instance
    {
        get
        {
            return m_instance;
        }
    }
	
	
	// Called first
	void Awake()
	{
		// Set instance
		m_instance = this;
		
		Application.targetFrameRate = 60;
	}
	
	
	// Called when object with the script attached becomes active
	void OnEnable()
	{
		EventsManager.OnShowScreen += OnShowScreen;
	}
	
	
	// Called when object with the script attached become inactive
	void OnDisable()
	{
		EventsManager.OnShowScreen -= OnShowScreen;
	}
	
	
	// Use this for initialization
	void Start() 
	{
		if ( m_connection == null )
		{
			m_connection = ( FindObjectOfType( typeof( PlayerIOConnector ) ) as PlayerIOConnector );
		}
	}
	
	
	// Update is called once per frame
	void Update() 
	{

	}
	
	
	/* Events */
	
	// Show game screen logic
	public void MoveToGame(string levelId)
	{
		// Set game hud
		MenuManager.Instance.ShowScreen( "GameScreen" );
		
		// Enable components required for game
		m_gameObjects.SetActiveRecursively( true );
		
		// Start new game
		( m_gameObjects.GetComponent( typeof( GameManager ) ) as GameManager ).StartGame( levelId ); 
	}
	
	
	// Disable game components
	public void MoveToMenus()
	{
		
	}
	
	
	// Called when screen is shown
	public void OnShowScreen ( string name )
	{
		Debug.Log( "Enter screen: " + name );
	}
	
	#endregion
}
