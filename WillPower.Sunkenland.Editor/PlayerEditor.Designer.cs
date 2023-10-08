namespace WillPower.Sunkenland.Editor
{
    partial class PlayerEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            InternalGameItem internalGameItem1 = new InternalGameItem();
            InternalGameItem internalGameItem2 = new InternalGameItem();
            InternalGameItem internalGameItem3 = new InternalGameItem();
            InternalGameItem internalGameItem4 = new InternalGameItem();
            InternalGameItem internalGameItem5 = new InternalGameItem();
            InternalGameItem internalGameItem6 = new InternalGameItem();
            InternalGameItem internalGameItem7 = new InternalGameItem();
            InternalGameItem internalGameItem8 = new InternalGameItem();
            InternalGameItem internalGameItem9 = new InternalGameItem();
            InternalGameItem internalGameItem10 = new InternalGameItem();
            InternalGameItem internalGameItem11 = new InternalGameItem();
            InternalGameItem internalGameItem12 = new InternalGameItem();
            InternalGameItem internalGameItem13 = new InternalGameItem();
            InternalGameItem internalGameItem14 = new InternalGameItem();
            InternalGameItem internalGameItem15 = new InternalGameItem();
            InternalGameItem internalGameItem16 = new InternalGameItem();
            InternalGameItem internalGameItem17 = new InternalGameItem();
            InternalGameItem internalGameItem18 = new InternalGameItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerEditor));
            CmbChars = new ComboBox();
            CmbWorlds = new ComboBox();
            groupBox1 = new GroupBox();
            TxtZ = new TextBox();
            label5 = new Label();
            TxtY = new TextBox();
            label4 = new Label();
            TxtX = new TextBox();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            TxtWet = new TextBox();
            TxtEnergy = new TextBox();
            TxtStamina = new TextBox();
            TxtTemp = new TextBox();
            TxtThirst = new TextBox();
            TxtHunger = new TextBox();
            TxtHealth = new TextBox();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            tips = new ToolTip(components);
            BtnReset = new Button();
            BtnSave = new Button();
            GrpQuickslots = new GroupBox();
            QS6 = new InventoryItem();
            QS5 = new InventoryItem();
            QS4 = new InventoryItem();
            QS3 = new InventoryItem();
            QS2 = new InventoryItem();
            QS1 = new InventoryItem();
            LblName = new TextBox();
            GrpEquipment = new GroupBox();
            label20 = new Label();
            IRebreather = new InventoryItem();
            label19 = new Label();
            IGloves = new InventoryItem();
            label18 = new Label();
            IShoes = new InventoryItem();
            label17 = new Label();
            IArmor = new InventoryItem();
            label16 = new Label();
            IPants = new InventoryItem();
            IClothes = new InventoryItem();
            label15 = new Label();
            IMask = new InventoryItem();
            label14 = new Label();
            IHelmet = new InventoryItem();
            label13 = new Label();
            groupBox3 = new GroupBox();
            IAux4 = new InventoryItem();
            IAux3 = new InventoryItem();
            IAux2 = new InventoryItem();
            IAux1 = new InventoryItem();
            groupBox4 = new GroupBox();
            PnlInventory = new Panel();
            groupBox5 = new GroupBox();
            CmbBag = new ComboBox();
            BtnBlueprints = new Button();
            BtnLocations = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            GrpQuickslots.SuspendLayout();
            GrpEquipment.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // CmbChars
            // 
            CmbChars.FormattingEnabled = true;
            CmbChars.Location = new Point(72, 8);
            CmbChars.Name = "CmbChars";
            CmbChars.Size = new Size(157, 23);
            CmbChars.TabIndex = 0;
            CmbChars.SelectedIndexChanged += CmbChars_SelectedIndexChanged;
            // 
            // CmbWorlds
            // 
            CmbWorlds.FormattingEnabled = true;
            CmbWorlds.Location = new Point(284, 8);
            CmbWorlds.Name = "CmbWorlds";
            CmbWorlds.Size = new Size(158, 23);
            CmbWorlds.TabIndex = 1;
            CmbWorlds.SelectedIndexChanged += CmbWorlds_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TxtZ);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(TxtY);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(TxtX);
            groupBox1.Controls.Add(label3);
            groupBox1.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.Location = new Point(5, 52);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(273, 49);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Location";
            // 
            // TxtZ
            // 
            TxtZ.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtZ.Location = new Point(202, 16);
            TxtZ.Name = "TxtZ";
            TxtZ.Size = new Size(60, 20);
            TxtZ.TabIndex = 5;
            TxtZ.KeyPress += Double_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(179, 19);
            label5.Name = "label5";
            label5.Size = new Size(17, 14);
            label5.TabIndex = 4;
            label5.Text = "Z:";
            // 
            // TxtY
            // 
            TxtY.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtY.Location = new Point(113, 16);
            TxtY.Name = "TxtY";
            TxtY.Size = new Size(60, 20);
            TxtY.TabIndex = 3;
            TxtY.KeyPress += Double_KeyPress;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(90, 19);
            label4.Name = "label4";
            label4.Size = new Size(17, 14);
            label4.TabIndex = 2;
            label4.Text = "Y:";
            // 
            // TxtX
            // 
            TxtX.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtX.Location = new Point(24, 16);
            TxtX.Name = "TxtX";
            TxtX.Size = new Size(60, 20);
            TxtX.TabIndex = 1;
            TxtX.KeyPress += Double_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(18, 14);
            label3.TabIndex = 0;
            label3.Text = "X:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 11);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 3;
            label1.Text = "Character:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(236, 11);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 4;
            label2.Text = "World:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(TxtWet);
            groupBox2.Controls.Add(TxtEnergy);
            groupBox2.Controls.Add(TxtStamina);
            groupBox2.Controls.Add(TxtTemp);
            groupBox2.Controls.Add(TxtThirst);
            groupBox2.Controls.Add(TxtHunger);
            groupBox2.Controls.Add(TxtHealth);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.Location = new Point(8, 107);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(125, 230);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Stats";
            // 
            // TxtWet
            // 
            TxtWet.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtWet.Location = new Point(58, 195);
            TxtWet.Name = "TxtWet";
            TxtWet.Size = new Size(60, 20);
            TxtWet.TabIndex = 13;
            TxtWet.KeyPress += Double_KeyPress;
            // 
            // TxtEnergy
            // 
            TxtEnergy.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtEnergy.Location = new Point(58, 166);
            TxtEnergy.Name = "TxtEnergy";
            TxtEnergy.Size = new Size(60, 20);
            TxtEnergy.TabIndex = 12;
            TxtEnergy.KeyPress += Double_KeyPress;
            // 
            // TxtStamina
            // 
            TxtStamina.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtStamina.Location = new Point(58, 137);
            TxtStamina.Name = "TxtStamina";
            TxtStamina.Size = new Size(60, 20);
            TxtStamina.TabIndex = 11;
            TxtStamina.KeyPress += Double_KeyPress;
            // 
            // TxtTemp
            // 
            TxtTemp.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtTemp.Location = new Point(58, 108);
            TxtTemp.Name = "TxtTemp";
            TxtTemp.Size = new Size(60, 20);
            TxtTemp.TabIndex = 10;
            TxtTemp.KeyPress += Double_KeyPress;
            // 
            // TxtThirst
            // 
            TxtThirst.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtThirst.Location = new Point(58, 79);
            TxtThirst.Name = "TxtThirst";
            TxtThirst.Size = new Size(60, 20);
            TxtThirst.TabIndex = 9;
            TxtThirst.KeyPress += Double_KeyPress;
            // 
            // TxtHunger
            // 
            TxtHunger.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtHunger.Location = new Point(58, 50);
            TxtHunger.Name = "TxtHunger";
            TxtHunger.Size = new Size(60, 20);
            TxtHunger.TabIndex = 8;
            TxtHunger.KeyPress += Double_KeyPress;
            // 
            // TxtHealth
            // 
            TxtHealth.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtHealth.Location = new Point(58, 21);
            TxtHealth.Name = "TxtHealth";
            TxtHealth.Size = new Size(60, 20);
            TxtHealth.TabIndex = 7;
            TxtHealth.KeyPress += Double_KeyPress;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(24, 198);
            label12.Name = "label12";
            label12.Size = new Size(29, 14);
            label12.TabIndex = 6;
            label12.Text = "Wet:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(9, 169);
            label11.Name = "label11";
            label11.Size = new Size(43, 14);
            label11.TabIndex = 5;
            label11.Text = "Energy:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(2, 140);
            label10.Name = "label10";
            label10.Size = new Size(47, 14);
            label10.TabIndex = 4;
            label10.Text = "Stamina:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(16, 111);
            label9.Name = "label9";
            label9.Size = new Size(37, 14);
            label9.TabIndex = 3;
            label9.Text = "Temp:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(16, 82);
            label8.Name = "label8";
            label8.Size = new Size(39, 14);
            label8.TabIndex = 2;
            label8.Text = "Thirst:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(5, 53);
            label7.Name = "label7";
            label7.Size = new Size(43, 14);
            label7.TabIndex = 1;
            label7.Text = "Hunger:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(10, 24);
            label6.Name = "label6";
            label6.Size = new Size(41, 14);
            label6.TabIndex = 0;
            label6.Text = "Health:";
            // 
            // BtnReset
            // 
            BtnReset.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnReset.Location = new Point(460, 7);
            BtnReset.Name = "BtnReset";
            BtnReset.Size = new Size(75, 23);
            BtnReset.TabIndex = 6;
            BtnReset.Text = "Reset";
            BtnReset.UseVisualStyleBackColor = true;
            BtnReset.Click += BtnReset_Click;
            // 
            // BtnSave
            // 
            BtnSave.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnSave.Location = new Point(541, 7);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(75, 23);
            BtnSave.TabIndex = 7;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // GrpQuickslots
            // 
            GrpQuickslots.Controls.Add(QS6);
            GrpQuickslots.Controls.Add(QS5);
            GrpQuickslots.Controls.Add(QS4);
            GrpQuickslots.Controls.Add(QS3);
            GrpQuickslots.Controls.Add(QS2);
            GrpQuickslots.Controls.Add(QS1);
            GrpQuickslots.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            GrpQuickslots.Location = new Point(7, 351);
            GrpQuickslots.Name = "GrpQuickslots";
            GrpQuickslots.Size = new Size(825, 74);
            GrpQuickslots.TabIndex = 8;
            GrpQuickslots.TabStop = false;
            GrpQuickslots.Text = "Quickslots";
            // 
            // QS6
            // 
            internalGameItem1.amount = 0;
            internalGameItem1.condition = 0D;
            internalGameItem1.Description = "";
            internalGameItem1.isCombinedWithItem = false;
            internalGameItem1.itemID = 0;
            internalGameItem1.MaxStack = 1;
            internalGameItem1.Name = "";
            QS6.GameItem = internalGameItem1;
            QS6.Location = new Point(687, 22);
            QS6.Name = "QS6";
            QS6.Size = new Size(131, 46);
            QS6.TabIndex = 5;
            // 
            // QS5
            // 
            internalGameItem2.amount = 0;
            internalGameItem2.condition = 0D;
            internalGameItem2.Description = "";
            internalGameItem2.isCombinedWithItem = false;
            internalGameItem2.itemID = 0;
            internalGameItem2.MaxStack = 1;
            internalGameItem2.Name = "";
            QS5.GameItem = internalGameItem2;
            QS5.Location = new Point(550, 22);
            QS5.Name = "QS5";
            QS5.Size = new Size(131, 49);
            QS5.TabIndex = 4;
            // 
            // QS4
            // 
            internalGameItem3.amount = 0;
            internalGameItem3.condition = 0D;
            internalGameItem3.Description = "";
            internalGameItem3.isCombinedWithItem = false;
            internalGameItem3.itemID = 0;
            internalGameItem3.MaxStack = 1;
            internalGameItem3.Name = "";
            QS4.GameItem = internalGameItem3;
            QS4.Location = new Point(414, 21);
            QS4.Name = "QS4";
            QS4.Size = new Size(131, 49);
            QS4.TabIndex = 3;
            // 
            // QS3
            // 
            internalGameItem4.amount = 0;
            internalGameItem4.condition = 0D;
            internalGameItem4.Description = "";
            internalGameItem4.isCombinedWithItem = false;
            internalGameItem4.itemID = 0;
            internalGameItem4.MaxStack = 1;
            internalGameItem4.Name = "";
            QS3.GameItem = internalGameItem4;
            QS3.Location = new Point(278, 21);
            QS3.Name = "QS3";
            QS3.Size = new Size(131, 49);
            QS3.TabIndex = 2;
            // 
            // QS2
            // 
            internalGameItem5.amount = 0;
            internalGameItem5.condition = 0D;
            internalGameItem5.Description = "";
            internalGameItem5.isCombinedWithItem = false;
            internalGameItem5.itemID = 0;
            internalGameItem5.MaxStack = 1;
            internalGameItem5.Name = "";
            QS2.GameItem = internalGameItem5;
            QS2.Location = new Point(141, 21);
            QS2.Name = "QS2";
            QS2.Size = new Size(131, 49);
            QS2.TabIndex = 1;
            // 
            // QS1
            // 
            internalGameItem6.amount = 0;
            internalGameItem6.condition = 0D;
            internalGameItem6.Description = "";
            internalGameItem6.isCombinedWithItem = false;
            internalGameItem6.itemID = 0;
            internalGameItem6.MaxStack = 1;
            internalGameItem6.Name = "";
            QS1.GameItem = internalGameItem6;
            QS1.Location = new Point(5, 21);
            QS1.Name = "QS1";
            QS1.Size = new Size(131, 49);
            QS1.TabIndex = 0;
            // 
            // LblName
            // 
            LblName.BackColor = SystemColors.Control;
            LblName.BorderStyle = BorderStyle.None;
            LblName.Location = new Point(367, 52);
            LblName.Multiline = true;
            LblName.Name = "LblName";
            LblName.Size = new Size(225, 38);
            LblName.TabIndex = 9;
            // 
            // GrpEquipment
            // 
            GrpEquipment.Controls.Add(label20);
            GrpEquipment.Controls.Add(IRebreather);
            GrpEquipment.Controls.Add(label19);
            GrpEquipment.Controls.Add(IGloves);
            GrpEquipment.Controls.Add(label18);
            GrpEquipment.Controls.Add(IShoes);
            GrpEquipment.Controls.Add(label17);
            GrpEquipment.Controls.Add(IArmor);
            GrpEquipment.Controls.Add(label16);
            GrpEquipment.Controls.Add(IPants);
            GrpEquipment.Controls.Add(IClothes);
            GrpEquipment.Controls.Add(label15);
            GrpEquipment.Controls.Add(IMask);
            GrpEquipment.Controls.Add(label14);
            GrpEquipment.Controls.Add(IHelmet);
            GrpEquipment.Controls.Add(label13);
            GrpEquipment.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            GrpEquipment.Location = new Point(836, 23);
            GrpEquipment.Name = "GrpEquipment";
            GrpEquipment.Size = new Size(145, 402);
            GrpEquipment.TabIndex = 10;
            GrpEquipment.TabStop = false;
            GrpEquipment.Text = "Equipment";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(5, 349);
            label20.Name = "label20";
            label20.Size = new Size(57, 14);
            label20.TabIndex = 15;
            label20.Text = "Rebreather";
            // 
            // IRebreather
            // 
            internalGameItem7.amount = 0;
            internalGameItem7.condition = 0D;
            internalGameItem7.Description = "";
            internalGameItem7.isCombinedWithItem = false;
            internalGameItem7.itemID = 0;
            internalGameItem7.MaxStack = 1;
            internalGameItem7.Name = "";
            IRebreather.GameItem = internalGameItem7;
            IRebreather.Location = new Point(6, 366);
            IRebreather.Name = "IRebreather";
            IRebreather.Size = new Size(130, 26);
            IRebreather.TabIndex = 14;
            IRebreather.ValueChanged += EquipmentChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(6, 302);
            label19.Name = "label19";
            label19.Size = new Size(38, 14);
            label19.TabIndex = 13;
            label19.Text = "Gloves";
            // 
            // IGloves
            // 
            internalGameItem8.amount = 0;
            internalGameItem8.condition = 0D;
            internalGameItem8.Description = "";
            internalGameItem8.isCombinedWithItem = false;
            internalGameItem8.itemID = 0;
            internalGameItem8.MaxStack = 1;
            internalGameItem8.Name = "";
            IGloves.GameItem = internalGameItem8;
            IGloves.Location = new Point(5, 318);
            IGloves.Name = "IGloves";
            IGloves.Size = new Size(131, 28);
            IGloves.TabIndex = 12;
            IGloves.ValueChanged += EquipmentChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(5, 254);
            label18.Name = "label18";
            label18.Size = new Size(33, 14);
            label18.TabIndex = 11;
            label18.Text = "Shoes";
            // 
            // IShoes
            // 
            internalGameItem9.amount = 0;
            internalGameItem9.condition = 0D;
            internalGameItem9.Description = "";
            internalGameItem9.isCombinedWithItem = false;
            internalGameItem9.itemID = 0;
            internalGameItem9.MaxStack = 1;
            internalGameItem9.Name = "";
            IShoes.GameItem = internalGameItem9;
            IShoes.Location = new Point(5, 271);
            IShoes.Name = "IShoes";
            IShoes.Size = new Size(131, 28);
            IShoes.TabIndex = 10;
            IShoes.ValueChanged += EquipmentChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 206);
            label17.Name = "label17";
            label17.Size = new Size(38, 14);
            label17.TabIndex = 9;
            label17.Text = "Armor";
            // 
            // IArmor
            // 
            internalGameItem10.amount = 0;
            internalGameItem10.condition = 0D;
            internalGameItem10.Description = "";
            internalGameItem10.isCombinedWithItem = false;
            internalGameItem10.itemID = 0;
            internalGameItem10.MaxStack = 1;
            internalGameItem10.Name = "";
            IArmor.GameItem = internalGameItem10;
            IArmor.Location = new Point(5, 223);
            IArmor.Name = "IArmor";
            IArmor.Size = new Size(131, 28);
            IArmor.TabIndex = 8;
            IArmor.ValueChanged += EquipmentChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 159);
            label16.Name = "label16";
            label16.Size = new Size(33, 14);
            label16.TabIndex = 7;
            label16.Text = "Pants";
            // 
            // IPants
            // 
            internalGameItem11.amount = 0;
            internalGameItem11.condition = 0D;
            internalGameItem11.Description = "";
            internalGameItem11.isCombinedWithItem = false;
            internalGameItem11.itemID = 0;
            internalGameItem11.MaxStack = 1;
            internalGameItem11.Name = "";
            IPants.GameItem = internalGameItem11;
            IPants.Location = new Point(5, 175);
            IPants.Name = "IPants";
            IPants.Size = new Size(131, 28);
            IPants.TabIndex = 6;
            IPants.ValueChanged += EquipmentChanged;
            // 
            // IClothes
            // 
            internalGameItem12.amount = 0;
            internalGameItem12.condition = 0D;
            internalGameItem12.Description = "";
            internalGameItem12.isCombinedWithItem = false;
            internalGameItem12.itemID = 0;
            internalGameItem12.MaxStack = 1;
            internalGameItem12.Name = "";
            IClothes.GameItem = internalGameItem12;
            IClothes.Location = new Point(5, 128);
            IClothes.Name = "IClothes";
            IClothes.Size = new Size(131, 28);
            IClothes.TabIndex = 5;
            IClothes.ValueChanged += EquipmentChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 111);
            label15.Name = "label15";
            label15.Size = new Size(42, 14);
            label15.TabIndex = 4;
            label15.Text = "Clothes";
            // 
            // IMask
            // 
            internalGameItem13.amount = 0;
            internalGameItem13.condition = 0D;
            internalGameItem13.Description = "";
            internalGameItem13.isCombinedWithItem = false;
            internalGameItem13.itemID = 0;
            internalGameItem13.MaxStack = 1;
            internalGameItem13.Name = "";
            IMask.GameItem = internalGameItem13;
            IMask.Location = new Point(6, 80);
            IMask.Name = "IMask";
            IMask.Size = new Size(131, 28);
            IMask.TabIndex = 3;
            IMask.ValueChanged += EquipmentChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(5, 64);
            label14.Name = "label14";
            label14.Size = new Size(32, 14);
            label14.TabIndex = 2;
            label14.Text = "Mask";
            // 
            // IHelmet
            // 
            internalGameItem14.amount = 0;
            internalGameItem14.condition = 0D;
            internalGameItem14.Description = "";
            internalGameItem14.isCombinedWithItem = false;
            internalGameItem14.itemID = 0;
            internalGameItem14.MaxStack = 1;
            internalGameItem14.Name = "";
            IHelmet.GameItem = internalGameItem14;
            IHelmet.Location = new Point(5, 33);
            IHelmet.Name = "IHelmet";
            IHelmet.Size = new Size(131, 28);
            IHelmet.TabIndex = 1;
            IHelmet.ValueChanged += EquipmentChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 17);
            label13.Name = "label13";
            label13.Size = new Size(41, 14);
            label13.TabIndex = 0;
            label13.Text = "Helmet";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(IAux4);
            groupBox3.Controls.Add(IAux3);
            groupBox3.Controls.Add(IAux2);
            groupBox3.Controls.Add(IAux1);
            groupBox3.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.Location = new Point(283, 52);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(552, 49);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "Miscellaneous";
            // 
            // IAux4
            // 
            internalGameItem15.amount = 0;
            internalGameItem15.condition = 0D;
            internalGameItem15.Description = "";
            internalGameItem15.isCombinedWithItem = false;
            internalGameItem15.itemID = 0;
            internalGameItem15.MaxStack = 1;
            internalGameItem15.Name = "";
            IAux4.GameItem = internalGameItem15;
            IAux4.Location = new Point(414, 17);
            IAux4.Name = "IAux4";
            IAux4.Size = new Size(130, 22);
            IAux4.TabIndex = 7;
            IAux4.ValueChanged += EquipmentChanged;
            // 
            // IAux3
            // 
            internalGameItem16.amount = 0;
            internalGameItem16.condition = 0D;
            internalGameItem16.Description = "";
            internalGameItem16.isCombinedWithItem = false;
            internalGameItem16.itemID = 0;
            internalGameItem16.MaxStack = 1;
            internalGameItem16.Name = "";
            IAux3.GameItem = internalGameItem16;
            IAux3.Location = new Point(278, 16);
            IAux3.Name = "IAux3";
            IAux3.Size = new Size(130, 22);
            IAux3.TabIndex = 6;
            IAux3.ValueChanged += EquipmentChanged;
            // 
            // IAux2
            // 
            internalGameItem17.amount = 0;
            internalGameItem17.condition = 0D;
            internalGameItem17.Description = "";
            internalGameItem17.isCombinedWithItem = false;
            internalGameItem17.itemID = 0;
            internalGameItem17.MaxStack = 1;
            internalGameItem17.Name = "";
            IAux2.GameItem = internalGameItem17;
            IAux2.Location = new Point(142, 17);
            IAux2.Name = "IAux2";
            IAux2.Size = new Size(130, 22);
            IAux2.TabIndex = 5;
            IAux2.ValueChanged += EquipmentChanged;
            // 
            // IAux1
            // 
            internalGameItem18.amount = 0;
            internalGameItem18.condition = 0D;
            internalGameItem18.Description = "";
            internalGameItem18.isCombinedWithItem = false;
            internalGameItem18.itemID = 0;
            internalGameItem18.MaxStack = 1;
            internalGameItem18.Name = "";
            IAux1.GameItem = internalGameItem18;
            IAux1.Location = new Point(6, 17);
            IAux1.Name = "IAux1";
            IAux1.Size = new Size(130, 24);
            IAux1.TabIndex = 4;
            IAux1.ValueChanged += EquipmentChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(PnlInventory);
            groupBox4.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.Location = new Point(136, 107);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(694, 246);
            groupBox4.TabIndex = 12;
            groupBox4.TabStop = false;
            groupBox4.Text = "Inventory";
            // 
            // PnlInventory
            // 
            PnlInventory.AutoScroll = true;
            PnlInventory.Dock = DockStyle.Fill;
            PnlInventory.Location = new Point(3, 16);
            PnlInventory.Name = "PnlInventory";
            PnlInventory.Size = new Size(688, 227);
            PnlInventory.TabIndex = 0;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(CmbBag);
            groupBox5.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox5.Location = new Point(652, 7);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(144, 47);
            groupBox5.TabIndex = 13;
            groupBox5.TabStop = false;
            groupBox5.Text = "Bag Type";
            // 
            // CmbBag
            // 
            CmbBag.FormattingEnabled = true;
            CmbBag.Location = new Point(6, 16);
            CmbBag.Name = "CmbBag";
            CmbBag.Size = new Size(132, 22);
            CmbBag.TabIndex = 0;
            CmbBag.SelectedIndexChanged += CmbBag_SelectedIndexChanged;
            // 
            // BtnBlueprints
            // 
            BtnBlueprints.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnBlueprints.Location = new Point(460, 36);
            BtnBlueprints.Name = "BtnBlueprints";
            BtnBlueprints.Size = new Size(75, 23);
            BtnBlueprints.TabIndex = 14;
            BtnBlueprints.Text = "Blueprints";
            BtnBlueprints.UseVisualStyleBackColor = true;
            BtnBlueprints.Click += BtnBlueprints_Click;
            // 
            // BtnLocations
            // 
            BtnLocations.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLocations.Location = new Point(541, 36);
            BtnLocations.Name = "BtnLocations";
            BtnLocations.Size = new Size(75, 23);
            BtnLocations.TabIndex = 15;
            BtnLocations.Text = "Locations";
            BtnLocations.UseVisualStyleBackColor = true;
            BtnLocations.Click += BtnLocations_Click;
            // 
            // PlayerEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 429);
            Controls.Add(BtnLocations);
            Controls.Add(BtnBlueprints);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(GrpEquipment);
            Controls.Add(LblName);
            Controls.Add(GrpQuickslots);
            Controls.Add(BtnSave);
            Controls.Add(BtnReset);
            Controls.Add(groupBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(CmbWorlds);
            Controls.Add(CmbChars);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PlayerEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Character Editor";
            Load += PlayerEditor_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            GrpQuickslots.ResumeLayout(false);
            GrpEquipment.ResumeLayout(false);
            GrpEquipment.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox CmbChars;
        private ComboBox CmbWorlds;
        private GroupBox groupBox1;
        private TextBox TxtX;
        private Label label3;
        private Label label1;
        private Label label2;
        private TextBox TxtY;
        private Label label4;
        private TextBox TxtZ;
        private Label label5;
        private GroupBox groupBox2;
        private Label label7;
        private Label label6;
        private Label label8;
        private Label label9;
        private TextBox TxtHealth;
        private Label label12;
        private Label label11;
        private Label label10;
        private TextBox TxtWet;
        private TextBox TxtEnergy;
        private TextBox TxtStamina;
        private TextBox TxtTemp;
        private TextBox TxtThirst;
        private TextBox TxtHunger;
        private ToolTip tips;
        private Button BtnReset;
        private Button BtnSave;
        private GroupBox GrpQuickslots;
        private InventoryItem QS2;
        private InventoryItem QS1;
        private InventoryItem QS6;
        private InventoryItem QS5;
        private InventoryItem QS4;
        private InventoryItem QS3;
        private TextBox LblName;
        private GroupBox GrpEquipment;
        private InventoryItem IClothes;
        private Label label15;
        private InventoryItem IMask;
        private Label label14;
        private InventoryItem IHelmet;
        private Label label13;
        private Label label16;
        private InventoryItem IPants;
        private Label label17;
        private InventoryItem IArmor;
        private Label label18;
        private InventoryItem IShoes;
        private Label label19;
        private InventoryItem IGloves;
        private Label label20;
        private InventoryItem IRebreather;
        private GroupBox groupBox3;
        private InventoryItem IAux4;
        private InventoryItem IAux3;
        private InventoryItem IAux2;
        private InventoryItem IAux1;
        private GroupBox groupBox4;
        private Panel PnlInventory;
        private GroupBox groupBox5;
        private ComboBox CmbBag;
        private Button BtnBlueprints;
        private Button BtnLocations;
    }
}