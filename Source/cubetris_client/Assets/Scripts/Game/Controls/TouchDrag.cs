using UnityEngine;
using System.Collections;

/* Allows touch drag controls of the parent object */
public class TouchDrag : MonoBehaviour 
{
	#region Members
	
	private const float 		WORLD_Z = 3.3f;
	
	private Vector3 			m_deltaPosition;
	
	public bool					m_touching = false;
	
	public PlayerIOConnector	m_connection;
	public BlockFigure			m_figure;
	
	#endregion


	#region Methods
	
	// Called first
	private void Awake()
	{
		
	}
	
	
	// Called when object with the script attached becomes active
	private void OnEnable()
	{
		EasyTouch.On_Drag += On_Drag;
		EasyTouch.On_DragStart += On_DragStart;
		EasyTouch.On_DragEnd += On_DragEnd;
	}
	
	
	// Called when object with the script attached become inactive
	private void OnDisable()
	{
		UnsubscribeEvent();
	}
	
	
	// Called when object is destroyed
	private void OnDestroy()
	{
		UnsubscribeEvent();
	}
	

	// Unsubcribe from drag events
	private	void UnsubscribeEvent()
	{
		EasyTouch.On_Drag -= On_Drag;
		EasyTouch.On_DragStart -= On_DragStart;
		EasyTouch.On_DragEnd -= On_DragEnd;
	}	
	
	
	
	// Use this for initialization
	private void Start() 
	{
		m_connection = ( FindObjectOfType( typeof( PlayerIOConnector ) ) as PlayerIOConnector );
		m_figure = gameObject.GetComponent< BlockFigure >();
	}
	
	
	// Update is called once per frame
	private void Update() 
	{
	
	}
	
	
	// Init the controls
	private void Init() 
	{
	
	}
	
	
	#endregion
	
	
	#region Events
	
	// At the drag beginning 
	private void On_DragStart( Gesture gesture )
	{
		if ( GameProperties.role == Role.BUILDER || GameProperties.role == Role.BOTH )
		{
			m_touching = true;
			
			// Verification that the action on the object
			if ( gesture.pickObject == gameObject )
			{
				gameObject.renderer.material.color = Color.white;
				// Reset object rotation, to always face the cam
				gameObject.transform.localRotation = Quaternion.identity;
				// the world coordinate from touch for z=5
				Vector3 position = gesture.GetTouchToWordlPoint( WORLD_Z );
					
				m_deltaPosition = position - transform.position;
			}	
		}
	}
	
	
	// During the drag
	private void On_Drag( Gesture gesture )
	{
		if ( GameProperties.role == Role.BUILDER || GameProperties.role == Role.BOTH )
		{
			// Verification that the action on the object
			if ( gesture.pickObject == gameObject )
			{
				// Reset object rotation, to always face the cam
				gameObject.transform.localRotation = Quaternion.identity;
				// the world coordinate from touch for z=5
				Vector3 position = gesture.GetTouchToWordlPoint( WORLD_Z );
				
				transform.position = position - m_deltaPosition;
				
				// Get the drag angle
				float angle = gesture.GetSwipeOrDragAngle();
				
				if ( GameProperties.role != Role.BOTH )
				{
					// Notify second player
					if ( m_connection != null && m_figure != null )
					{
						m_connection.SendMoveBlockCommand( m_figure.m_id, transform.position );
					}
				}
			}
		}
	}
	
	
	// At the drag end
	private void On_DragEnd( Gesture gesture )
	{
		if ( GameProperties.role == Role.BUILDER || GameProperties.role == Role.BOTH )
		{
			// Verification that the action on the object
			if ( gesture.pickObject == gameObject )
			{
				// Reset object rotation, to always face the cam
				gameObject.transform.localRotation = Quaternion.identity;
				// transform.position = new Vector3( 3f, 1.8f, -5f );
				gameObject.renderer.material.color = GetComponent< BlockFigure >().m_selectedColor;
				
				m_touching = false;
			}
		}
	}
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
