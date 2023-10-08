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

public partial class InventoryItem : UserControl
{
    public delegate void ValueChangedHandler(InventoryItem item);
    public event ValueChangedHandler? ValueChanged;

    public InternalGameItem GameItem { get; set; }
    private bool loading;

    public InventoryItem()
    {
        loading = true;
        InitializeComponent();
        GameItem = new();
        ToggleStack();
    }
    public InventoryItem(InternalGameItem item)
    {
        loading = true;
        InitializeComponent();
        GameItem = item;
        ToggleStack();
    }

    public void Update(InternalGameItem item)
    {
        GameItem = item;
        ToggleStack();
        CmbItem.SelectedItem = GameItem.Name;
    }

    public void SetRange(List<InternalGameItem> items, InternalGameItem? selected = null)
    {
        CmbItem.Items.Clear();
        foreach (string item in items.Select(x => x.Name))
        {
            CmbItem.Items.Add(item);
        }
        if (selected != null)
        {
            Update(selected);
        }
    }

    private void InventoryItem_Load(object sender, EventArgs e)
    {
        CmbItem.Items.Clear();
        foreach (string item in GameDAL.ItemNames)
        {
            CmbItem.Items.Add(item);
        }
        CmbItem.SelectedItem = GameItem.Name;
        TxtQty.Text = GameItem.amount.ToString();
        loading = false;
    }

    private void CmbItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (loading) return;
        int qty = GameItem.amount;
        GameItem = GameDAL.GameItems.FirstOrDefault(x => x.Name == CmbItem.SelectedItem.ToString()) ?? GameDAL.GameItems.First();
        GameItem.amount = GameItem.CanStack() ? qty : 1;
        ToggleStack();
        ValueChanged?.Invoke(this);
    }

    private void ToggleStack()
    {
        if (!GameItem.CanStack())
        {
            GameItem.amount = 1;
            TxtQty.Visible = false;
            LblQty.Visible = false;
            RbCase.Visible = false;
            RbNum.Visible = false;
            RbStack.Visible = false;
        }
        else
        {
            LblQty.Visible = true;
            TxtQty.Visible = true;
            RbCase.Visible = true;
            RbNum.Visible = true;
            RbStack.Visible = true;
            TxtQty.Text = GameItem.amount.ToString();
        }
        toolTip1.SetToolTip(CmbItem, GameItem.Description);
    }

    private void Integer_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
        {
            e.Handled = true;
        }
        else
        {
            GameItem.amount = Int32.Parse(string.IsNullOrEmpty(TxtQty.Text) ? "0" : TxtQty.Text);
            ValueChanged?.Invoke(this);
        }
    }

    private void RbType_Click(object sender, EventArgs e)
    {
        if (((RadioButton)sender).Name == "RbNum")
        {
            TxtQty.Enabled = true;
        }
        else if (((RadioButton)sender).Name == "RbStack")
        {
            GameItem.amount = GameItem.MaxStack;
            TxtQty.Text = GameItem.amount.ToString();
            TxtQty.Enabled = false;
            ValueChanged?.Invoke(this);
        }
        else
        {
            GameItem.amount = GameItem.MaxStack * 25;
            TxtQty.Text = GameItem.amount.ToString();
            TxtQty.Enabled = false;
            ValueChanged?.Invoke(this);
        }
    }
}
