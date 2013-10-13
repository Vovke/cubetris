/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/* Responsible for instanitiating prefabs on demand
 * Uses Singleton pattern */
public class ResourcePoolManager : MonoBehaviour 
{
	#region Members
	
	private static ResourcePoolManager 					m_instance;						// Reference to self instance	
	
	public GameObject[] 								m_objectPrefabs;				// Object prefabs ( Assigned from editor )
	public List< GameObject >[] 						m_pooledObjects;				// List of pooled objects 
	public int[] 										m_amountToBuffer;				// Num of objects to keep loaded for each prefab
    public const int 									DEFAULT_BUFFER = 3;				// Default value if no value were defined
	private GameObject 									m_containerObject;				// Object to contain the inactive objects
	
	#endregion


	#region Methods
	
	// Static getter
	public static ResourcePoolManager Instance
    {
        get
        {
            return m_instance;
        }
    }
	
	// Called first
	void Awake()
	{
		m_instance = this;
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
	void Start() 
	{
		// Create the container object
		m_containerObject = new GameObject( "ObjectPool" );
		
		// Create a list for each prefab
        m_pooledObjects = new List< GameObject >[ m_objectPrefabs.Length ];

        int i = 0;
		
		// Iterate through all the lists and Initialize them
		// then fill the list with instances of the defined prefab
        foreach ( GameObject objectPrefab in m_objectPrefabs )
        {
            m_pooledObjects[ i ] = new List< GameObject >(); 
			
            int bufferAmount;
			
            if ( i < m_amountToBuffer.Length ) 
			{
				// Use pre defined buffer size
				bufferAmount = m_amountToBuffer[ i ];
			}
            else
			{
				// Use default buffer size
                bufferAmount = DEFAULT_BUFFER;
			}
			
            for ( int n = 0; n < bufferAmount; n++ )
            {
                GameObject newObj = Instantiate( objectPrefab ) as GameObject;

                newObj.name = objectPrefab.name;
				newObj.tag = objectPrefab.tag;

                PoolObject( newObj );
            }
			
            i++;
		}
	}

   
	// Get object of some type
	// IN - string objectType - The name of required prefab
    public GameObject GetObjectForType( string objectType )
    {
        for( int i = 0; i < m_objectPrefabs.Length; i++ )
        {
			// Check if object type exists
            if( m_objectPrefabs[ i ].name == objectType )
            {
					// Instantiate a new project
					GameObject obj = Instantiate( m_objectPrefabs[ i ] ) as GameObject;
					obj.SetActiveRecursively( false );
					obj.transform.parent = null;
                    obj.active = true;
                    return obj;
            }
        }

        //If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
        return null;
    }

	
	// Return object to pool
	// IN - GameObject obj - reference to the object
    public void PoolObject( GameObject obj )
    {
        for ( int i = 0; i < m_objectPrefabs.Length; i++ )
        {
			// Check if object name matches  any of the pre-defined prefabs
            if ( m_objectPrefabs[ i].name == obj.name || m_objectPrefabs[ i ].tag == obj.tag )
            {
				// Disable the object and add it to the pool container object
                obj.SetActiveRecursively( false );
                obj.transform.parent = m_containerObject.transform;
                m_pooledObjects[ i ].Add( obj );
                return;
            }
        }
    }
	
	#endregion
}
