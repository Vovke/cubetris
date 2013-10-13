/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Responsible for playing audio clips */
public class SoundManager : MonoBehaviour 
{
	#region Members
	
	private static SoundManager 					m_instance;							// Reference to self instance
	
	#endregion


	#region Methods
	
	// Static getter
	public static SoundManager Instance
    {
        get
        {
            return m_instance;
        }
    }
	
	
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
	
	#endregion
}
