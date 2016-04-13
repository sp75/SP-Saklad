﻿namespace SP_Sklad.MainTabs
{
    partial class PayDocUserControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayDocUserControl));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.CurrEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.SumEdit = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.PersonEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.CashEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.PTypeComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.NumEdit = new DevExpress.XtraEditors.TextEdit();
            this.ExecPayCheckBox = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SumEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PersonEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PTypeComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecPayCheckBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.CurrEdit);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.SumEdit);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.PersonEdit);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.CashEdit);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.simpleButton4);
            this.panelControl1.Controls.Add(this.PTypeComboBox);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.NumEdit);
            this.panelControl1.Controls.Add(this.ExecPayCheckBox);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(394, 213);
            this.panelControl1.TabIndex = 0;
            // 
            // CurrEdit
            // 
            this.CurrEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrEdit.Enabled = false;
            this.CurrEdit.Location = new System.Drawing.Point(290, 173);
            this.CurrEdit.Name = "CurrEdit";
            this.CurrEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CurrEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Назва")});
            this.CurrEdit.Properties.DisplayMember = "NAME";
            this.CurrEdit.Properties.ShowFooter = false;
            this.CurrEdit.Properties.ShowHeader = false;
            this.CurrEdit.Properties.ValueMember = "WID";
            this.CurrEdit.Size = new System.Drawing.Size(58, 22);
            this.CurrEdit.StyleController = this.styleController1;
            this.CurrEdit.TabIndex = 26;
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(235, 176);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(49, 16);
            this.labelControl6.StyleController = this.styleController1;
            this.labelControl6.TabIndex = 25;
            this.labelControl6.Text = "Валюта:";
            // 
            // SumEdit
            // 
            this.SumEdit.Location = new System.Drawing.Point(114, 173);
            this.SumEdit.Name = "SumEdit";
            this.SumEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SumEdit.Size = new System.Drawing.Size(104, 22);
            this.SumEdit.StyleController = this.styleController1;
            this.SumEdit.TabIndex = 24;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 176);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(34, 16);
            this.labelControl5.StyleController = this.styleController1;
            this.labelControl5.TabIndex = 23;
            this.labelControl5.Text = "Сума:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Location = new System.Drawing.Point(354, 108);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(22, 22);
            this.simpleButton2.TabIndex = 22;
            // 
            // PersonEdit
            // 
            this.PersonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PersonEdit.Location = new System.Drawing.Point(114, 143);
            this.PersonEdit.Name = "PersonEdit";
            this.PersonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PersonEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name6")});
            this.PersonEdit.Properties.DisplayMember = "Name";
            this.PersonEdit.Properties.ShowFooter = false;
            this.PersonEdit.Properties.ShowHeader = false;
            this.PersonEdit.Properties.ValueMember = "KaId";
            this.PersonEdit.Size = new System.Drawing.Size(234, 22);
            this.PersonEdit.StyleController = this.styleController1;
            this.PersonEdit.TabIndex = 21;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 145);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 20;
            this.labelControl4.Text = "Через кого:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Location = new System.Drawing.Point(354, 143);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(22, 22);
            this.simpleButton1.TabIndex = 19;
            // 
            // CashEdit
            // 
            this.CashEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CashEdit.Location = new System.Drawing.Point(114, 108);
            this.CashEdit.Name = "CashEdit";
            this.CashEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CashEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name5")});
            this.CashEdit.Properties.DisplayMember = "Name";
            this.CashEdit.Properties.ShowFooter = false;
            this.CashEdit.Properties.ShowHeader = false;
            this.CashEdit.Properties.ValueMember = "CashId";
            this.CashEdit.Size = new System.Drawing.Size(234, 22);
            this.CashEdit.StyleController = this.styleController1;
            this.CashEdit.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 110);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "Каса:";
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton4.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.Image")));
            this.simpleButton4.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton4.Location = new System.Drawing.Point(354, 75);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(22, 22);
            this.simpleButton4.TabIndex = 16;
            // 
            // PTypeComboBox
            // 
            this.PTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PTypeComboBox.Location = new System.Drawing.Point(114, 75);
            this.PTypeComboBox.Name = "PTypeComboBox";
            this.PTypeComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PTypeComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name4")});
            this.PTypeComboBox.Properties.DisplayMember = "Name";
            this.PTypeComboBox.Properties.ShowFooter = false;
            this.PTypeComboBox.Properties.ShowHeader = false;
            this.PTypeComboBox.Properties.ValueMember = "PTypeId";
            this.PTypeComboBox.Size = new System.Drawing.Size(234, 22);
            this.PTypeComboBox.StyleController = this.styleController1;
            this.PTypeComboBox.TabIndex = 15;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 78);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Вид оплати:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Номер платежу:";
            // 
            // NumEdit
            // 
            this.NumEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumEdit.Location = new System.Drawing.Point(114, 41);
            this.NumEdit.Name = "NumEdit";
            this.NumEdit.Size = new System.Drawing.Size(104, 22);
            this.NumEdit.StyleController = this.styleController1;
            this.NumEdit.TabIndex = 5;
            // 
            // ExecPayCheckBox
            // 
            this.ExecPayCheckBox.Location = new System.Drawing.Point(12, 8);
            this.ExecPayCheckBox.Name = "ExecPayCheckBox";
            this.ExecPayCheckBox.Properties.Caption = "Оплатити документ";
            this.ExecPayCheckBox.Properties.ValueChecked = 1;
            this.ExecPayCheckBox.Properties.ValueUnchecked = 0;
            this.ExecPayCheckBox.Size = new System.Drawing.Size(203, 20);
            this.ExecPayCheckBox.StyleController = this.styleController1;
            this.ExecPayCheckBox.TabIndex = 4;
            this.ExecPayCheckBox.CheckedChanged += new System.EventHandler(this.ExecPayCheckBox_CheckedChanged);
            // 
            // PayDocUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "PayDocUserControl";
            this.Size = new System.Drawing.Size(394, 213);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SumEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PersonEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PTypeComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExecPayCheckBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit ExecPayCheckBox;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit NumEdit;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit PTypeComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LookUpEdit PersonEdit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LookUpEdit CashEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.LookUpEdit CurrEdit;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CalcEdit SumEdit;

    }
}