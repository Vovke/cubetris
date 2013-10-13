/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Game Controls Manager controls */
public class ControlsManager : MonoBehaviour 
{
	#region Members	
	
	public TouchControls		m_touch;			// Reference to touch controls
	public GameObject			m_kinect;			// Reference to Kinect controls
	
	#endregion


	#region Methods
	
	// Called first
	void Awake()
	{
		
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
		// Init controls
		// Currently kinect works only for defender
		if ( GameProperties.selectedControls == ControlsType.KINECT && GameProperties.role == Role.DEFENDER )
		{
			m_kinect.SetActiveRecursively( true );
		}
		else
		{
			Destroy( m_kinect.gameObject );
		}
	}
	
	
	// Update is called once per frame
	void Update() 
	{

	
	}
	
	#endregion
}
