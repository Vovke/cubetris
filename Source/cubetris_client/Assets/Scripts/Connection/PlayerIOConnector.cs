/*----------------------------------------------*/
/*           	CubeTris	 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;

// Responsible for connection to server
public class PlayerIOConnector : MonoBehaviour 
{
	#region Members
	
	private bool			m_gameStarted = false;		// Flag raised when game is started
	private Connection 		m_connection;				// Reference to Player IO Connenction instance
	private Client			m_client;					// Reference to Player IO Client instance
	private RoomInfo[]		m_rooms;					// Array of available rooms
	private float			m_updateTimer;				// Update requests timer
	private float			UPDATE_FREQUENCY = 100;		// The frequence in which the requests are sent
	
	public ConnectionScreen m_connectionVisual;			// Reference to visual connection class
	
	private bool			m_developmentServer = true;// Go to development server
	//private string			m_developmentServerIP = "172.20.10.2";
	private string			m_developmentServerIP = "127.0.0.1";
	
	public GameManager		m_gameManager;				// Reference to game manager
	
	
	
	#endregion


	#region Methods
	
	// Called first
	private void Awake()
	{
		
	}
	
	
	// Called when object with the script attached becomes active
	private void OnEnable()
	{
		
	}
	
	
	// Called when object with the script attached become inactive
	private void OnDisable()
	{
		
	}
	
	
	// Use this for initialization
	private void Start() 
	{

	}
	
	
	// Update is called once per frame
	private void Update() 
	{
		if ( m_gameStarted )
		{
			// Increment timer
			m_updateTimer += Time.deltaTime;
			
			if ( m_updateTimer >= UPDATE_FREQUENCY )
			{
				// Send update to server
				//m_connection.Send( Message.Create( 
			}
		}
	}
	
	
	// Init connection - sync
	public void Init()
	{
		// Required to setup the Player.IO Client in Unity3D
		PlayerIOClient.PlayerIO.UnityInit( this );
		
		// Init connection class
		PlayerIOClient.PlayerIO.Connect( "cubetris-9goxdtgbcumv0txnmo2ag", "public" , "test" + Random.Range( 0, 1000 ).ToString(), null, null,
			InitSuccessResponse, InitFailResponse );
		
		// Show visual
		m_connectionVisual.Show( "Initializing server connecting", true, true, "Error connecting", "Please fix your connection and run the APP again", "EXIT" );
	}
	
	
	// Connect to room - sync
	// IN - roomName - how to name the room , if not set will create a random name
	public void RetrieveRooms()
	{
		m_client.Multiplayer.ListRooms( "CubeTris", null, 10, 0, FoundRooms, ErrorFindingRooms );
		m_connectionVisual.Show( "Trying to find available rooms", true, true, "No rooms found", "Create a new one or search again", "OK" );
	}
	
	
	// Creates a new room
	public void CreateRoom( string id = "" )
	{
		string roomName = id;
		if ( id == "" )
		{
			roomName = "room" + Random.Range( 0, 1000000 ).ToString();
		}
	
		m_client.Multiplayer.CreateJoinRoom( roomName, "CubeTris", true, null, null, JoinRoomSuccess, JoinRoomFail ); 
		m_connectionVisual.Show( "Awaiting connections", true, true, "Error", "Try again", "OK" );
	}
	
	
	// Trying to join and existing room
	// IN - RoomInfo - room to join
	public void JoinRoom( RoomInfo room )
	{
		m_client.Multiplayer.JoinRoom( room.Id, null, JoinRoomSuccess, JoinRoomFail );
		m_connectionVisual.Show( "Awaiting connections", true, true, "Error", "Try again", "OK" );
	}
	
	
	// Command sent by builder
	// IN - int id - moved block id
	// IN - Vector3 position - new block position
	public void SendMoveBlockCommand( int id, Vector3 position )
	{
		if ( m_connection != null )
		{
			m_connection.Send( Message.Create( Constants.MOVED_BLOCK, id, string.Format( "{0},{1},{2}", position.x, position.y, position.z ) ) );
		}
	}
	
	
	// Command sent by builder to syncronize level layout
	// IN - List of figures
	public void SyncLevel( List< BlockFigure > figures )
	{
		
	}
	
	
	// Command sent by defender
	// IN - int id - destroyed missile ID
	public void SendDestroyMissileCommand( int id )
	{
		if ( m_connection != null )
		{
			m_connection.Send( Message.Create( Constants.DESTROYED_MISSILE, id ) );
		}
	}
	
	#endregion
	
	
	#region Events
		

	// Called when init is successful
	private void InitSuccessResponse( Client client )
	{
		int i = 0;
		m_client = client;
				
		// Set development server 	
		if ( m_developmentServer )
        {
            ServerEndpoint se = new ServerEndpoint( m_developmentServerIP, 8184 );
            m_client.Multiplayer.DevelopmentServer = se;
        }
		
		Debug.Log( "Init Connection successfull" );
		
		// Hide visual
		m_connectionVisual.Hide(); 
		
		//MenuManager.Instance.ShowScreen( "RoomsScreen" );
		RetrieveRooms();
	}
	
	
	// Called when there is an error is Init connection command
	private void InitFailResponse( PlayerIOError error )
	{
		Debug.Log( "Init Connection failed" + error.ToString() );
		
		// Hide visual and exit upon click
		m_connectionVisual.Hide( true, true ); 
	}
	
	
	// Called after successfully joining room
	private void JoinRoomSuccess( Connection connection )
	{
		m_connection = connection;
		
		// Set callback for further messages
		m_connection.OnMessage += OnMessage;
		
		// Hide visual
		//m_connectionVisual.Hide( false, false );
	}
	
	
	// Called when there is a problem joining room
	private void JoinRoomFail( PlayerIOError error )
	{
		Debug.Log( "Join room failed" + error.ToString() );
		
		// Hide visual
		m_connectionVisual.Hide( true, false ); 
	}
	
	
	// Called when retrieving list of rooms
	private void FoundRooms( RoomInfo[] rooms )
	{
		m_rooms = rooms;
		m_connectionVisual.Hide( false, false );
		
		// If there is an available room, join it, otherwise create a new room
		
		// First check - rooms count is bigger than 0
		if ( rooms.Length > 0 )
		{
			// Second check - the room has less than 2 players which means it can be joined
			foreach( RoomInfo room in rooms )
			{
				if ( room.OnlineUsers > 0 && room.OnlineUsers < 2 )
				{
					// Join the room
					JoinRoom( room );
					return;
				}
			}
		}
		
		// If we got here than we couldn't find a room to join
		// Create a new one
		CreateRoom();
	}
	
	
	// Could not find active rooms
	private void ErrorFindingRooms( PlayerIOError error )
	{
		Debug.Log( "Error finding rooms" + error.ToString() );
		m_connectionVisual.Hide( false, true );
	}
	
	
	// Called when message is received from server
	private void OnMessage( object sender, Message message )
	{
		switch( message.Type )
		{
			case Constants.PLAYER_CREATED:
					Debug.Log( "USER ID: " + message.GetInt( 0 ).ToString() );
					PlayerManagerLite.Instance.UserId = message.GetInt( 0 );
					m_connection.Send( Message.Create( Constants.INIT_PLAYER ) );
					break;	
			
			case Constants.RECEIVE_XML:
					string xmlName = message.GetString( 0 );
					string[] playerOneRole = message.GetString( 1 ).Split( ',' );
					string[] playerTwoRole = message.GetString( 2 ).Split( ',' );
					if( System.Convert.ToInt32( playerOneRole[0] ) == PlayerManagerLite.Instance.UserId )
					{
						GameProperties.role = ( Role )System.Convert.ToInt32( playerOneRole[ 1 ] );
					}
					else
					{
						GameProperties.role = ( Role )System.Convert.ToInt32( playerTwoRole[ 1 ] );
					}
					
					Debug.Log( "Player got role: " + GameProperties.role.ToString() );
			
					m_connectionVisual.Hide( false, false );
					ApplicationManager.Instance.MoveToGame( xmlName );
					m_gameStarted = true;
			
					break;
				
			case Constants.START_GAME:
					
					break;
			
			case Constants.MOVED_BLOCK:
				m_gameManager.MovedBlock( message.GetInt( 0 ), message.GetString( 1 ) );
				break;
			
			case Constants.DESTROYED_MISSILE:
				m_gameManager.DestroyRocket( message.GetInt( 0 ) );
				break;
			
			case Constants.FINISH_GAME:
				
					break;
				
			
			
		}
	}
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
