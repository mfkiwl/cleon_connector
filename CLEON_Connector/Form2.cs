using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CLEON_Connector
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Form1.ActiveForm.Location.X + Form1.ActiveForm.Size.Width, Form1.ActiveForm.Location.Y);

            InitializeComponent();
        }
    }
}
