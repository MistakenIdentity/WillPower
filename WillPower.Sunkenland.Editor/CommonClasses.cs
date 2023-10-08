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
namespace WillPower.Sunkenland.Editor;

#pragma warning disable IDE1006 // Naming Styles

public class TypeValue
{
    public string __type { get; set; } = "string";
    public string value { get; set; } = "0.1.34";//last game version coded against

}

public class PlayerValue
{
    public string __type { get; set; } = "PlayerSaveData,Assembly-CSharp";
    public PlayerSaveData value { get; set; } = new();
}

public class WorldPosition
{
    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }

}

public class InventoryContainer
{
    public int maxItemsAmount { get; set; }
    public List<GameItem?> itemSaveDataArray { get; set; } = new();//50 entries

}

public class GameItem
{
    public int itemID { get; set; }
    public int amount { get; set; } = 1;
    public double condition { get; set; }
    public bool isCombinedWithItem { get; set; }
    public object? itemProperties { get; set; }

}

#pragma warning restore IDE1006 // Naming Styles


