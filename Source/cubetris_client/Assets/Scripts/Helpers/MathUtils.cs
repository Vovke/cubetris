/* Written for SideKick
 * Project: HateBot for Android with XTR
 * Author: Slava Svirsky
 */
using UnityEngine;
using System.Collections;
using System;


/* Mathematical methods for basic helping calculations */
public static class MathUtils 
{
	// Trunscate float to 2 numbers after the dot
	// OUT - float 
	public static float TruncateToDecimalPlace( float numberToTruncate )
	{
		int decimalPlaces = 2;
		decimal power = ( decimal)( Math.Pow( 10.0, ( double)decimalPlaces ) );
		
		return ( float)(Math.Truncate( ( power * (decimal)numberToTruncate ) ) / power );
	}
	
	
	// Gets the overlapping percentage of 2 rectanges
	// OUT - float ( 0 - 1 )
	public static float GetTwoRectanglesOverlapping( Rect rect1, Rect rect2 )
	{
		float overlapPercentage = RectOneRectTwoOverlapping( rect1, rect2 );
		
		if ( overlapPercentage > 0 )
		{
			// Already found overlapping - return it
			return overlapPercentage;
		}
		else
		{
			// Check overlapping from rect 2 perspective
			return RectOneRectTwoOverlapping( rect2, rect1 );	
		}
	}
	
	
	// Helper method for GetTwoRectanglesOverlapping wich calculates the percentage from rect1 perspective
	// OUT - float ( 0 - 1 )
	public static float RectOneRectTwoOverlapping( Rect rect1, Rect rect2 )
	{
		float overlapPercentage = 0;
		
		// Check that rect2 X is within Rect1 width
		if ( rect2.xMin >= rect1.xMin && rect2.xMin < rect1.xMax )
		{
			// Check that rect2 Y is within Rect1 height
			if ( rect2.yMin >= rect1.yMin && rect2.yMin < rect1.yMax )
			{
				// If we got here then rect2 has overlapping area with rect1
				// Calculate X overlap and Y overlap
				float xOverlap = rect1.xMax - rect2.xMin;
				float yOverlap = rect1.yMax - rect2.yMin;
				
				// Calculate overlap area and devide by rect1 area
				overlapPercentage = ( xOverlap * yOverlap ) / ( rect1.height * rect1.width ); 
			}
		}
		
		return overlapPercentage;
	}
}
