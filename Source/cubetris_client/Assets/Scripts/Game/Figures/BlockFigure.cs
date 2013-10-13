/*----------------------------------------------*/
/*           	Slice A Cig 					*/
/* 			Copyright © 2013 LaMa Games			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;

public class BlockFigure : BaseFigure
{
	#region Members
	
	public Color 		m_selectedColor = Color.cyan;
	public bool			m_inactive = false;
	public int			m_id;
	
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
	
	}
	
	
	// Update is called once per frame
	protected override void Update() 
	{
	
	}
	
	protected override void OnCollisionEnter( Collision collision )
	{
		// Currently KINECT only supported for defender, so we ingore it touching blocks
		if ( !m_inactive )
		{
			base.OnCollisionEnter( collision );

			TouchDrag touch = GetComponent< TouchDrag >();
						
			if ( touch != null && !touch.m_touching )
			{
				// If touch is inactive remove it
				Destroy( GetComponent< TouchDrag >() );
				m_inactive = true;
			}
			
			if ( GameProperties.role != Role.BOTH )
			{
				rigidbody.mass = 10000;
			}
		}
	}
	
	#endregion
	
	
	#region Events
	
	
	#endregion
	
	
	#region Setters/Getters
	
	
	#endregion
}
