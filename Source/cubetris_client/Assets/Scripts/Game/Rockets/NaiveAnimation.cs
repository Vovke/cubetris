/*----------------------------------------------*/
/*           	Slice A Cig 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


// Very naive animation class ( HACKZOR )
public class NaiveAnimation : MonoBehaviour 
{
	#region Members
	
	public Material[]		m_materials;	// List of materials for animation
	public float			m_ttl;			// Animation total time
	private float			m_currentTime;	// Animation current
	private float			m_nextTimeSegment;
	private float			m_timeSegment;	// Time segment for switching materials
	public bool				m_autoDestroy = true; // Destroy after finishing
	private MeshRenderer	m_renderer;		// Reference to renderer
	private int				m_currentMaterial;
	
	#endregion


	#region Methods
	
	// Called first
	void Awake()
	{
		// Cache renderer
		m_renderer = GetComponentInChildren< MeshRenderer >();
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
	public void Init() 
	{
		// Calculate timers
		m_timeSegment = m_ttl / m_materials.Length;
		m_currentTime = 0;
		
		// Set first material
		// NO error testing!!!
		m_currentMaterial = 0;
		m_nextTimeSegment = m_timeSegment * ( m_currentMaterial + 1 );
		m_renderer.material = m_materials[ m_currentMaterial ];
	}
	
	
	// Update is called once per frame
	void Update() 
	{
		// Increment time
		m_currentTime += Time.deltaTime;
		
		if ( m_currentTime >= m_nextTimeSegment )
		{
			m_currentMaterial++;
			
			if ( m_currentMaterial >= m_materials.Length )
			{
				if ( m_autoDestroy )
				{
					Destroy( gameObject );
				}
				
				return;
			}
			
			m_nextTimeSegment = m_timeSegment * ( m_currentMaterial + 1 );
			m_renderer.material = m_materials[ m_currentMaterial ];
		}
	}
	
	#endregion
	
	
	#region Events
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
