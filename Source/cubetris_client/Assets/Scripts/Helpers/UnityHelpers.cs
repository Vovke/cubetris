/*----------------------------------------------*/
/*           	Cubetris				*/
/* 			Copyright © 2013 Vova Lando& Slava Svirsky			*/
/*----------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

/* Set of methods to help working with unity */
public class UnityHelpers
{
	#region Methods
	
	// Generates a random number
	// IN - int min - lower bound
	// IN - int max - high bound
	// IN - int size - number length ( default = 1 )
	public static int RealRandomGenerator( int min, int max, int size = 1 )
	{
	    // Random byte array size of 1
        byte[] randomNumber = new byte[ size ];
		
		// Create an RNGCryptoProvider
        RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();
        int rand;
		
		// Loop until return
        while ( true )
        {
			// Get sequence of random numbers
            Gen.GetBytes( randomNumber );
			
			// Convert the byte to int
            rand = System.Convert.ToInt32( randomNumber[0] );
			
			// Check if the generated number is within limit
            if ( rand < max  && rand >= min)
			{
                return rand;
			}
        }   
	}
	
	
	// Find child by name in the whole transform tree
	public static Transform FindChildDeep( Transform parent, string name )
	{
		if ( parent.name == name )
		{
			return parent;
		}
 
        for ( int i = 0; i < parent.childCount; ++i )
        {
			// Search recursively in inner children
            Transform result = FindChildDeep( parent.GetChild( i ), name );
 
            if (result != null) 
			{
				return result;
			}
        }
 
        return null;

	}
	
	#endregion
}
