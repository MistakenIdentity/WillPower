namespace WillPower.Sunkenland.Editor
{
    partial class InventoryItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            CmbItem = new ComboBox();
            TxtQty = new TextBox();
            LblQty = new Label();
            RbNum = new RadioButton();
            RbStack = new RadioButton();
            RbCase = new RadioButton();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // CmbItem
            // 
            CmbItem.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbItem.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            CmbItem.FormattingEnabled = true;
            CmbItem.Location = new Point(0, 0);
            CmbItem.Name = "CmbItem";
            CmbItem.Size = new Size(151, 22);
            CmbItem.TabIndex = 0;
            CmbItem.SelectedIndexChanged += CmbItem_SelectedIndexChanged;
            // 
            // TxtQty
            // 
            TxtQty.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            TxtQty.Location = new Point(31, 22);
            TxtQty.Name = "TxtQty";
            TxtQty.Size = new Size(26, 20);
            TxtQty.TabIndex = 1;
            TxtQty.KeyPress += Integer_KeyPress;
            // 
            // LblQty
            // 
            LblQty.AutoSize = true;
            LblQty.Font = new Font("Times New Roman", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            LblQty.Location = new Point(2, 25);
            LblQty.Name = "LblQty";
            LblQty.Size = new Size(28, 14);
            LblQty.TabIndex = 2;
            LblQty.Text = "Qty:";
            // 
            // RbNum
            // 
            RbNum.AutoSize = true;
            RbNum.CheckAlign = ContentAlignment.TopLeft;
            RbNum.Checked = true;
            RbNum.FlatStyle = FlatStyle.Popup;
            RbNum.Font = new Font("Times New Roman", 7F, FontStyle.Regular, GraphicsUnit.Point);
            RbNum.Location = new Point(60, 24);
            RbNum.Name = "RbNum";
            RbNum.Size = new Size(27, 16);
            RbNum.TabIndex = 3;
            RbNum.TabStop = true;
            RbNum.Text = "#";
            RbNum.UseVisualStyleBackColor = true;
            RbNum.CheckedChanged += RbType_Click;
            // 
            // RbStack
            // 
            RbStack.AutoSize = true;
            RbStack.CheckAlign = ContentAlignment.TopLeft;
            RbStack.FlatStyle = FlatStyle.Popup;
            RbStack.Font = new Font("Times New Roman", 7F, FontStyle.Regular, GraphicsUnit.Point);
            RbStack.Location = new Point(87, 24);
            RbStack.Name = "RbStack";
            RbStack.Size = new Size(36, 16);
            RbStack.TabIndex = 4;
            RbStack.TabStop = true;
            RbStack.Text = "Stk";
            RbStack.UseVisualStyleBackColor = true;
            RbStack.CheckedChanged += RbType_Click;
            // 
            // RbCase
            // 
            RbCase.AutoSize = true;
            RbCase.CheckAlign = ContentAlignment.TopLeft;
            RbCase.FlatStyle = FlatStyle.Popup;
            RbCase.Font = new Font("Times New Roman", 7F, FontStyle.Regular, GraphicsUnit.Point);
            RbCase.Location = new Point(117, 24);
            RbCase.Name = "RbCase";
            RbCase.Size = new Size(33, 16);
            RbCase.TabIndex = 5;
            RbCase.TabStop = true;
            RbCase.Text = "Cs";
            RbCase.UseVisualStyleBackColor = true;
            RbCase.CheckedChanged += RbType_Click;
            // 
            // InventoryItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(RbCase);
            Controls.Add(RbStack);
            Controls.Add(RbNum);
            Controls.Add(LblQty);
            Controls.Add(TxtQty);
            Controls.Add(CmbItem);
            Name = "InventoryItem";
            Size = new Size(153, 43);
            Load += InventoryItem_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox CmbItem;
        private TextBox TxtQty;
        private Label LblQty;
        private RadioButton RbNum;
        private RadioButton RbStack;
        private RadioButton RbCase;
        private ToolTip toolTip1;
    }
}
