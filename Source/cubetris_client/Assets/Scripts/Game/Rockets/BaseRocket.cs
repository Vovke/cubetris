/*----------------------------------------------*/
/*           	Slice A Cig 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Abstract interface for rockets to inherit from */
public class BaseRocket : MonoBehaviour 
{
	#region Members
	
	public int					m_id;
	public PlayerIOConnector	m_connection;
	
	
	#endregion


	#region Methods
	
	// Called first
	protected virtual void Awake()
	{
		
	}
	
	
	// Called when object with the script attached becomes active
	protected virtual void OnEnable()
	{
		
	}
	
	
	// Called when object with the script attached become inactive
	protected virtual void OnDisable()
	{
		
	}
	
	
	// Use this for initialization
	protected virtual void Start() 
	{
		m_connection = ( FindObjectOfType( typeof( PlayerIOConnector ) ) as PlayerIOConnector );
	}
	
	
	// Update is called once per frame
	protected virtual void Update() 
	{
	
	}
	
	
	// Activates the rocket
	public virtual void Activate( float force, Vector3 direction ) 
	{
	
	}
	
	
	// Destroys a rocket
	public virtual void Destroy()
	{
		if ( GameProperties.role != Role.BOTH )
		{
			// Notify second player
			if ( m_connection != null )
			{
				m_connection.SendDestroyMissileCommand( m_id );
			}
		}
	}
	
	
	#endregion
	
	
	#region Events
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
