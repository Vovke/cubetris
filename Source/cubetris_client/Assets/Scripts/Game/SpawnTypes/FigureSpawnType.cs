using UnityEngine;
using System.Collections;

// Container for spawnable element
public class FigureSpawnType
{
	// Possible figure types
	public enum FigureType
	{
		CUBE,
		RECTANGLE,
		TRIANGLE
	}
	
	private int				m_id;			// Figure ID
	private FigureType		m_type;			// Selected figure type
	private float			m_time;			// Time to spawn it, relative to level time
	private int				m_spawner;		// Spawner number
	private float 			m_speed;		// Figure Speed
	private int 			m_color;		// Figure color
	
	
	
	// Ctor
	// IN - Type type - type of figure to set
	// IN - float time - time to spawn it
	public FigureSpawnType( int id, string type, float time , int spawner, float speed, int figureColor )
	{
		switch(type)
		{
		case "cube":
			m_type = FigureType.CUBE;
			break;
		case "triangle":
			m_type = FigureType.TRIANGLE;
			break;
		case "rectangle":
			m_type = FigureType.RECTANGLE;
			break;
		}
			
		m_time = time;
		m_spawner = spawner;
		m_speed = speed;
		m_color = figureColor;
		m_id = id;
	}
	
	
	// Accessors
	public FigureType type 
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

	public int color 
	{
		get 
		{
			return this.m_color;
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

