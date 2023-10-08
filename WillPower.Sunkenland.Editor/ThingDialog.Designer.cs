namespace WillPower.Sunkenland.Editor
{
    partial class ThingDialog
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
            panel1 = new Panel();
            BtnCancel = new Button();
            BtnOk = new Button();
            panel2 = new Panel();
            ChkAll = new CheckBox();
            LblCaption = new Label();
            PnlItems = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnCancel);
            panel1.Controls.Add(BtnOk);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 376);
            panel1.Name = "panel1";
            panel1.Size = new Size(732, 36);
            panel1.TabIndex = 0;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(538, 7);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(75, 23);
            BtnCancel.TabIndex = 1;
            BtnCancel.Text = "&Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // BtnOk
            // 
            BtnOk.Location = new Point(645, 7);
            BtnOk.Name = "BtnOk";
            BtnOk.Size = new Size(75, 23);
            BtnOk.TabIndex = 0;
            BtnOk.Text = "&OK";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += BtnOk_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(ChkAll);
            panel2.Controls.Add(LblCaption);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(732, 22);
            panel2.TabIndex = 1;
            // 
            // ChkAll
            // 
            ChkAll.AutoSize = true;
            ChkAll.Location = new Point(637, 3);
            ChkAll.Name = "ChkAll";
            ChkAll.Size = new Size(74, 19);
            ChkAll.TabIndex = 1;
            ChkAll.Text = "Select &All";
            ChkAll.UseVisualStyleBackColor = true;
            ChkAll.CheckedChanged += ChkAll_CheckedChanged;
            // 
            // LblCaption
            // 
            LblCaption.AutoSize = true;
            LblCaption.Location = new Point(4, 3);
            LblCaption.Name = "LblCaption";
            LblCaption.Size = new Size(49, 15);
            LblCaption.TabIndex = 0;
            LblCaption.Text = "Caption";
            // 
            // PnlItems
            // 
            PnlItems.AutoScroll = true;
            PnlItems.Dock = DockStyle.Fill;
            PnlItems.Location = new Point(0, 22);
            PnlItems.Name = "PnlItems";
            PnlItems.Size = new Size(732, 354);
            PnlItems.TabIndex = 2;
            // 
            // ThingDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 412);
            Controls.Add(PnlItems);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ThingDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Things";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label LblCaption;
        private Button BtnCancel;
        private Button BtnOk;
        private Panel PnlItems;
        private CheckBox ChkAll;
    }
}