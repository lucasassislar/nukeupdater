using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryToCode
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string filename = open.FileName;

                    byte[] data = File.ReadAllBytes(filename);
                    string code = "static byte[] arrei = new byte[] {";
                    for (int i = 0; i < data.Length; i++)
                    {
                        code += data[i] + ",";
                    }
                    code += "}";

                    txtCode.Text = code;

                    Clipboard.SetText(code);
                }
            }
        }
    }
}
