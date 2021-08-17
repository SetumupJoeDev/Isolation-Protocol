using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    #region Player
    public static void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath + "/player.data");
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath + "/player.data");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in" + path);
            return null;
        }
    }

    #endregion

    #region Fabricator

    public static void SaveFabricator(FabricatorUpgradeListGenerator droneList, FabricatorUpgradeListGenerator exoSuitList, FabricatorUpgradeListGenerator weaponList)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath + "/fabricator.data");
        FileStream stream = new FileStream(path, FileMode.Create);

        FabricatorData data = new FabricatorData(droneList, exoSuitList, weaponList);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static FabricatorData LoadFabricator()
    {
        string path = Path.Combine(Application.persistentDataPath + "/fabricator.data");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            FabricatorData data = formatter.Deserialize(stream) as FabricatorData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in" + path);
            return null;
        }
    }

    #endregion
}
