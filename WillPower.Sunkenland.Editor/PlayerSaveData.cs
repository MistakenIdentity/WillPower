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
public class PlayerContainer
{
    public PlayerValue PlayerSaveData { get; set; } = new();
    public TypeValue ApplicationVersion { get; set; } = new();
    public TypeValue version { get; set; } = new();

}

public class PlayerSaveData
{
    public WorldPosition lastPresentWorldPosition { get; set; } = new();
    public PlayerStatus playerStatusData { get; set; } = new();
    public Equipment playerEquipmentData { get; set; } = new();
    public InventoryContainer inventoryStorageSaveData { get; set; } = new();
    public InventoryContainer quickSlotStorageSaveData { get; set; } = new();
    public List<int> learnedBlueprints { get; set; } = new();
    public List<int> unlockedItems { get; set; } = new();
    public List<string> discoveredLocations { get; set; } = new();

    public int InventorySpace(BagType? type = null)
    {
        if (type != null)
        {
            inventoryStorageSaveData.maxItemsAmount = (int)type.Value;
        }
        return inventoryStorageSaveData.maxItemsAmount; 
    }

}

public class PlayerStatus
{
    public double health { get; set; }
    public double targetHealth { get; set; }
    public double hunger {  get; set; }
    public double thirst { get; set; }
    public double bodyTemperature { get; set; }
    public double targetBodyTemperature { get; set; }
    public double stamina {  get; set; }
    public double energy { get; set; }
    public double wet { get; set; }

    public void MaxHunger()
        => hunger = 100;
    public void MaxThirst()
        => thirst = 100;
    public void MaxHealth()
        => health = targetHealth;
    public void MaxTemperature()
        => bodyTemperature = targetBodyTemperature;
    public void MaxStamina()
        => stamina = 100;
    public void MaxEnergy()
        => energy = 100;
    public void MaxWet()
        => wet = 100;

    public void MaxAll()
    {
        MaxEnergy();
        MaxHealth();
        MaxHunger();
        MaxStamina();
        MaxTemperature();
        MaxThirst();
        MaxWet();
    }
}

public class Equipment
{
    public GameItem? helmet { get; set; } = new();
    public GameItem? mask { get; set; } = new();
    public GameItem? clothes { get; set; } = new();
    public GameItem? pants { get; set; } = new();
    public GameItem? shoes { get; set; } = new();
    public GameItem? armor { get; set; } = new();
    public GameItem? backpack { get; set; } = new();
    public GameItem? misc1 { get; set; } = new();
    public GameItem? misc2 { get; set; } = new();
    public GameItem? misc3 { get; set; } = new();
    public GameItem? misc4 { get; set; } = new();
    public GameItem? gloves { get; set; } = new();
    public GameItem? rebreather { get; set; } = new();

}

#pragma warning restore IDE1006 // Naming Styles
