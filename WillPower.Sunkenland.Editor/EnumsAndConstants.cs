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

public readonly struct Collections
{
    public static readonly List<int> Armor = new() 
    { -1, 11, 13, 15, 29, 31, 32, 34 };
    public static readonly List<int> Helmets = new() 
    { -1, 12, 14, 16, 27, 28, 30, 33, 35 };
    public static readonly List<int> Clothes = new() 
    { -1, 10, 18, 19, 20, 21, 22, 23, 24, 25, 26, 209, 210, 211, 212, 240, 241, 242
        , 243, 244, 253, 254, 255, 274, 275, 276, 277, 278, 279, 280, 290, 291, 292
        , 297, 298, 299, 300, 301, 302, 303, 304, 311, 312, 313, 327, 328, 329, 330
        , 331, 332, 333, 334, 335, 336, 338, 339 };
    public static readonly List<int> Pants = new() 
    { -1, 17, 213, 227, 228, 229, 230, 231, 235, 236, 237, 238, 239, 260, 261, 262
        , 263, 267, 268, 269, 270, 271, 272, 281, 282, 283, 284, 285, 286, 287, 288
        , 289, 293, 294, 295, 296, 307, 308, 309, 310, 317, 318, 319, 323, 324, 325
        , 326, 337 };
    public static readonly List<int> Masks = new() 
    { -1, 216, 217, 218, 219, 250, 251, 252, 305, 306 };
    public static readonly List<int> Rebreathers = new() 
    { -1, 95, 541  };
    public static readonly List<int> Shoes = new() 
    { -1, 215, 220, 221, 222, 223, 224, 225, 226, 232, 233, 234, 247, 248, 249, 256
        , 257, 258, 259, 264, 265, 266, 273, 314, 315, 316, 320, 321, 322 };
    public static readonly List<int> Gloves = new()
    { -1, 245, 246, 340, 341, 342, 343, 344 };
    public static readonly List<int> Auxilliaries = new()
    { -1, 92, 99, 532 };

    public static int BagID(BagType type)
    {
        return type switch
        {
            BagType.None => -1,
            BagType.Sack => 88,
            BagType.Backpack => 96,
            BagType.MilitaryBackpack => 531,
            _ => -1,
        };
    }
    public static int BagID(int capacity) => BagID((BagType)capacity);
}

public enum ThingType
{
    Blueprint,
    Location
}

public enum BagType
{
    None = 15,
    Sack = 20,
    Backpack = 25,
    MilitaryBackpack = 30
}