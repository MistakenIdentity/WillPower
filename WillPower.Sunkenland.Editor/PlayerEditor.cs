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
using System.Text.RegularExpressions;

namespace WillPower.Sunkenland.Editor;

public partial class PlayerEditor : Form
{
    private const string GuidRegex = "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$";

    private readonly string path;

    private string CharactersPath => Path.Combine(path, "Characters");
    private string WorldsPath => Path.Combine(path, "Worlds");

    private readonly List<string> Characters;
    private readonly List<string> Worlds;

    private PlayerSaveData Character => CharacterFile?.PlayerSaveData.value ?? new();
    private PlayerContainer? CharacterFile;
    private string? FileName;

    public PlayerEditor(string sunkenlandSavePath)
    {
        InitializeComponent();
        path = sunkenlandSavePath;
        Characters = new();
        Worlds = new();
        foreach (string s in Directory.GetDirectories(CharactersPath))
        {
            Characters.Add(s.Split('\\').Last());
        }
        foreach (string s in Directory.GetDirectories(WorldsPath))
        {
            Worlds.Add(s.Split('\\').Last());
        }
    }

    private void PlayerEditor_Load(object sender, EventArgs e)
    {
        this.SuspendLayout();
        CmbBag.Items.Clear();
        foreach (int x in Enum.GetValues(typeof(BagType)))
        {
            CmbBag.Items.Add((BagType)x);
        }
        IHelmet.SetRange(GameDAL.Helmets);
        IMask.SetRange(GameDAL.Masks);
        IClothes.SetRange(GameDAL.Clothes);
        IPants.SetRange(GameDAL.Leggings);
        IArmor.SetRange(GameDAL.Armor);
        IShoes.SetRange(GameDAL.Shoes);
        IGloves.SetRange(GameDAL.Gloves);
        IRebreather.SetRange(GameDAL.Rebreathers);
        IAux1.SetRange(GameDAL.Auxilliaries);
        IAux2.SetRange(GameDAL.Auxilliaries);
        IAux3.SetRange(GameDAL.Auxilliaries);
        IAux4.SetRange(GameDAL.Auxilliaries);
        if (Characters.Count < 1)
        {
            ShowMessage("No characters found for editing.");
        }
        else
        {
            CmbChars.Items.Clear();
            foreach (string s in Characters)
            {
                CmbChars.Items.Add(s.Split('~').First());
            }
        }
        this.ResumeLayout(true);
    }

    private DialogResult ShowMessage(string message, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
    {
        return MessageBox.Show(this, message, "Sunkenland Editor", buttons, icon);
    }

    private string GetWorldName(string name)
    {
        string key = name.Split('~').Last();
        if (!Worlds.Any(x => x.Split('~').Last() == key))
        {
            return key;
        }
        return Worlds.First(x => x.Split('~').Last() == key).Split('~').First();
    }

    private string GetFileName(string name)
    {
        if (!Worlds.Any(x => x.StartsWith($"{name}~")))
        {
            return Path.Combine(CharacterPath(), $"{name}.json");
        }
        string key = Worlds.First(x => x.Split('~')[0] == name).Split('~')[1];
        return Path.Combine(CharacterPath(), $"{key}.json");
    }

    private string CharacterPath()
    {
        return Path.Combine(CharactersPath, Characters.First(x => x.StartsWith($"{CmbChars.Items[CmbChars.SelectedIndex]}~")));
    }

    private void CmbChars_SelectedIndexChanged(object sender, EventArgs e)
    {
        CmbWorlds.Items.Clear();
        string[] files = Directory.GetFiles(Path.Combine(CharactersPath, Characters.First(x => x.StartsWith($"{CmbChars.Items[CmbChars.SelectedIndex]}~"))), "*.json");
        List<FileInfo> files2 = new();
        foreach (string s in files)
        {
            Match mat = Regex.Match(Path.GetFileNameWithoutExtension(s), GuidRegex);
            if (mat.Success)
            {
                files2.Add(new(s));
            }
        }
        foreach (FileInfo f in files2.OrderByDescending(x => x.LastWriteTime))
        {
            CmbWorlds.Items.Add(GetWorldName(Path.GetFileNameWithoutExtension(f.Name)));
        }
    }

    private void CmbWorlds_SelectedIndexChanged(object sender, EventArgs e)
    {
        string file = GetFileName(CmbWorlds.Items[CmbWorlds.SelectedIndex].ToString() ?? "");
        if (File.Exists(file))
        {
            LoadFile(file);
        }
    }

    private void LoadFile(string fileName)
    {
        FileName = fileName;
        PlayerContainer? play = JsonSerializer.Deserialize<PlayerContainer>(File.ReadAllText(FileName));
        if (play != null)
        {
            this.SuspendLayout();
            CharacterFile = play;
            LblName.Text = $"{CmbChars.Text} in {CmbWorlds.Text}";
            //location - WorldPosition
            TxtX.Text = Character.lastPresentWorldPosition.x.ToString();
            TxtY.Text = Character.lastPresentWorldPosition.y.ToString();
            TxtZ.Text = Character.lastPresentWorldPosition.z.ToString();
            //status - PlayerStatus
            TxtEnergy.Text = Character.playerStatusData.energy.ToString();
            TxtHealth.Text = Character.playerStatusData.health.ToString();
            TxtHunger.Text = Character.playerStatusData.hunger.ToString();
            TxtStamina.Text = Character.playerStatusData.stamina.ToString();
            TxtTemp.Text = Character.playerStatusData.bodyTemperature.ToString();
            TxtThirst.Text = Character.playerStatusData.thirst.ToString();
            TxtWet.Text = Character.playerStatusData.wet.ToString();
            //equipment - GameItem[]
            IHelmet.Update(new(Character.playerEquipmentData.helmet ?? new()));
            IMask.Update(new(Character.playerEquipmentData.mask ?? new()));
            IClothes.Update(new(Character.playerEquipmentData.clothes ?? new()));
            IPants.Update(new(Character.playerEquipmentData.pants ?? new()));
            IArmor.Update(new(Character.playerEquipmentData.armor ?? new()));
            IShoes.Update(new(Character.playerEquipmentData.shoes ?? new()));
            IGloves.Update(new(Character.playerEquipmentData.gloves ?? new()));
            IRebreather.Update(new(Character.playerEquipmentData.rebreather ?? new()));
            IAux1.Update(new(Character.playerEquipmentData.misc1 ?? new()));
            IAux2.Update(new(Character.playerEquipmentData.misc2 ?? new()));
            IAux3.Update(new(Character.playerEquipmentData.misc3 ?? new()));
            IAux4.Update(new(Character.playerEquipmentData.misc4 ?? new()));
            //inventory - InventoryContainer
            CmbBag.SelectedItem = (BagType)Character.InventorySpace();
            //quick slots - InventoryContainer
            QS1.Update(new(Character.quickSlotStorageSaveData.itemSaveDataArray[0]));
            QS2.Update(new(Character.quickSlotStorageSaveData.itemSaveDataArray[1]));
            QS3.Update(new(Character.quickSlotStorageSaveData.itemSaveDataArray[2]));
            QS4.Update(new(Character.quickSlotStorageSaveData.itemSaveDataArray[3]));
            QS5.Update(new(Character.quickSlotStorageSaveData.itemSaveDataArray[4]));
            QS6.Update(new(Character.quickSlotStorageSaveData.itemSaveDataArray[5]));
            //unlocked items - Thing[]
            this.ResumeLayout(true);
        }
    }

    private void SetInventory()
    {
        const int leftIncrement = 152;
        foreach (InventoryItem ii in PnlInventory.Controls)
        {
            ii.ValueChanged -= InventoryChanged;
        }
        PnlInventory.Controls.Clear();
        int top = 0, left = 0;
        for (int i = 0; i < Character.InventorySpace(); i++)
        {
            InventoryItem item = new(new(Character.inventoryStorageSaveData.itemSaveDataArray[i] ?? GameDAL.GameItems.First()))
            {
                Name = $"Inv{i}",
                Height = 46,
                Width = leftIncrement,
                Top = top,
                Left = left,
            };
            item.ValueChanged += InventoryChanged;
            PnlInventory.Controls.Add(item);
            left += leftIncrement;
            if (left > (4 * leftIncrement))
            {
                left = 0;
                top += 46;
            }
        }
    }

    private void EquipmentChanged(InventoryItem item)
    {
        switch (item.Name)
        {
            case "IHelmet":
                Character.playerEquipmentData.helmet = item.GameItem.ToGameItem();
                break;
            //TODO:
        }
    }

    private void InventoryChanged(InventoryItem item)
    {
        int idx = int.Parse(item.Name.Replace("Inv", ""));
        Character.inventoryStorageSaveData.itemSaveDataArray[idx] = item.GameItem.ToGameItem();
    }

    private void Save()
    {
        if (CharacterFile != null)
        {
            Character.lastPresentWorldPosition.x = double.Parse(TxtX.Text);
            Character.lastPresentWorldPosition.y = double.Parse(TxtY.Text);
            Character.lastPresentWorldPosition.z = double.Parse(TxtZ.Text);
            Character.playerStatusData.energy = double.Parse(TxtEnergy.Text);
            Character.playerStatusData.health = double.Parse(TxtHealth.Text);
            Character.playerStatusData.bodyTemperature = double.Parse(TxtTemp.Text);
            Character.playerStatusData.hunger = double.Parse(TxtHunger.Text);
            Character.playerStatusData.stamina = double.Parse(TxtStamina.Text);
            Character.playerStatusData.thirst = double.Parse(TxtThirst.Text);
            Character.playerStatusData.wet = double.Parse(TxtWet.Text);
            Character.quickSlotStorageSaveData.itemSaveDataArray[0] = QS1.GameItem.ToGameItem();
            Character.quickSlotStorageSaveData.itemSaveDataArray[1] = QS2.GameItem.ToGameItem();
            Character.quickSlotStorageSaveData.itemSaveDataArray[2] = QS3.GameItem.ToGameItem();
            Character.quickSlotStorageSaveData.itemSaveDataArray[3] = QS4.GameItem.ToGameItem();
            Character.quickSlotStorageSaveData.itemSaveDataArray[4] = QS5.GameItem.ToGameItem();
            Character.quickSlotStorageSaveData.itemSaveDataArray[5] = QS6.GameItem.ToGameItem();
            SaveCharacterFile();
        }
    }

    private void SaveCharacterFile()
    {
        if (CharacterFile != null && !string.IsNullOrWhiteSpace(FileName))
        {
            string file = Path.Combine(CharacterPath(), $"{Path.GetFileNameWithoutExtension(FileName)}.backup.{DateTime.Now:yyyyMMddhhmmss}.json");
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.Move(FileName, file);
            File.WriteAllText(FileName, 
                JsonSerializer.Serialize(CharacterFile, typeof(PlayerContainer), new JsonSerializerOptions() { WriteIndented = true }).Replace("[]", "{}"));
            ShowMessage($"File saved and backed up at {file}");
        }
    }

    private void Double_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
        {
            e.Handled = true;
        }
    }

    private void Integer_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!Char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void BtnReset_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(FileName))
        {
            LoadFile(FileName);
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        Save();
    }

    private void BtnBlueprints_Click(object sender, EventArgs e)
    {
        using ThingDialog dialog = new();
        if (dialog.ShowDialog(this,
            "Select Known Blueprints:",
            GameDAL.Blueprints,
            "Blueprints",
            Character.learnedBlueprints.Select(x => new Thing()
            {
                ID = x,
                Name = GameDAL.Blueprints.First(y => y.ID == x).Name
            })
            .ToList()) == DialogResult.OK)
        {
            Character.learnedBlueprints = dialog.SelectedItems.Select(x => x.ID).ToList();
        }
    }

    private void BtnLocations_Click(object sender, EventArgs e)
    {
        using ThingDialog dialog = new();
        if (dialog.ShowDialog(this,
            "Select Known Locations:",
            GameDAL.Locations,
            "Locations",
            Character.discoveredLocations.Select(x => new Thing()
            {
                ID = GameDAL.Locations.First(y => y.Name == x).ID,
                Name = x
            })
            .ToList()) == DialogResult.OK)
        {
            Character.discoveredLocations = dialog.SelectedItems.Select(x => x.Name).ToList();
        }
    }

    private void CmbBag_SelectedIndexChanged(object sender, EventArgs e)
    {
        Character.InventorySpace((BagType)CmbBag.SelectedItem);
        SetInventory();
    }
}
