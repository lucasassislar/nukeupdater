using NukeUpdater.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateMaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private ProjectInfo project;

        private void btn_Search_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                if (browser.ShowDialog() == DialogResult.OK)
                {
                    project = new ProjectInfo();
                    project.InitializeServer(browser.SelectedPath);
                    project.Name = "Default";
                    UpdateView();
                }
            }
        }

        private void UpdateView()
        {
            txtFolder.Text = project.Root;
            btnMake.Enabled = true;

            if (project.Created)
            {
                label_Found.Text = "Found Nuke Project";
                label_Found.ForeColor = Color.Green;
            }
            else
            {
                label_Found.Text = "No Nuke Project";
                label_Found.ForeColor = Color.Red;
            }

            UpdateInfoBuilder builder = new UpdateInfoBuilder();
            builder.IgnoreDefault = chkIgnore.Checked;

            if (project.Created)
            {
                UpdateInfo latest = project.ReadUpdate(project.Latest);
                update = builder.MakeUpdate(latest, project.Root);
            }
            else
            {
                project.Make();

                // first update
                update = builder.MakeFirstUpdate(project.Root);
            }

            //listBox1.DataSource = update.Entries;
        }

        private UpdateInfo update;
        private void btnMake_Click(object sender, EventArgs e)
        {
            if (update == null)
            {
                return;
            }

            project.SaveUpdate(update);
            project.Save();
        }

        private void txtProj_TextChanged(object sender, EventArgs e)
        {
            project.Name = txtProj.Text;
        }
    }
}
