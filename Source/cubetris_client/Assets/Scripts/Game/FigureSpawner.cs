/*----------------------------------------------*/
/*           	Slice A Cig 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;

public class FigureSpawner : MonoBehaviour 
{
	#region Members
	public Transform				m_rocketTargetLoacator;				// Default rocket target locator to aim on
	
	private Color[]                 m_availableColors = { Color.red, Color.green, Color.blue, Color.yellow };
	
	public GameManager				m_gameManager;						// Reference to game manager
	
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
	
	}
	
	
	// Update is called once per frame
	void Update() 
	{
	
	}
	
	
	// Spawns a new figure
	public void SpawnFigure( FigureSpawnType figure )
	{
		string prefabName = "";
		switch( figure.type )
		{
			case FigureSpawnType.FigureType.CUBE:
				prefabName = "CubeFigure";
				break;
			
			case FigureSpawnType.FigureType.RECTANGLE:
				prefabName = "RectangleFigure";
				break;
			
			case FigureSpawnType.FigureType.TRIANGLE:
				prefabName = "TriangleFigure";
				break;
		}
		
		GameObject go = ResourcePoolManager.Instance.GetObjectForType( prefabName );
		go.transform.localPosition = transform.localPosition;
		
		BlockFigure script = go.GetComponent< BlockFigure >();
		
		// Set id
		script.m_id = figure.id;
		
		go.renderer.material.color = m_availableColors[ Random.Range( 0, m_availableColors.Length ) ];
		script.m_selectedColor = go.renderer.material.color;
		
		if( GameProperties.role == Role.BOTH )
		{
			go.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
		}
		
		// Remove physics from figure for defender
		if ( GameProperties.role == Role.DEFENDER )
		{
			//Destroy( go.rigidbody );
			//Destroy( go.collider );
		}
		
		// Add drag component if user is using touch controls
		if ( GameProperties.selectedControls == ControlsType.TOUCH )
		{
			go.AddComponent< TouchDrag >();
		}
		
		
		// Add the figure to active figures list
		m_gameManager.m_activeFigures.Add( script );
	}
	
	
	// Spawns a new rocket
	public void SpawnRocket( RocketSpawnType rocket )
	{
		string prefabName = "";
		switch( rocket.type )
		{
			case RocketSpawnType.RocketType.EXPLOSIVE:
				prefabName = "RocketExplosive";
				break;
			
			case RocketSpawnType.RocketType.NON_EXPLOSIVE:
				prefabName = "RocketNonExplosive";
				break;
		}
		
		// Get rocket object
		GameObject go = ResourcePoolManager.Instance.GetObjectForType( prefabName );
		
		// Set position and rotation
		go.transform.localPosition = transform.localPosition;
		go.transform.LookAt( m_rocketTargetLoacator );
		
		// Activate the rocket
		BaseRocket script = go.GetComponent< BaseRocket >();
				
		// Set id
		script.m_id = rocket.id;
		
		script.Activate( rocket.speed, -( go.transform.position - m_rocketTargetLoacator.position ).normalized );
		
		m_gameManager.m_activeRockets.Add( script );

	}
	
	
	// Spawns a new bonus
	public void SpawnBonus( BonusSpawnType bonus )
	{
	}
	
	
	#endregion
	
	
	#region Events
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
