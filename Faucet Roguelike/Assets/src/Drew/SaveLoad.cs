using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; // Used as the formatter. Can be replaced if we want something human-readble.

public class SaveLoad : MonoBehaviour
{	
    private SerializableClass testSerializableClass = new SerializableClass();
    
    void Awake()
    {
		// This will be removed eventually. Only included for the purposes of the demonstration.
        if (File.Exists(Application.persistentDataPath + "/testSave.sav"))
        {
            Debug.Log("File exists, loading...");
            Load("testSave");
            testSerializableClass.setTimesStarted(testSerializableClass.getTimesStarted() + 1);
            Save("testSave");
        }
        else
        {
            Debug.Log("File not found, creating...");
            testSerializableClass.setTimesStarted(1);
            Save("testSave");
        }
        Debug.Log("Game has been started " + testSerializableClass.getTimesStarted() + " time(s).");
    }

    public void Save(string filename)
    {
        Debug.Log("Game Saved"); // Included until I fully implement saving
		
        var formatter = new BinaryFormatter();
        FileStream save = File.Open(Application.persistentDataPath + "/" + filename + ".sav", FileMode.Create);
        // This will make it overwite an existing save of the same name.
        // Not sure if it should be me or UI people who checks for existing filenames first

        formatter.Serialize(save, testSerializableClass);
        save.Close();
    }

    public void Load(string filename)
    {
        if (File.Exists(Application.persistentDataPath + "/" + filename + ".sav"))
        {
            Debug.Log("Game loaded"); // Included until I fully implement loading
			
            var formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename + ".sav", FileMode.Open);

            testSerializableClass = (SerializableClass)formatter.Deserialize(file);
            file.Close();
			
            // todo: Make this part more general so it can be used for the actual project.
        }
        else
        {
            Debug.Log("File did not exist.");
        }

    }

}

// Because of the way C# handles serialization, it's necessary to create what will probably become an abomination here.
// Either that, or I'm misunderstanding something, which is entirely possible. 
[Serializable]
class SerializableClass
{
    private int timesStarted;

    public int getTimesStarted()
    {
        return timesStarted;
    }

    public void setTimesStarted(int value)
    {
        timesStarted = value;
    }
}