/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;

public enum ControlsType
{
	TOUCH,
	KINECT
}

public enum Role
{
	BUILDER = 0,
	DEFENDER,
	BOTH
}


/* Active game properties */
public class GameProperties 
{
	#region Methods
	
	// Const values
	
	
	// Changeble values
	private static ControlsType			m_selectedControls = ControlsType.TOUCH;
	private static Role					m_role = Role.BOTH;

	#endregion
	
	#region Methods

	// Builds properties from save data
	public static void BuildPropertiesFromSave( string[] args )
	{
		
	}

	
	#endregion
	
	#region Getters/Setters
	
	// Choosen game controls
	public static ControlsType selectedControls
	{
		get 
		{
			return m_selectedControls;
		}
		set 
		{
			m_selectedControls = value;
		}
	}
	
	
	// Player role
	public static Role role 
	{
		get 
		{
			return m_role;
		}
		set 
		{
			m_role = value;
		}
	}
	#endregion
}
