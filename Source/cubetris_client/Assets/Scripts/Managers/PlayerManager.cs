/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Manages player data 
 * using Singleton pattern */
public class PlayerManager : MonoBehaviour 
{
	#region Members
	
	private static PlayerManager 				m_instance;							// Reference to self instance
	
	// Game variables
	// Keys to access PlayerPrefs
	public const string							KEY_HAS_DATA = "HAS_DATA";			
	public const string 						KEY_SHOW_ADS = "SHOW_ADS";								
	public const string							KEY_GAME_IN_PROGGRESS = "GAME_IN_PROGGRESS";						
	public const string							KEY_UNLOCKED_DIFFICULTY = "UNLOCKED_DIFFICULTY";						
	public const string							KEY_LAST_DIFFICULTY = "LAST_DIFFICULTY";						
	public const string							KEY_LAST_LEVEL = "LAST_LEVEL";								
	public const string							KEY_SCORE = "SCORE";									
	public const string							KEY_USER_NAME = "USER_NAME";								
	public const string							KEY_USER_ID = "USER_ID";	
	public const string							KEY_MUSIC = "MUSIC";	
	public const string							KEY_SOUND = "SOUND";	
	public const string							KEY_VIBRATION = "VIBRATION";	
	public const string							KEY_SHOW_TIPS = "SHOW_TIPS";
	public const string							KEY_FIRST_GAME = "FIRST_GAME";
	
	// Data variables
	private bool								m_hasData;			
	private bool 								m_showAds;							// Flag which indicates if ads should be displayed
	private bool								m_gameInProgress;					// In middle of saved game
	private bool								m_firstGame;						// First game flag
	private int									m_unlockedDifficulty;				// Highest unlocked difficulty
	private int									m_lastDifficulty;					// Difficulty in middle of game
	private int									m_lastLevel;						// Last reached level if in middle of game
	private int									m_score;							// Active game score
	private string								m_userName;							// User name for online activity
	private string								m_userId;							// Id received during registration
	
	// User settings variables
	private bool								m_music = true;						// True = play music, false = don't play
	private bool								m_sound = true;						// True = play game sounds, false = don't play
	private bool								m_vibration = true;					// True = vibrate, false = don't vibrate
	private bool								m_showTips = true;					// True = show, false = don't show
	
	
	// Static getter
	public static PlayerManager Instance
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
		LoadUserData();
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
	
	
	// Load user data from save file
	public void LoadUserData()
	{
		// Check if there is data store
		if ( PlayerPrefs.GetString( KEY_HAS_DATA ) != "" )
		{
			m_showAds = System.Convert.ToBoolean( PlayerPrefs.GetString( KEY_SHOW_ADS ) );
			m_gameInProgress = System.Convert.ToBoolean( PlayerPrefs.GetString( KEY_GAME_IN_PROGGRESS ) );
			m_firstGame = System.Convert.ToBoolean( PlayerPrefs.GetString( KEY_FIRST_GAME ) );
			m_unlockedDifficulty = PlayerPrefs.GetInt( KEY_UNLOCKED_DIFFICULTY );
			m_lastDifficulty = PlayerPrefs.GetInt( KEY_LAST_DIFFICULTY );
			m_lastLevel = PlayerPrefs.GetInt( KEY_LAST_LEVEL );
			m_score = PlayerPrefs.GetInt( KEY_SCORE );
			m_userId = PlayerPrefs.GetString( KEY_USER_ID );
			m_userName = PlayerPrefs.GetString( KEY_USER_NAME );
			m_music = System.Convert.ToBoolean( PlayerPrefs.GetString( KEY_MUSIC ) );
			m_sound = System.Convert.ToBoolean( PlayerPrefs.GetString( KEY_SOUND ) );
			m_vibration = System.Convert.ToBoolean( PlayerPrefs.GetString( KEY_VIBRATION ) );
			m_showTips = System.Convert.ToBoolean( PlayerPrefs.GetString( KEY_SHOW_TIPS ) );
		}
		else
		{
			// No data yet
			ResetUserData();
		}
		
	}
	
	
	// Save user data to a save fila
	public void SaveUserData()
	{
		PlayerPrefs.SetString( KEY_HAS_DATA, m_hasData.ToString() );
		PlayerPrefs.SetString( KEY_SHOW_ADS, m_showAds.ToString() );
		PlayerPrefs.SetString( KEY_GAME_IN_PROGGRESS, m_gameInProgress.ToString() );
		PlayerPrefs.SetString( KEY_FIRST_GAME, m_firstGame.ToString() );
		PlayerPrefs.SetInt( KEY_UNLOCKED_DIFFICULTY, m_unlockedDifficulty );
		PlayerPrefs.SetInt( KEY_LAST_DIFFICULTY, m_lastDifficulty );
		PlayerPrefs.SetInt( KEY_LAST_LEVEL, m_lastLevel );
		PlayerPrefs.SetInt( KEY_SCORE, m_score );
		PlayerPrefs.SetString( KEY_USER_ID, m_userId );
		PlayerPrefs.SetString( KEY_USER_NAME, m_userName );
		PlayerPrefs.SetString( KEY_MUSIC, m_music.ToString() );
		PlayerPrefs.SetString( KEY_SOUND, m_sound.ToString() );
		PlayerPrefs.SetString( KEY_VIBRATION, m_vibration.ToString() );
		PlayerPrefs.SetString( KEY_SHOW_TIPS, m_showTips.ToString() );
		PlayerPrefs.Save();
	}
	
	
	// Reset all user data and save it
	public void ResetUserData()
	{
		m_hasData = true;			
		m_showAds = true;					
		m_gameInProgress = false;	
		m_firstGame = true;
		m_unlockedDifficulty = 0;			
		m_lastDifficulty = 0;				
		m_lastLevel = 0;					
		m_score = 0;						
		m_userName = "";					
		m_userId = "";						
		m_music = true;						
		m_sound = true;						
		m_vibration = true;					
		m_showTips = true;	
		
		SaveUserData();
	}
	
	
	// Resets game in progress data ( should not be called during game )
	public void ResetGameInProggress()
	{
		m_gameInProgress = false;
		m_lastDifficulty = 0;				
		m_lastLevel = 0;					
		m_score = 0;
		
		SaveUserData();
	}
	
	#endregion
	
	
	#region Getters/Setters

	public bool GameInProgress 
	{
		get 
		{
			return this.m_gameInProgress;
		}
		set 
		{
			m_gameInProgress = value;
		}
	}

	public int LastDifficulty 
	{
		get 
		{
			return this.m_lastDifficulty;
		}
		set 
		{
			m_lastDifficulty = value;
		}
	}

	public int LastLevel 
{
		get 
		{
			return this.m_lastLevel;
		}
		set 
		{
			m_lastLevel = value;
		}
	}

	public bool Music 
	{
		get 
		{
			return this.m_music;
		}
		set 
		{
			m_music = value;
		}
	}

	public int Score 
	{
		get {
			return this.m_score;
		}
		set {
			m_score = value;
		}
	}

	public bool ShowAds 
	{
		get 
		{
			return this.m_showAds;
		}
		set 
		{
			m_showAds = value;
		}
	}

	public bool Sound 
	{
		get 
		{
			return this.m_sound;
		}
		set 
		{
			m_sound = value;
		}
	}

	public int UnlockedDifficulty 
	{
		get 
		{
			return this.m_unlockedDifficulty;
		}
		set 
		{
			m_unlockedDifficulty = value;
		}
	}

	public string UserId 
	{
		get 
		{
			return this.m_userId;
		}
		set 
		{
			m_userId = value;
		}
	}

	public string UserName 
	{
		get 
		{
			return this.m_userName;
		}
		set 
		{
			m_userName = value;
		}
	}

	public bool Vibration 
	{
		get 
		{
			return this.m_vibration;
		}
		set 
		{
			m_vibration = value;
		}
	}
	
	
	#endregion
}
