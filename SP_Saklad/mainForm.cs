﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Saklad.SpData;
using System.Data.Entity;

namespace SP_Saklad
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            docsUserControl1.OnLoad();
        }
    }

    public enum EditWBOptions
    {
        New = 1,
        Edit = 2
    }
}
