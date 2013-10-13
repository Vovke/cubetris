public class Constants
{
    /* Send message */
    public const string PING_MSG = "PING";                  	// Ping probing  
    public const string GAME_READY = "GAME_READY";          	// Player is ready to start game ( loading completed )
    public const string GAME_ENDED = "GAME_ENDED";          	// Message from player that game is ended, Score should be sent at this point
    public const string INIT_PLAYER = "INIT_PLAYER";        	// New player initialisation

    /* Response message */
    public const string RECEIVE_XML = "RECEIVE_XML";        	// Sending game XML to players
    public const string START_GAME = "START_GAME";          	// Sended after both players sent GAME_READY command
    public const string FINISH_GAME = "FINISH_GAME";        	// Sent upon finishing game ( both players sent GAME_ENDED command )
    public const string PLAYER_CREATED = "PLAYER_CREATED";  	// Responce upon new player initialisation
	
	/* Defender messages */
	public const string DESTROYED_MISSILE = "DESTROYED_MISSILE";// Received by builder, means the defender have destroyed a missile
	
	/* Builder messages */
	public const string MOVED_BLOCK = "MOVED_BLOCK";			// Received by defender, means the builder moved blocks


}