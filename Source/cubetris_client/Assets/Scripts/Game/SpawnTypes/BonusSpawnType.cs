using UnityEngine;
using System.Collections;

// Container for spawnable bonus element
public class BonusSpawnType
{
	// Possible figure types
	public enum BonusType
	{
		SLOW,
		SCORE,
		DEFENCE
	}
	
	private BonusType		m_type;			// Selected figure type
	private float 			m_speed;		// Rocket speed
	private float			m_time;			// Time to spawn it, relative to level time
	private int				m_spawner;		// Spawner number
	
	
	// Ctor
	// IN - Type type - type of figure to set
	// IN - float time - time to spawn it
	public BonusSpawnType(  string type, float speed, float time , int spawner )
	{
		switch(type)
		{
		case "score":
			m_type = BonusType.SCORE;
			break;
			
		case "slow":
			m_type = BonusType.SLOW;
			break;
			
		case "defence":
			m_type = BonusType.DEFENCE;
			break;
		}
		
		m_speed = speed;
		m_time = time;
		m_spawner = spawner;
	}
	
	
	// Accessors
	public BonusType type 
	{
		get
		{
			return this.m_type;
		}
	}
	
	
	public float time 
	{	
		get 
		{
			return this.m_time;
		}
	}
	
	
	public int spawner 
	{
		get 
		{
			return this.m_spawner;
		}
	}

	public float speed {
		get {
			return this.m_speed;
		}
	}
}

