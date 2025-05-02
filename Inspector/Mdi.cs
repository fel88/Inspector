

using Inspector.Layouts;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inspector
{
    public partial class Mdi : Form
    {
        public Mdi()
        {
            InitializeComponent();

            try
            {
                LoadSettings();
            }
            catch (Exception ex)
            {
                Extensions.ShowError(ex.Message, Text);
            }
        }

        private void LoadSettings()
        {
            if (!File.Exists("settings.xml")) return;
            XDocument doc = XDocument.Load("settings.xml");
            foreach (var item in doc.Descendants("setting"))
            {
                var nm = item.Attribute("name").Value;
                var vl = item.Attribute("value").Value;
                switch (nm)
                {
                    case "layout":
                        if (vl == "dagre")
                        {
                            Form1.DefaultLayout = typeof(DagreGraphLayout);
                        }
                        break;

                }
            }

        }

        public void SetStatusMessage(string str)
        {
            toolStripStatusLabel1.Text = str;
        }


        public const string WindowCaption = "Inspector";
        public Form GenerateChildForm(Control cntr)
        {
            var frm = new Form();
            frm.Size = new Size(700, 500);
            //frm.MinimumSize = new Size(700, 500);

            frm.Controls.Add(cntr);
            cntr.Dock = DockStyle.Fill;

            frm.MdiParent = this;

            frm.FormClosing += (x, y) =>
            {
                foreach (var item in frm.Controls)
                {
                    if (item is Form1 f)
                    {
                        f.StopDrawThread();
                    }
                }
            };
            frm.Shown += (x, y) =>
            {
                foreach (var item in frm.Controls)
                {
                    if (item is Form1 f)
                    {

                    }
                }

            };
            return frm;
        }


        private void Mdi_DragDrop(object sender, DragEventArgs e)
        {
            var ar = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (ar == null || ar.Length < 1) return;
            LoadModel(ar[0]);

        }

        public void LoadModel(string ar)
        {
            //if (IsMdiContainer)
            {
                var f1 = new Form1();
                var frm = GenerateChildForm(f1);
                if (!f1.LoadModel(ar))
                {
                    return;
                }
                if (MdiChildren.Length == 1)
                {
                    frm.WindowState = FormWindowState.Maximized;
                }
                frm.Show();

            }
            /* else
             {
                 foreach (var item in Controls)
                 {
                     if (item is Form1 uc)
                     {
                         uc.LoadModel(ar);
                     }
                 }
             }*/
        }



        private void Mdi_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csproj files (*.csproj)|*.csproj|All files (*.*)|*.*";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            LoadModel(ofd.FileName);
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);

        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);

        }



        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in MdiChildren)
            {
                item.Close();
            }
        }

        private void windowsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var t = sender as ToolStripMenuItem;
            t.DropDownItems.Clear();
            foreach (var item in MdiChildren)
            {
                var t1 = new ToolStripMenuItem() { Tag = item, Text = item.Text };
                t.DropDownItems.Add(t1);
                t1.Click += T1_Click;
            }
        }

        private void T1_Click(object sender, EventArgs e)
        {
            ((sender as ToolStripItem).Tag as Form).Activate();
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }




        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

            
        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose any file from directory";

            ofd.Filter = "All files (*.*)|*.*";


            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            LoadModel(ofd.FileName);
        }
    }
}
