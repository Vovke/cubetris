/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


public class LoadingArea
{
	public Transform							parent;
	public UISprite 							background;
	public UILabel 								loadingLabel;
}


// Stats area components
public class StatsArea
{
	public Transform 							parent;
	public UILabel								rankLabel;
	public UILabel								scoreLabel;
}



/* Game HUD */
public class GameScreen : MenuScreen
{	
	// Screen state
	public enum State
	{
		LOADING,
		PLAYING,
		STATS,
		TRANSITION_ON,
		TRANSITION_OFF
	}
	
	
	#region Members
	
	private State 								m_state;					// Screen state
	
	// Animation timers
	private const float							TRANSITION_ON_TIMER = 1f;
	private const float							TRANSITION_OFF_TIMER = 1f;
	private float								m_animationTimer = 0;
	
	// Cache NGUI components
	private LoadingArea							m_loadingArea;				// Loading parent
	private StatsArea							m_statsArea;					// Stats area object
	
	// Loading event flag
	private bool								m_loading;
	
	// Reference to game manager
	public GameManager							m_gameManager;
	private UILabel								m_gameTime;
	
	#endregion
	
	
	#region Methods
	
	// Called first
	public override void Awake()
	{
		base.Awake();
		
		// Cache dynamic components
		
		// Loading area
		m_loadingArea = new LoadingArea();
		m_loadingArea.parent = UnityHelpers.FindChildDeep( transform, "Loading" );
		m_loadingArea.background = m_loadingArea.parent.FindChild( "Background" ).GetComponent( typeof( UISprite ) ) as UISprite;
		m_loadingArea.loadingLabel = m_loadingArea.parent.FindChild( "LoadingLabel" ).GetComponent( typeof( UILabel ) ) as UILabel;
		
		// Stats area
		m_statsArea = new StatsArea();
		m_statsArea.parent = UnityHelpers.FindChildDeep( transform, "Stats" );
		m_statsArea.rankLabel = m_statsArea.parent.FindChild( "RankLabel" ).GetComponent( typeof( UILabel) ) as UILabel;
		m_statsArea.scoreLabel = m_statsArea.parent.FindChild( "ScoreLabel" ).GetComponent( typeof( UILabel) ) as UILabel; 
		
		m_gameTime = UnityHelpers.FindChildDeep( transform, "TimeLabel" ).GetComponent< UILabel >();
		
	}
	
	// Called when object with the script attached becomes active
	void OnEnable()
	{
		EventsManager.OnLevelLoading += OnLevelLoading;
		EventsManager.OnLevelLoaded += OnLevelLoaded;
	}
	
	
	// Called when object with the script attached become inactive
	void OnDisable()
	{
		EventsManager.OnLevelLoading -= OnLevelLoading;
		EventsManager.OnLevelLoaded -= OnLevelLoaded;
	}
	
	
	// Use this for initialization
	void Start() 
	{
	
	}
	
	
	// Update is called once per frame
	void Update() 
	{
		UpdateState();
	}
	
	
	// Show the menu
	// IN - string[] args - arguments for screen logic
	public override void Show( string[] args = null )
	{
		base.Show( args );
		
		// Hide loading 
		m_loadingArea.parent.gameObject.SetActiveRecursively( false );
		
		Debug.Log( "Screen:" + transform.gameObject.name );
	}
	
	
	// Set new game state ( Logic before entering state )
	public void SetState( State state )
	{
		// Set state to required one
		m_state = state;
		
		switch( m_state )
		{
			case State.LOADING:
				m_loading = false;
				m_loadingArea.parent.gameObject.SetActiveRecursively( true );
			
				// Hide the stats aread
				m_statsArea.parent.gameObject.SetActiveRecursively( false );
				break;
			
			case State.PLAYING:

				break;
			
			case State.STATS:
				// Determinate player score and rank
				// TODO: implement complete scoring solution, ATM it uses hardcoded pre-definitions
				m_statsArea.scoreLabel.text = ( ( int )( m_gameManager.complePercntage * 10000 / 0.7 ) ).ToString() + "POINTS";
			
				if( m_gameManager.complePercntage < 0.05f )
				{
					m_statsArea.rankLabel.text = "NOOBZOR!";
				}
				else if( m_gameManager.complePercntage < 0.3f )
				{
					m_statsArea.rankLabel.text = "ROOKIE!";
				}
				else if( m_gameManager.complePercntage < 0.55f )
				{
					m_statsArea.rankLabel.text = "MASTER!";
				}
				else if( m_gameManager.complePercntage < 1f )
				{
					m_statsArea.rankLabel.text = "CHUCK NORRIS!";
				}
			
				// Show stats area
				m_statsArea.parent.gameObject.SetActiveRecursively( true );
				break;
			
			case State.TRANSITION_ON:
				m_animationTimer = 0;
				break;
			
			case State.TRANSITION_OFF:
				// Hide the stats aread
				m_statsArea.parent.gameObject.SetActiveRecursively( false );
			
				m_loadingArea.parent.gameObject.SetActiveRecursively( true );
				m_animationTimer = 0;
				break;
		}
	}
	
	
	// Update active game state
	public void UpdateState()
	{
		switch( m_state )
		{
			case State.LOADING:
				UpdateStateLoading();
				break;
			
			case State.PLAYING:
				UpdateStatePlaying();
				break;
			
			case State.STATS:
				break;
			
			case State.TRANSITION_ON:
				UpdateStateTransitionOn();
				break;
			
			case State.TRANSITION_OFF:
				UpdateStateTransitionOff();
				break;
		}
	}
	
	
	// Update during loading state
	void UpdateStateLoading()
	{
		if ( !m_loading )
		{
			// Raise loading screen ready event
			//EventsManager.RaiseOnLoadingScreenReady();
			m_loading = true;
		}
	}
	
	
	// Update during Transition On state
	void UpdateStateTransitionOn()
	{
		m_animationTimer += Time.deltaTime;
		
		if ( m_animationTimer >= TRANSITION_ON_TIMER )
		{
			// Done transition
			m_loadingArea.parent.gameObject.SetActiveRecursively( false );
			SetState( State.PLAYING );
		}
		else
		{
			// Apply transition animation
			m_loadingArea.background.alpha = 1 - ( 1 / TRANSITION_ON_TIMER ) * m_animationTimer; 
			m_loadingArea.loadingLabel.alpha = 1 - ( 1 / TRANSITION_ON_TIMER ) * m_animationTimer; 
		}
	}
	
	
	// Update during Transition Off state
	void UpdateStateTransitionOff()
	{
		m_animationTimer += Time.deltaTime;
		
		if ( m_animationTimer >= TRANSITION_ON_TIMER )
		{
			// Done transition
			SetState( State.LOADING );
		}
		else
		{
			// Apply transition animation
			m_loadingArea.background.alpha = ( 1 / TRANSITION_ON_TIMER ) * m_animationTimer; 
			m_loadingArea.loadingLabel.alpha = ( 1 / TRANSITION_ON_TIMER ) * m_animationTimer; 
		}
	}
	
	
	// Update during Playing state
	void UpdateStatePlaying()
	{
		// Update time
		m_gameTime.text = m_gameManager.LevelDuration.ToString();
	}
	
	
	// Called when level starts loading
	void OnLevelLoading()
	{
		SetState( State.LOADING );
	}
	
	
	// Called when level is loaded
	void OnLevelLoaded()
	{
		SetState( State.TRANSITION_ON );
	}
	
	
	// Public method to show stats
	public void OnShowStats()
	{
		SetState( State.STATS );
	}
	
	
	// Public method to over the game
	public void OnGameOver()
	{
		SetState( State.TRANSITION_OFF );
	}
	
	#endregion
	
}
