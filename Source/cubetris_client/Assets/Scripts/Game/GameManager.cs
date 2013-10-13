/*----------------------------------------------*/
/*           	Cubetris						*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/* Manages game flow */
public class GameManager : MonoBehaviour 
{
	enum State
	{
		LOADING,
		PLAYING,
		STATS
	}
	
	
	
	
	#region Members
	
	private GameScreen							m_screen;							// Reference to game screen ( GUI )
	private State								m_state;							// Current state
	private float								m_timer;							// State time
	private float 								m_overallTimer;						// Overall game Timer for Spawn events
	private string								m_currentLevelId;					//
	private float 								m_levelDuration;					// Hardcoded end time
	private const float							LOADING_TIME = 3;
	
	public List< FigureSpawner >				m_figureSpawners;					// Reference to figure spawner
	
	public List< GameObject >					m_targetFigures;					// List of target level figures
	
	//Temporary HardCoded Vars TODO: Remove and change to Generic Vars
	public Queue< FigureSpawnType >				m_figures;			
	public Queue< RocketSpawnType >				m_rockets;
	public Queue< BonusSpawnType >				m_bonuses;
	
	private float								m_complePercntage;					// Target figure percentage - calculated at end of level
	
	
	// Active objects
	// TODO: change to proper solution
	public List< BaseRocket > 					m_activeRockets;
	public List< BlockFigure >					m_activeFigures;
	
	#endregion
	
	
	#region Methods
	
	// Called first (Caching)
	void Awake()
	{
		// Set game screen reference
		m_screen = FindObjectOfType( typeof( GameScreen ) ) as GameScreen;
	}
	
	
	// Called when object with the script attached becomes active
	void OnEnable()
	{
		EventsManager.OnLoadingScreenReady += OnLoadingScreenReady;
	}
	
	
	// Called when object with the script attached become inactive
	void OnDisable()
	{
		EventsManager.OnLoadingScreenReady -= OnLoadingScreenReady;
	}
	
	
	// Use this for initialization
	void Start() 
	{
	
	}
	
	
	// Update is called once per frame
	void Update() 
	{
		m_timer += Time.deltaTime;
		
		switch( m_state )
		{
			case State.LOADING:
				// TODO: if load data is completed
				if ( m_timer >= LOADING_TIME )
				{
					EventsManager.RaiseOnLevelLoaded();
									
					SetState( State.PLAYING );
				}

				break;
			
			case State.PLAYING:
				// Increment timer
				m_overallTimer += Time.deltaTime;
			
				// Decrement duration
				m_levelDuration -= Time.deltaTime;
			
				// Game ends when level duration reaches 0
				if ( m_levelDuration <= 0 )
				{
					SetState( State.STATS );
				}
			
				// Spawn figures
				if ( m_figures.Count > 0 )
				{
					if( m_overallTimer >= m_figures.Peek().time )
					{
						// Spawn figure
						m_figureSpawners[ m_figures.Peek().spawner ].SpawnFigure( m_figures.Peek() );
					
						// Remove from queue
						m_figures.Dequeue();
	
					}
				}
			
				// Spawn rockets
				if ( m_rockets.Count > 0 )
				{
					if( m_overallTimer >= m_rockets.Peek().time )
					{
						// Spawn figure
						m_figureSpawners[ m_rockets.Peek().spawner ].SpawnRocket( m_rockets.Peek() );
					
						// Remove from queue
						m_rockets.Dequeue();
	
					}
				}
				// Spawn bonuses
				break;
			
			case State.STATS:
				break;
		}
	}
	
	
	// Sets new state
	void SetState( State state )
	{
		// Set the requested state
		m_state = state;
		
		// Reset state timer
		m_timer = 0;
		
		switch( m_state )
		{
			case State.LOADING:
				FromXML();
				EventsManager.RaiseOnLevelLoading();
				break;
			
			case State.PLAYING:
				// Nullify timer
				m_overallTimer = 0;
			
				// Allocate active figures lists
				m_activeRockets = new List< BaseRocket >();
				m_activeFigures = new List< BlockFigure >();
				break;
			
			case State.STATS:
				// Calculate stats based on filled figures ( VERY HACKY SOLUTION )
				Object[] figuresSearchResult = FindObjectsOfType( typeof( BlockFigure ) );
				List< Rect > figuresRects = new List< Rect >();
				float overlappingPercentage = 0;
				// ALGORITHM:
				// First: Run over the target figures
				// Second: Get 2D rect for each target figure
				//	1. Check if any block 2D rect collides with the target one
				//	2. For each block from (1) calculate the percentage within target rect
				foreach( GameObject tf in m_targetFigures )
				{
					
					// Collider to rect
					Rect tfRect = new Rect( tf.transform.localPosition.x - tf.transform.localScale.x / 2, tf.transform.localPosition.y - tf.transform.localScale.y / 2
						, tf.transform.localScale.x, tf.transform.localScale.y );
				
					// Iterate over figures	
					foreach( Object f in figuresSearchResult )
					{
						BlockFigure bf = ( f as BlockFigure ); 
						Rect bfRect = new Rect( bf.transform.localPosition.x - bf.transform.localScale.x / 2, bf.transform.localPosition.y - bf.transform.localScale.y / 2
							, bf.transform.localScale.x, tf.transform.localScale.y );
						overlappingPercentage += MathUtils.GetTwoRectanglesOverlapping( tfRect, bfRect );  
					}
				}
			
				// Devide overlapping percentage by number of target figures to find average
				overlappingPercentage /= m_targetFigures.Count;
			
				// Set variable to store overlapping percentage for further usage
				m_complePercntage = overlappingPercentage;
			
				// Notify the game screen to show stats
				m_screen.OnShowStats();
				break;
		}
	}
	
	
	// Starts a new game
	public void StartGame(string levelId)
	{
		m_currentLevelId = levelId;
		SetState( State.LOADING );
	}
	
	
	// Called when loading screen is ready and loading logic should begin
	void OnLoadingScreenReady()
	{
		// Raise Loaded event
		//EventsManager.RaiseOnLevelLoaded();
	}
	
	
	// Loads level data from xml
	// IN - XmlNode node - parent node
	public void FromXML()
	{
		XmlDocument doc = XMLUtils.LoadXML( m_currentLevelId );
		
		// Init queue
		m_figures = new Queue< FigureSpawnType >();
		m_rockets = new Queue< RocketSpawnType >();
		m_bonuses = new Queue< BonusSpawnType >();
		m_targetFigures = new List< GameObject >();
	
		XmlNode node = doc.SelectSingleNode( "level" );
		
		// Overall Game time 
		m_levelDuration = XMLUtils.GetAttributeFloat( node, "duration" ); 
		
		// Parsing target figure
		XmlNode targetFigureNode = node.SelectSingleNode( "goalfigure" );
		XmlNodeList targetFigureNodes = targetFigureNode.SelectNodes( "target_figure" );
		
		foreach( XmlNode targetFigure in targetFigureNodes )
		{
			string type = XMLUtils.GetAttributeString( targetFigure, "type" );
			Vector2 position2D = XMLUtils.GetAttributeVector2( targetFigure, "vector" );
			
			// UGLY HACK, TODO: fix explicit Z value
			Vector3 position3D = new Vector3( position2D.x, position2D.y, 3.2f );
			
			GameObject go = null;
			
			if ( type == "rectangle" )
			{
				go = ResourcePoolManager.Instance.GetObjectForType( "TargetRectangleFigure" );
			}
			else if ( type == "cube" )
			{
				go = ResourcePoolManager.Instance.GetObjectForType( "TargetCubeFigure" );
			}
			
			go.transform.position = position3D;
			
			m_targetFigures.Add( go );
		}
		
		
		// Level Figures from XML
		XmlNode figureParentNode = node.SelectSingleNode( "figures" );
		XmlNodeList figureNodes = figureParentNode.SelectNodes( "figure" );
		List< FigureSpawnType > fromXMLFiguresList = new List< FigureSpawnType >();
		foreach( XmlNode figureNode in figureNodes ) 
		{
			int id = XMLUtils.GetAttributeInt( figureNode, "id" );
			string type = XMLUtils.GetAttributeString( figureNode, "type" );
			float speed = XMLUtils.GetAttributeFloat( figureNode, "speed" );
			float time = XMLUtils.GetAttributeFloat( figureNode, "time" );
			int spawnerId = XMLUtils.GetAttributeInt( figureNode, "spawner_id" );
			int colorId = XMLUtils.GetAttributeInt( figureNode, "color_id" );
			fromXMLFiguresList.Add (new FigureSpawnType( id, type, time , spawnerId, speed, colorId ) );
		}
		
		// Sorting list and inserting into Queue by time from big ->
		while( fromXMLFiguresList.Count > 0 )
		{
			FigureSpawnType smallest = fromXMLFiguresList[ 0 ];
			for ( int i = 1; i < fromXMLFiguresList.Count; i++)
			{
				if( fromXMLFiguresList[ i ].time < smallest.time )
				{
					smallest = fromXMLFiguresList[ i ];
				}
			}
			m_figures.Enqueue( smallest );
			fromXMLFiguresList.Remove( smallest );
		} 
		
		// Level Rockets from XML
		XmlNode rocketsParentNode = node.SelectSingleNode( "rockets" );
		XmlNodeList rocketsNodes = rocketsParentNode.SelectNodes( "rocket" );
		List < RocketSpawnType > fromXMLRocketsList = new List< RocketSpawnType >();
		foreach( XmlNode rocketNode in rocketsNodes )
		{
			int id = XMLUtils.GetAttributeInt( rocketNode, "id" );
			string type = XMLUtils.GetAttributeString( rocketNode, "type" );
			float speed = XMLUtils.GetAttributeFloat( rocketNode, "speed" );
			float time = XMLUtils.GetAttributeFloat( rocketNode, "time" );
			int spawnerId = XMLUtils.GetAttributeInt( rocketNode, "spawner_id" );
			fromXMLRocketsList.Add(new RocketSpawnType( id, type, speed, time , spawnerId ) );
		}
		//Sorting list and inserting into Queue by time from big ->
		while( fromXMLRocketsList.Count > 0 )
		{
			RocketSpawnType smallest = fromXMLRocketsList[ 0 ];
			for ( int i = 1; i < fromXMLRocketsList.Count; i++ )
			{
				if( fromXMLRocketsList[ i ].time < smallest.time )
				{
					smallest = fromXMLRocketsList[ i ];
				}
			}
			m_rockets.Enqueue( smallest );
			fromXMLRocketsList.Remove( smallest );
		}
		
		// Level Bonuses from XML
		XmlNode bonusesParentNode = node.SelectSingleNode( "bonuses" );
		XmlNodeList bonusesNodes = bonusesParentNode.SelectNodes( "bonus");
		List < BonusSpawnType > fromXMLBonusesList = new List< BonusSpawnType >();
		foreach( XmlNode bonusNode in bonusesNodes )
		{
			string type = XMLUtils.GetAttributeString( bonusNode, "type" );
			float speed = XMLUtils.GetAttributeFloat( bonusNode, "speed" );
			float time = XMLUtils.GetAttributeFloat( bonusNode, "time" );
			int spawnerId = XMLUtils.GetAttributeInt( bonusNode, "spawner_id" );
			fromXMLBonusesList.Add( new BonusSpawnType( type, speed, time , spawnerId ) );
		}
		//Sorting list and inserting into Queue by time from big ->
		while( fromXMLBonusesList.Count > 0 )
		{
			BonusSpawnType smallest = fromXMLBonusesList[ 0 ];
			for ( int i = 1; i < fromXMLBonusesList.Count; i++ )
			{
				if( fromXMLBonusesList[ i ].time < smallest.time )
				{
					smallest = fromXMLBonusesList[ i ];
				}
			}
			m_bonuses.Enqueue( smallest );
			fromXMLBonusesList.Remove( smallest );
		}
	}

	
	// Destroys a rocket with gived id
	public void DestroyRocket( int id )
	{
		foreach( BaseRocket br in m_activeRockets )
		{
			if ( br != null && br.m_id == id )
			{
				br.Destroy();
			}
		}
	}
	
	
	// Moves block to new position
	public void MovedBlock( int id, string strVector )
	{
		foreach( BlockFigure bf in m_activeFigures )
		{
			if ( bf != null && bf.m_id == id )
			{
				// Parse the vector
				string[] seperateStrVector = strVector.Split( ',' );
				Vector3 newPos = new Vector3( float.Parse( seperateStrVector[ 0 ] ), float.Parse( seperateStrVector[ 1 ] ), float.Parse( seperateStrVector[ 2 ] ) );
				bf.transform.position = newPos;
			}
		}
	}
	
	
	#endregion
	
	
	#region Getters/Setters

	public float LevelDuration 
	{
		get 
		{
			return this.m_levelDuration;
		}

	}

	

	public float complePercntage 
	{
		get {
			return this.m_complePercntage;
		}
	}
	#endregion
}
