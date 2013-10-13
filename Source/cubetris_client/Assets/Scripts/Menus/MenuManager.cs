/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Manages the menus in game
 * using Singleton pattern */
public class MenuManager : MonoBehaviour 
{
	#region Members
	
	private static MenuManager 					m_instance;							// Reference to self instance
	
	private List< MenuScreen >					m_activeMenus;						// Active menus
	private Dictionary< string, MenuScreen >	m_availableMenus;					// All the menus
	
	public string								m_firstScreen;						// Name of the first screen to display when application starts
	
	
	// Static getter
	public static MenuManager Instance
    {
        get
        {
            return m_instance;
        }
    }
	
	#endregion
	
	#region Methods
	
	// Called first
	void Awake()
	{
		// Init data structures
		m_activeMenus = new List< MenuScreen >();
		m_availableMenus = new Dictionary< string, MenuScreen >();
		
		// Find child menus
		foreach ( Transform child in transform )
		{
			// Retrieve relevant data from child
			string name = child.gameObject.name;
			MenuScreen menu = child.GetComponent( typeof( MenuScreen ) ) as MenuScreen;
			
			// Hide the screen
			menu.Hide();
			
			// Add the menu to menus dictionary
			m_availableMenus.Add( name, menu );
		}
		
		// Set instance
		m_instance = this;
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
		// Show first screen
		ShowScreen( m_firstScreen );
	}
	
	
	// Update is called once per frame
	void Update() 
	{
	
	}
	
	
	// Show some screen
	// IN - string name - required screen name
	// IN - string[] args - arguments to pass to screen
	public void ShowScreen( string name, string[] args = null )
	{
		// Check if screen exists
		if ( m_availableMenus.ContainsKey( name ) )
		{
			// Check if screen already exists in active menus list
			// in case it is, add it to end of the list
			if ( m_activeMenus.Contains( m_availableMenus[ name ] ) )
			{
				m_activeMenus.Remove( m_availableMenus[ name ] );
			}
			
			// Add the menu to last place in the list ( STACK like )
			m_activeMenus.Add( m_availableMenus[ name ] );
		}
		
		// Show the required screen and hide the previous one
		for ( int i = 0; i < m_activeMenus.Count; i++ )
		{
			if ( i == m_activeMenus.Count - 2 )
			{
				// Previous screen
				m_activeMenus[ i ].Hide();
			}
			else if ( i == m_activeMenus.Count - 1 )
			{
				// New screen
				m_activeMenus[ i ].Show( args );
			}
		}
		
		// Raise event
		EventsManager.RaiseOnShowScreen( name );
	}
	
	
	// Return back to previous screen
	// IN - string[] args - arguments to pass to screen
	public void Back( string[] args = null )
	{
		if ( m_activeMenus.Count > 1 )
		{
			// Remove current screen and show the previous one
			// Hide
			m_activeMenus[ m_activeMenus.Count - 1 ].Hide();
			
			// Remove from stack
			m_activeMenus.RemoveAt( m_activeMenus.Count - 1 );
			
			// Show
			m_activeMenus[ m_activeMenus.Count - 1 ].Show();
		}
	}
	
	
	// Clears the back navigation stack
	public void ResetNavigation()
	{
		// Clear all screens besides the current one
		for ( int i = 0; i < m_activeMenus.Count - 1; i++ )
		{
			// Hide
			m_activeMenus[ i ].Hide();
			
			// Remove
			m_activeMenus.RemoveAt( i );
		}
	}
	
	#endregion
}
