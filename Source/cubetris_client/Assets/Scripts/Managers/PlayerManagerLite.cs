/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Manages player data 
 * using Singleton pattern */
public class PlayerManagerLite : MonoBehaviour 
{
	#region Members
	
	private static PlayerManagerLite 			m_instance;							// Reference to self instance
	
	// Data variables
	private int									m_score;							// Active game score
	private int									m_userId;							// Id received upon joining room
	
	
		// Called first
	void Awake()
	{
		// Set instance
		m_instance = this;
	}
	
	
	// Static getter
	public static PlayerManagerLite Instance
    {
        get
        {
            return m_instance;
        }
    }
	
	
	public int Score 
	{
		get 
		{
			return this.m_score;
		}
		set 
		{
			m_score = value;
		}
	}
	
	
	public int UserId 
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
	
	#endregion
}
