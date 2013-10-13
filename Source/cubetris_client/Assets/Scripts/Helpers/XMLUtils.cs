using System.Collections;
using System.Xml;
using System;
using UnityEngine;


// Set of methods for working with XML's
public class XMLUtils
{
	public static XmlDocument LoadXML( string fileName )
	{
		TextAsset textAsset = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml ( textAsset.text );
		
		return doc;
	}
	
	public static int GetAttributeInt( XmlNode node, string name )
	{
		string attrVal = node.Attributes[ name ].Value;
		return System.Convert.ToInt32( attrVal );
	}
	
	
	public static float GetAttributeFloat( XmlNode node, string name )
	{
		string attrVal = node.Attributes[ name ].Value;
		return float.Parse( attrVal );
	}
	
	
	public static string GetAttributeString( XmlNode node, string name )
	{
		string attrVal = node.Attributes[ name ].Value;
		return attrVal;
	}
	
		
	public static Vector2 GetAttributeVector2( XmlNode node, string name )
	{
		string attrVal = node.Attributes[ name ].Value;
		string[] splittedValue = attrVal.Split( ',' );
		return new Vector2( float.Parse( splittedValue[ 0 ] ), float.Parse( splittedValue[ 1 ] ) );
	}
}
