using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inspector
{
    public partial class AssembliesSelectorDialog : Form
    {
        public AssembliesSelectorDialog()
        {
            InitializeComponent();
            FormClosing += AssembliesSelectorDialog_FormClosing;
            listView1.ItemChecked += ListView1_ItemChecked;
        }

        List<string> checkedList = new List<string>();
        bool insideUpdating = false;
        private void ListView1_ItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            if (insideUpdating)
                return;

            var str = e.Item.Tag as string;
            if (e.Item.Checked)
            {

                if (!checkedList.Contains(str))
                    checkedList.Add(str);
            }
            else
            {
                if (checkedList.Contains(str))
                    checkedList.Remove(str);
            }
        }

        private void AssembliesSelectorDialog_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Files = checkedList.ToArray();
        }

        public string[] Files { get; internal set; }

        internal void Init(string[] files)
        {
            Files = files;
            UpdateList();
        }

        public void UpdateList()
        {
            listView1.BeginUpdate();
            insideUpdating = true;
            listView1.Items.Clear();
            foreach (string file in Files)
            {
                if (!string.IsNullOrEmpty(filter) && !file.ToLower().Contains(filter.ToLower()))
                    continue;

                var lvi = new ListViewItem(new string[] { Path.GetFileName(file), file }) { Tag = file };
                listView1.Items.Add(lvi);
                if (checkedList.Contains(file))
                    lvi.Checked = true;
            }
            insideUpdating = false;
            listView1.EndUpdate();
        }

        string filter = "";
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filter = textBox1.Text;
            UpdateList();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {            
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Checked = true;
            }
        }
    }
}
