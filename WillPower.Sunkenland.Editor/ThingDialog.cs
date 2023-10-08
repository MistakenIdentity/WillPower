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

public partial class ThingDialog : Form
{
    private const int Interval = 176;

    public string Message
    {
        get => LblCaption.Text;
        set => LblCaption.Text = value;
    }

    public string Caption
    {
        get => this.Text;
        set => this.Text = value;
    }

    public List<Thing> SelectedItems { get; private set; }
    public List<Thing> Range { get; private set; }

    public ThingDialog()
    {
        InitializeComponent();
        SelectedItems = new();
        Range = new();
    }

    public DialogResult ShowDialog(IWin32Window owner, string message, List<Thing> range, string? caption = null, List<Thing>? selected = null)
    {
        Message = message;
        if (!string.IsNullOrWhiteSpace(caption))
        {
            Caption = caption;
        }
        SelectedItems = selected ?? new();
        Range = range;
        PnlItems.Controls.Clear();
        int top = 3, left = 6;
        foreach (Thing t in range)
        {
            PnlItems.Controls.Add(new CheckBox() { Top = top, Left = left, Width = Interval, Text = t.Name, Checked = SelectedItems.Any(x => x.Name == t.Name) });
            left += Interval;
            if (left > Interval * 4)
            {
                top += 36;
                left = 6;
            }
        }
        return this.ShowDialog(owner);
    }

    private void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (CheckBox c in PnlItems.Controls)
        {
            c.Checked = ChkAll.Checked;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        SelectedItems.Clear();
        foreach (CheckBox c in PnlItems.Controls)
        {
            if (c.Checked)
            {
                SelectedItems.Add(Range.First(x => x.Name == c.Text));
            }
        }
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
