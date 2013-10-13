/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using PlayerIOClient;


/* Static events system */
public class EventsManager
{
	#region Events
	
	/* Called when a screen is shown */
	// Delegate
	// IN - string name - shown screen name
	public delegate void ShowScreen( string name );	
	// Event
	public static event ShowScreen OnShowScreen;
	// Trigger
	public static void RaiseOnShowScreen( string name )
    {
        if ( OnShowScreen != null)
        {
            // Invoke the subscribed event-handler(s)
            OnShowScreen( name );  
        }
	}
	
	
	/* Called when there is a single touch event */
	// Delegate
	// IN - Vector3 pos - touch position ( world coordinates )
	public delegate void SingleTouch( Vector3 pos );	
	// Event
	public static event SingleTouch OnSingleTouch;
	// Trigger
	public static void RaiseOnSingleTouch( Vector3 pos )
    {
        if ( OnSingleTouch != null)
        {
            // Invoke the subscribed event-handler(s)
            OnSingleTouch( pos );  
        }
	}
	
	
	/* Called when level is loading */
	// Delegate
	// IN - int level - level number
	// IN - int difficulty - difficulty of the game
	public delegate void LevelLoading();	
	// Event
	public static event LevelLoading OnLevelLoading;
	// Trigger
	public static void RaiseOnLevelLoading()
    {
        if ( OnLevelLoading != null)
        {
            // Invoke the subscribed event-handler(s)
            OnLevelLoading();  
        }
	}
	
	
	/* Called when level is finished loading */
	// Delegate
	// IN - int level - level number
	// IN - int difficulty - difficulty of the game
	public delegate void LevelLoaded();	
	// Event
	public static event LevelLoaded OnLevelLoaded;
	// Trigger
	public static void RaiseOnLevelLoaded()
    {
        if ( OnLevelLoaded != null)
        {
            // Invoke the subscribed event-handler(s)
            OnLevelLoaded();  
        }
	}
	
	
	/* Called when loading screen is ready */
	// Delegate
	public delegate void LoadingScreenReady();	
	// Event
	public static event LoadingScreenReady OnLoadingScreenReady;
	// Trigger
	public static void RaiseOnLoadingScreenReady()
    {
        if ( OnLoadingScreenReady != null)
        {
            // Invoke the subscribed event-handler(s)
            OnLoadingScreenReady();  
        }
	}

	#endregion
}
