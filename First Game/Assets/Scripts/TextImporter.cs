using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour {

    //Variables
    public TextAsset textFile;
    public string[] sentences;

	void Start ()
    {
	    if (textFile)
        {
            sentences = textFile.text.Split('\n');
        }	
	}

}
