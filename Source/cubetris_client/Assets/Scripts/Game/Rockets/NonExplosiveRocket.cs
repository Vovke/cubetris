/*----------------------------------------------*/
/*           	Slice A Cig 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


// Non explosive rocket behaviour
public class NonExplosiveRocket : BaseRocket
{
	private enum State
	{
		FLYING,			// Flying before collision
		EXPLOSION,		// Explosion after collision with figure or by input from player
		DEATH			// Death after explosion 
	}
	
	#region Members
	
	private State				m_state;				// Current rocket state
	private float				m_timer;				// State timer
	private float				m_currentForce;			// Active force
	private Vector3				m_direction;			// Flight direction ( where to apply force )
	private const float			COLLISION_MASS = 1000;  // Mass to add during collision
	
	public GameObject			m_smokePrefab;
	
	private static float[] 		m_stateTimes =
	{
		0,			// Flying ( not needed )
		0.1f,		// Explosion 
		0.01f		// Death
	};
	
	
	
	#endregion


	#region Methods
	
	// Called first
	protected override void Awake()
	{
		
	}
	
	
	// Called when object with the script attached becomes active
	protected override void OnEnable()
	{
		
	}
	
	
	// Called when object with the script attached become inactive
	protected override void OnDisable()
	{
		
	}
	
	
	// Use this for initialization
	protected override void Start() 
	{
		base.Start();
	}
	
	
	// Use this for initialization
	public override void Activate( float force, Vector3 direction ) 
	{
		m_direction = direction;
		m_currentForce = force;
		
		// Start the rockect life cycle
		SetState( State.FLYING );
	}
	
	
	// Destroys a rocket 
	public override void Destroy()
	{
		base.Destroy();
		SetState( State.DEATH );
	}
	
	
	// Update is called once per frame
	protected override void Update() 
	{
		// Increment time
		m_timer += Time.deltaTime;
		
		switch( m_state )
		{
			case State.FLYING:
				break;
			
			case State.EXPLOSION:
				if ( m_timer >= m_stateTimes[ ( int )State.EXPLOSION ] )
				{
					// Time to die!
					SetState( State.DEATH );
				}
				break;
			
			case State.DEATH:
				if ( m_timer >= m_stateTimes[ ( int )State.DEATH ] )
				{
					// Return to pool
					Destroy( gameObject );
					// TODO: Fix pooling
					//ResourcePoolManager.Instance.PoolObject( gameObject );
				}
				break;
		}
	}
	
	
	// Set new state
	private void SetState( State state )
	{
		m_state = state;
		m_timer = 0;
		
		switch( m_state )
		{
			case State.FLYING:
				// Apply force 	
				rigidbody.AddForce( m_currentForce * m_direction );
				rigidbody.mass = 1;
				break;
			
			case State.EXPLOSION:
				// TODO: add animation here
				rigidbody.mass += COLLISION_MASS;
				break;
			
			case State.DEATH:
				// TODO: add animation here
			
				// Remove all forces before returning object to pool
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
				rigidbody.isKinematic = true;
				rigidbody.isKinematic = false;
			
				GameObject obj = Instantiate( m_smokePrefab ) as GameObject;
				obj.transform.position = transform.position;
				obj.GetComponent< NaiveAnimation >().Init();
			
				break;
		}
	}
	
	#endregion
	
	
	#region Events
	
	// Collision with other physical object event
	private void OnCollisionEnter( Collision collision ) 
	{
		if ( m_state == State.FLYING )
		{
			// Collision should occcur only ones
			SetState( State.EXPLOSION );
		}
	}
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
