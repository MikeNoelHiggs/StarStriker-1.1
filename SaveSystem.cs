using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{ 

    public static void SaveToSlotOne(PlayerProfile profile)
    {
        string slotOnePath = Application.persistentDataPath + "/SaveGame1.bin";
        Save(slotOnePath, profile);
    }

    public static void SaveToSlotTwo(PlayerProfile profile)
    {
        string slotTwoPath = Application.persistentDataPath + "/SaveGame2.bin";
        Save(slotTwoPath, profile);
    }

    public static void SaveToSlotThree(PlayerProfile profile)
    {
        string slotThreePath = Application.persistentDataPath + "/SaveGame3.bin";
        Save(slotThreePath, profile);
    }

    public static PlayerProfile LoadFromSlotOne()
    {
        string slotOnePath = Application.persistentDataPath + "/SaveGame1.bin";
        PlayerProfile profile = Load(slotOnePath);

        if (profile == null)
        {
            return null;
        }
        else
        {
            return profile;
        }
    }

    public static PlayerProfile LoadFromSlotTwo()
    {
        string slotTwoPath = Application.persistentDataPath + "/SaveGame2.bin";
        PlayerProfile profile = Load(slotTwoPath);

        if (profile == null)
        {
            return null;
        }
        else
        {
            return profile;
        }
    }

    public static PlayerProfile LoadFromSlotThree()
    {
        string slotThreePath = Application.persistentDataPath + "/SaveGame3.bin";
        PlayerProfile profile = Load(slotThreePath);

        if (profile == null)
        {
            return null;
        }
        else
        {
            return profile;
        }
    }

    private static void Save(string slotPath, PlayerProfile profile)
    {
        FileStream fs = new FileStream(slotPath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(fs, profile);
        fs.Close();
    }

    private static PlayerProfile Load(string slotPath)
    {
        FileStream fs = new FileStream(slotPath, FileMode.Open);

        if (File.Exists(slotPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            PlayerProfile profile = bf.Deserialize(fs) as PlayerProfile;
            fs.Close();
            return profile;
        }
        else
        {
            Debug.LogError("Save File not found in " + slotPath);
            return null;
        }
    }
}





