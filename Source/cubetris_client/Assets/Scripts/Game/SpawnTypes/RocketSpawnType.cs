using UnityEngine;
using System.Collections;

// Container for spawnable element
public class RocketSpawnType
{
	// Possible figure types
	public enum RocketType
	{
		EXPLOSIVE,
		NON_EXPLOSIVE
	}
	
	private int				m_id;			// Rocket id
	private RocketType		m_type;			// Selected figure type
	private float 			m_speed;		// Rocket speed
	private float			m_time;			// Time to spawn it, relative to level time
	private int				m_spawner;		// Spawner number
	
	
	
	// Ctor
	// IN - Type type - type of figure to set
	// IN - float time - time to spawn it
	public RocketSpawnType( int id, string type, float speed, float time , int spawner )
	{
		switch(type)
		{
		case "explosive_rocket":
			m_type = RocketType.EXPLOSIVE;
			break;
		case "non_explosive_rocket":
			m_type = RocketType.NON_EXPLOSIVE;
			break;
		}
		
		m_speed = speed;
		m_time = time;
		m_spawner = spawner;
		m_id = id;
	}
	
	
	// Accessors
	public RocketType type 
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

	public float speed 
	{
		get 
		{
			return this.m_speed;
		}
	}
	
		
	public int id 
	{
		get 
		{
			return this.m_id;
		}
	}
}

