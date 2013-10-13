/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;


/* Touch and mouse controls */
public class TouchControls : BaseControls
{
	private GameObject 			m_trail;
	private const float 		WORLD_Z = 3.3f;
	
	// Subscribe to events
	protected override void OnEnable()
	{
		EasyTouch.On_SwipeStart += On_SwipeStart;
		EasyTouch.On_Swipe += On_Swipe;
		EasyTouch.On_SwipeEnd += On_SwipeEnd;		
	}
	

	protected override void OnDisable()
	{
		UnsubscribeEvent();
		
	}
	
	
	protected override void OnDestroy()
	{
		UnsubscribeEvent();
	}
	
	
	private void UnsubscribeEvent()
	{
		EasyTouch.On_SwipeStart -= On_SwipeStart;
		EasyTouch.On_Swipe -= On_Swipe;
		EasyTouch.On_SwipeEnd -= On_SwipeEnd;	
	}
	
	
	protected override void Start()
	{

	}
	
	
	public override void Init()
	{

	}
	
	
	// At the swipe beginning 
	private void On_SwipeStart( Gesture gesture)
	{
		if ( GameProperties.role == Role.DEFENDER || GameProperties.role == Role.BOTH )
		{
			// Only for the first finger
			if ( gesture.fingerIndex == 0 && m_trail== null )
			{ 
				Vector3 position = gesture.GetTouchToWordlPoint( WORLD_Z );
				transform.localPosition = position;
				m_trail = Instantiate( Resources.Load( "Trail" ), position, Quaternion.identity ) as GameObject;
			}
		}
	}
	
	
	// During the swipe
	private void On_Swipe( Gesture gesture )
	{
		if ( GameProperties.role == Role.DEFENDER || GameProperties.role == Role.BOTH )
		{
			if ( m_trail != null )
			{	
				Vector3 position = gesture.GetTouchToWordlPoint( WORLD_Z );
				transform.localPosition = position;
				m_trail.transform.position = position;
				
				// Check collision with rockets
				Ray ray = Camera.main.ScreenPointToRay( gesture.position );
				RaycastHit hit;
				if ( Physics.Raycast( ray, out hit, 5 ) )
				{
					// Hit something
					if ( hit.transform.tag == "ROCKET" )
					{
						// Destroy the rocket
						hit.transform.gameObject.GetComponent< BaseRocket >().Destroy();
					}
				}
			}
		}
	}
	
	
	// At the swipe end 
	private void On_SwipeEnd( Gesture gesture )
	{
		if ( GameProperties.role == Role.DEFENDER || GameProperties.role == Role.BOTH )
		{
			if ( m_trail!=null )
			{
				Destroy( m_trail) ;
				
				Vector3 position = gesture.GetTouchToWordlPoint( WORLD_Z );
				transform.localPosition = position;
				
				// Get the swipe angle
				float angles = gesture.GetSwipeOrDragAngle();
			}
		}
	}
}
