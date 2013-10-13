/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Abstract interface for control devices */
public class BaseControls : MonoBehaviour 
{
	#region Members
	
	
	
	#endregion


	#region Methods
	
	// Called first
	protected virtual void Awake()
	{
		
	}
	
	
	// Called when object with the script attached becomes active
	protected virtual void OnEnable()
	{
		
	}
	
	
	// Called when object with the script attached become inactive
	protected virtual void OnDisable()
	{
		
	}
	
	
	// Called when object is destroyed
	protected virtual void OnDestroy()
	{
		
	}
	
	
	
	// Use this for initialization
	protected virtual void Start() 
	{
	
	}
	
	
	// Update is called once per frame
	protected virtual void Update() 
	{
	
	}
	
	
	// Init the controls
	public virtual void Init() 
	{
	
	}
	
	
	#endregion
	
	
	#region Events
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
