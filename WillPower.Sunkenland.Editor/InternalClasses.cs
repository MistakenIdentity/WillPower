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

public class InternalGameItem : GameItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaxStack { get; set; }

    public InternalGameItem()
    {
        itemID = -1;
        Name = "-Empty-";
        Description = "Nothing";
        MaxStack = 1;
    }

    public InternalGameItem(string line)
    {
        if (line.Length > 4)
        {
            itemID = Convert.ToInt32(line[..4].Trim());
            Name = line[4..].Trim();
        }
        else
        {
            itemID = Convert.ToInt32(line.Trim());
            Name = line.Trim();
        }
        Description = Name;
        MaxStack = 1;
        condition = 100;
    }

    public InternalGameItem(GameItem? item, string? name = null, int? maxStack = null, string? description = null)
    {
        InternalGameItem gi = item == null ? GameDAL.GameItems.First() : GameDAL.GameItems.FirstOrDefault(x => x.itemID == item.itemID) ?? new();
        MaxStack = maxStack ?? gi.MaxStack;
        Name = name ?? gi.Name;
        Description = description ?? gi.Description;
        itemID = gi.itemID;
        condition = item?.condition ?? gi.condition;
        isCombinedWithItem = item?.isCombinedWithItem ?? gi.isCombinedWithItem;
        amount = item?.amount ?? gi.amount;
        itemProperties = item?.itemProperties ?? gi.itemProperties;
    }

    public bool CanStack() => MaxStack > 1;
    
    public GameItem? ToGameItem() => itemID < 0 ? null : (GameItem)this;
    
}

public class Thing
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
}


