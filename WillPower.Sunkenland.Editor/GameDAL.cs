//
// ********************************************************************************************************
// ********************************************************************************************************
// ***                                                                                                  ***
// *** Code Copyright © 2023, Will `Willow' Osborn.                                                     ***
// ***                                                                                                  ***
// *** This code is provided 'AS IS, NO WARRANTY' and is intended for no specific use or person.        ***
// *** In fact, the code herein is so confuggled, it should not be used by ANYONE EVER and ANYTHING     ***
// *** that happens as a result of its use is COMPLETELY and UTTERLY YOUR FAULT.  :p                    ***
// ***                                                                                                  ***
// *** You have my permission to extract, copy, modify, steal, borrow, beg, fold, spindle, mutilate or  ***
// *** otherwise abuse the code herein PROVIDED YOU LEAVE ME OUT OF IT! You Acknowledge and Accept      ***
// *** FULL and SOLE responsibility and culpability for ANYTHING you do with or around it.              ***
// ***                                                                                                  ***
// ********************************************************************************************************
// ********************************************************************************************************
//
using System.Text.Json;

namespace WillPower.Sunkenland.Editor;

public class GameDAL
{
    private static GameDataContainer DataContainer
    {
        get
        {
            _dataContainer ??= File.Exists(DataFile) ? JsonSerializer.Deserialize<GameDataContainer>(File.ReadAllText(DataFile)) : JsonSerializer.Deserialize<GameDataContainer>(DataJson);
            return _dataContainer ?? new();
        }
    }
    private static GameDataContainer? _dataContainer = null;
    private static string DataJson
    {
        get => Properties.Resources.GameData;
    }
    private static string DataFile
    {
        get => Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? "", "GameData.json");
    }

    public static List<InternalGameItem> GameItems => DataContainer?.Items ?? new();
    public static List<Thing> Blueprints => DataContainer?.Blueprints ?? new();
    public static List<Thing> Locations => DataContainer?.Locations ?? new();
    public static List<string> ItemNames => GameItems.Select(x => x.Name).OrderBy(x => x).ToList();
    public static List<InternalGameItem> SupplyItems => GameItems.Where(x => x.CanStack()).ToList();
    public static List<InternalGameItem> SingleItems => GameItems.Where(x => !x.CanStack()).ToList();
    public static List<InternalGameItem> Helmets => GameItems.Where(x => Collections.Helmets.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Armor => GameItems.Where(x => Collections.Armor.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Clothes => GameItems.Where(x => Collections.Clothes.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Leggings => GameItems.Where(x => Collections.Pants.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Masks => GameItems.Where(x => Collections.Masks.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Rebreathers => GameItems.Where(x => Collections.Rebreathers.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Gloves => GameItems.Where(x => Collections.Gloves.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Auxilliaries => GameItems.Where(x => Collections.Auxilliaries.Contains(x.itemID)).ToList();
    public static List<InternalGameItem> Shoes => GameItems.Where(x => Collections.Shoes.Contains(x.itemID)).ToList();

    public static string GetName(int id)
        => GameItems.FirstOrDefault(x => x.itemID == id)?.Name ?? Blueprints.FirstOrDefault(x => x.ID == id)?.Name ?? "Not Found";

    public static int GetId(string name)
        => GameItems.FirstOrDefault(x => x.Name == name)?.itemID ?? Blueprints.FirstOrDefault(x => x.Name == name)?.ID ?? 0;

    public static void WritebackGameData()
    {
        File.WriteAllText(DataFile, JsonSerializer.Serialize(DataContainer, typeof(GameDataContainer), new JsonSerializerOptions() { WriteIndented = true }));
    }

}
