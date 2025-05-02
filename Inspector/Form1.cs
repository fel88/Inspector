using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Inspector.Layouts;


namespace Inspector
{
    public partial class Form1 : UserControl
    {
        public Form1()
        {
            InitializeComponent();

            pictureBox1.Visible = false;
            var rc = ctx.GenerateRenderControl() as Control;
            pictureBox1.Parent.Controls.Add(rc);
            rc.ContextMenuStrip = contextMenuStrip1;
            rc.Dock = DockStyle.Fill;
            tableLayoutPanel1.SetRowSpan(rc, 2);
            RenderControl = rc;

            ctx.Init(rc);
            pictureBox1.SetDoubleBuffered(true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            ctx.Redraw = Redraw;

            CurrentLayout = Activator.CreateInstance(DefaultLayout) as GraphLayout;

            pictureBox1.Focus();
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            rc.MouseDown += Rc_MouseDown;
            HideInfoTab();

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                LoadModel(args[1]);
            }
            EdgeNode.DrawingContext = ctx;
            Load += Form1_Load;
        }

        private void Rc_MouseDown(object? sender, MouseEventArgs e)
        {
            //if (hovered == null)
            // return;

            selected = hovered;
            ShowInfoTab();
            UpdateInfo();
        }

        Control RenderControl = null;
        public static Type DefaultLayout = typeof(DagreGraphLayout);
        private void Form1_Load(object sender, EventArgs e)
        {
            mf = new MessageFilter();
            Application.AddMessageFilter(mf);
        }

        MessageFilter mf = null;
        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {


        }


        GraphNode selected = null;

        public Tuple<long[], float[]> ParseTensorFromString(string data)
        {


            long[] dims;
            float[] w;

            Stack<char> s = new Stack<char>();
            int max = 0;
            foreach (var item in data)
            {
                max = Math.Max(s.Count, max);
                if (item == '[')
                {
                    s.Push(item);
                }
                if (item == ']')
                {
                    if (s.Peek() != '[') throw new DataException();
                    s.Pop();
                }
            }
            if (s.Any()) throw new DataException();
            dims = new long[max];
            var temp = new long[max];
            w = data.Split(new char[] { ',', '[', ']', ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(z => float.Parse(z.Replace(",", "."), CultureInfo.InvariantCulture)).ToArray();
            foreach (var item in data)
            {
                max = Math.Max(s.Count, max);
                if (item == '[')
                {
                    s.Push(item);
                }
                if (item == ']')
                {
                    dims[s.Count - 1] = temp[s.Count - 1] + 1;
                    temp[s.Count - 1] = 0; ;
                    if (s.Peek() != '[') throw new DataException();
                    s.Pop();
                }
                if (item == ',') { temp[s.Count - 1]++; }
            }
            return new Tuple<long[], float[]>(dims, w);
        }

        internal void StopDrawThread()
        {
            // exitRequired = true;
            reset.Set();
        }

        public static void Rec(StringBuilder sb, long[] dims, int level, float[] array, long offset, int? lenLimit = null)
        {
            if (lenLimit != null && sb.Length > lenLimit) return;
            if (level == dims.Length - 1)
            {
                sb.Append("[");
                for (int i = 0; i < dims[level]; i++)
                {
                    if (i > 0) sb.Append(", ");
                    sb.Append(array[i + offset]);
                }
                sb.Append("]");
                return;
            }
            sb.Append("[");
            for (int i = 0; i < dims[level]; i++)
            {
                if (i > 0) sb.Append(",");
                Rec(sb, dims, level + 1, array, offset, lenLimit);
                offset += dims[level + 1];
            }
            sb.Append("]");
        }



        public static string GetFormattedArray(InputData data, int? lenLimit = null)
        {
            StringBuilder sb = new StringBuilder();

            Rec(sb, data.Dims, 0, data.Weights, 0, lenLimit);
            return sb.ToString();
        }

        public void UpdateInfo()
        {


        }


        private void ShowInfoTab()
        {

        }
        private void HideInfoTab()
        {

        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (hovered == null)
            // return;

            selected = hovered;
            ShowInfoTab();
            UpdateInfo();
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //pictureBox1.Focus();
        }
        /*public void DrawRoundedRectangle(Graphics g,
                                 RectangleF r, int d, Pen pen)
        {
            g.DrawPath(pen, ctx.GetRoundedRectangle(r, d));
        }
      */

        public void FillRoundedRectangle(IDrawingContext g,
                                RectangleF r, float d, Brush myBrush)
        {
            g.FillPath(myBrush, ctx.GetRoundedRectangle(r, d));
        }

        GraphNode hovered = null;
        Font f = new Font("Arial", 18);
        Font f2 = new Font("Arial", 14);
        Brush textBrush = Brushes.Black;
        private void drawEdges(IDrawingContext ctx)
        {
            if (Model.Edges != null && CurrentLayout.EdgesDrawAllowed)
                foreach (var item in Model.Edges)
                {
                    item.Draw(ctx);
                }
            else
                foreach (var item in Model.Nodes)
                {

                    var dtag = item.DrawTag as GraphNodeDrawInfo;
                    if (dtag == null) continue;
                    foreach (var citem in item.Childs)
                    {
                        var dtag2 = citem.DrawTag as GraphNodeDrawInfo;
                        if (dtag2 == null) continue;
                        var size = 6 * ctx.zoom;
                        AdjustableArrowCap bigArrow = new AdjustableArrowCap(size, size, true);
                        Pen pen1 = new Pen(Color.Black);
                        pen1.CustomEndCap = bigArrow;

                        ctx.DrawLine(pen1,
                            ctx.Transform(dtag.Rect.Location.X + dtag.Rect.Size.Width / 2, dtag.Rect.Location.Y + dtag.Rect.Height / 2),
                            ctx.Transform(dtag2.Rect.Location.X + dtag2.Rect.Size.Width / 2, dtag2.Rect.Location.Y + dtag2.Rect.Height / 2)
                            );
                    }
                }
        }

        bool drawEnabled = true;
        void Redraw()
        {
            if (ParentForm != null)
            {
                bool exit = false;
                ParentForm.Invoke((Action)(() =>
                {
                    if (Program.MainForm.ActiveMdiChild != ParentForm) exit = true;
                }));
                if (exit) return;

            }
            textBrush = Brushes.Black;
            lock (DrawingContext.lock1)
            {
                ctx.Update();

                ctx.Clear(Color.White);
                ctx.AntiAlias(true);

                ctx.ResetTransform();

                //ctx.DrawLine(Pens.Black, ctx.Transform(new PointF(0, 0)), ctx.Transform( new PointF(100, 100)));

                ///axis
                //ctx.Graphics.DrawLine(Pens.Red, ctx.Transform(new PointF(0, 0)), ctx.Transform(new PointF(1000, 0)));
                //ctx.Graphics.DrawLine(Pens.Blue, ctx.Transform(new PointF(0, 0)), ctx.Transform(new PointF(0, 1000)));


                if (Model != null && drawEnabled)
                {
                    drawEdges(ctx);
                    drawNodes(ctx);
                    drawLabels(ctx);
                }

                if (selected != null)
                {
                    ctx.DrawString(selected.FilePath, new Font(SystemFonts.DefaultFont.FontFamily, 12), Brushes.Black, 10, 10);
                }

            }


            pictureBox1.Invoke((Action)(() =>
            {
                ctx.Swap();
                //ctx.Box.Refresh();
            }));
        }

        private void drawNodes(IDrawingContext ctx)
        {

            var list = Model.Nodes.ToList();
            if (Model.Groups.Any())
            {
                list = list.Except(Model.Groups.SelectMany(z => z.Nodes)).ToList();
                list.AddRange(Model.Groups);
            }
            foreach (var item in list)
            {

                var dtag = item.DrawTag as GraphNodeDrawInfo;
                if (dtag == null)
                    continue;

                var rr = ctx.Transform(dtag.Rect);
                var rr2 = ctx.Transform(new RectangleF(dtag.Rect.Left, dtag.Rect.Top, dtag.Rect.Width, dtag.Rect.Height));

                textBrush = Brushes.Black;

                int cornerRadius = (int)(15 * ctx.zoom);
                Brush brush = Brushes.LightGray;
                textBrush = Brushes.White;

                brush = StaticColors.ConvBrush;


                var borderPen = Pens.Black;
                if (item is GroupNode)
                {
                    brush = StaticColors.GroupBrush;
                    borderPen = new Pen(Color.Black, 3);
                    textBrush = Brushes.Black;
                }
                if (item == hovered)
                {
                    textBrush = Brushes.Black;
                    if (CurrentLayout.DrawHeadersAllowed && item.DrawHeader)
                    {
                        RectangleF headerRect = new RectangleF(rr2.Left, rr2.Top, rr2.Width, item.HeaderHeight * ctx.zoom);
                        ctx.FillPath(Brushes.White, ctx.RoundedRect(rr2, cornerRadius));


                    }
                    else
                    {
                        FillRoundedRectangle(ctx, rr2, (40 * ctx.zoom), Brushes.LightYellow);
                    }
                }
                else if (CurrentLayout.FlashHoveredRelatives && item.Parents.Contains(hovered))
                {
                    FillRoundedRectangle(ctx, rr2, (40 * ctx.zoom), Brushes.LightPink);
                }
                else if (CurrentLayout.FlashHoveredRelatives && item.Childs.Contains(hovered))
                {
                    FillRoundedRectangle(ctx, rr2, (40 * ctx.zoom), Brushes.LightBlue);
                }
                else
                if (item == selected)
                {
                    textBrush = Brushes.Black;

                    if (CurrentLayout.DrawHeadersAllowed && item.DrawHeader)
                    {
                        RectangleF headerRect = new RectangleF(rr2.Left, rr2.Top, rr2.Width, item.HeaderHeight * ctx.zoom);
                        ctx.FillPath(Brushes.LightGreen, ctx.RoundedRect(rr2, cornerRadius));

                    }
                    else
                    {
                        FillRoundedRectangle(ctx, rr2, (40 * ctx.zoom), Brushes.LightGreen);
                    }
                }
                else
                {
                    if (CurrentLayout.DrawHeadersAllowed && item.DrawHeader)
                    {
                        RectangleF headerRect = new RectangleF(rr2.Left, rr2.Top, rr2.Width, item.HeaderHeight * ctx.zoom);
                        ctx.FillPath(brush, ctx.RoundedRect(rr2, cornerRadius));



                    }
                    else
                    {
                        ctx.FillPath(brush, ctx.RoundedRect(rr2, cornerRadius));

                        //FillRoundedRectangle(ctx.Graphics, rr2, cornerRadius, brush);
                    }
                }
                ctx.DrawPath(borderPen, ctx.RoundedRect(rr2, cornerRadius));

                //DrawRoundedRectangle(ctx.Graphics, rr, (int)(40 * ctx.zoom), Pens.Black);


                ctx.ResetTransform();
                var sh = ctx.Transform(dtag.Rect.Left, dtag.Rect.Top + 10);
                ctx.TranslateTransform(sh.X, sh.Y);
                ctx.ScaleTransform(ctx.zoom, ctx.zoom);
                //ctx.Graphics.DrawString($"{item.Name}: ({item.OpType})", f, Brushes.Black, 0, 0);

                {
                    var ms = ctx.MeasureString($"{item.Name}", f);
                    ctx.DrawString($"{item.Name}", f, textBrush, +dtag.Rect.Width / 2 - ms.Width / 2, 0);
                }


                if (item is GroupNode gn)
                {
                    var ms = ctx.MeasureString($"Group: {gn.Prefix}", f);
                    ctx.DrawString($"Group: {gn.Prefix}", f, textBrush, +dtag.Rect.Width / 2 - ms.Width / 2, 0);
                    /*ctx.Graphics.DrawPath(new Pen(Color.Blue, 5), Helpers.RoundedRect(new RectangleF(10, 0, 60, 60), (int)(cornerRadius / ctx.Zoom)));
                    ctx.Graphics.DrawLine(new Pen(Color.Blue, 5), 50, 10, 50, 50);
                    ctx.Graphics.DrawLine(new Pen(Color.Blue, 5), 20, 50, 50, 50);*/
                }


                var ms2 = ctx.MeasureString(item.Name, f);
                //ctx.Graphics.DrawString(item.OpType, f, textBrush, +dtag.Rect.Width / 2 - ms2.Width / 2, 30);
                if (CurrentLayout.FlashHoveredRelatives)
                {
                    if (item.Parents.Contains(hovered))
                    {
                        ctx.DrawString("child", f, textBrush, +dtag.Rect.Width / 2 - ms2.Width / 2, 60);
                    }
                    if (item.Childs.Contains(hovered))
                    {
                        ctx.DrawString("parent", f, textBrush, +dtag.Rect.Width / 2 - ms2.Width / 2, 60);
                    }
                }


                ctx.ResetTransform();
            }
        }
        private void drawLabels(IDrawingContext ctx)
        {
            foreach (var item in Model.Nodes)
            {
                var dtag = item.DrawTag as GraphNodeDrawInfo;
                if (dtag == null)
                    continue;

            }
        }


        AutoResetEvent reset = new AutoResetEvent(true);



        IDrawingContext ctx = new DoubleBufferedDrawingContext();
        //IDrawingContext ctx = new SkiaGLDrawingContext();



        public const string WindowCaption = "Inspector";
        private string _lastPath;
        public GraphModel Model;

        public List<string> loadedModels = new List<string>();
        public bool LoadModel(string path, bool _fitAll = true)
        {
            if (ParentForm != null)
                ParentForm.FormClosing += ParentForm_FormClosing;

            _lastPath = path;

            Stopwatch sw = new Stopwatch();
            WaitDialog wd = new WaitDialog();
            timer1.Enabled = false;
            Action loadAct = () =>
            {
                sw.Start();
                var model = LoadFromFile(path);
                Model = model;
                if (!loadedModels.Any(z => z.ToLower() == path.ToLower()))
                {
                    loadedModels.Add(path);
                }


                foreach (var item in model.Nodes)
                {
                    //listView1.Items.Add(new ListViewItem(new string[] { item.Name, ss, item.Output[0] }) { Tag = nodes[i] });
                }

                //var cnt2 = res2.Graph.Output[0].Name;
                //nodes.InsertRange(0, res2.Graph.Input.Select(z => outs[z.Name]));

                updateNodesSizes();
                CurrentLayout.GetRenderTextWidth = renderTextWidth;
                CurrentLayout.Layout(Model);

                //Text = $"{WindowCaption}: {Path.GetFileName(path)}";
                if (ParentForm != null)
                {
                    ParentForm.Invoke((Action)(() =>
                    {
                        ParentForm.Text = Path.GetFileName(path);
                    }));

                }
                drawEnabled = true;
                reset.Set();
                if (_fitAll)
                    fitAll();
                sw.Stop();
            };
            drawEnabled = false;

            wd.Init(loadAct);
            wd.ShowDialog();
            timer1.Enabled = true;
            if (wd.Exception != null)
            {
                Extensions.ShowError(wd.Exception.Message, Program.MainForm.Text);
            }
            Program.MainForm.SetStatusMessage($"Load time: {sw.ElapsedMilliseconds} ms");

            return true;
        }

        private GraphModel LoadFromFile(string path)
        {
            GraphModel ret = new GraphModel();
            List<GraphNode> nodes = new List<GraphNode>();
            var dir = Path.GetDirectoryName(path);
            string[] files =
    Directory.GetFiles(dir, "*.csproj", SearchOption.AllDirectories);
            foreach (var item in files)
            {
                var nn = new GraphNode()
                {
                    Name = Path.GetFileName(item),
                    FilePath = item,
                };

                nodes.Add(nn);
            }

            foreach (var item in files)
            {
                try
                {
                    var node = nodes.First(z => z.Name == Path.GetFileName(item));

                    var doc = XDocument.Load(item);
                    foreach (var ritem in doc.Descendants().Where(z => z.Name.LocalName == "ProjectReference"))
                    {
                        var path1 = ritem.Attribute("Include").Value;
                        var fname = Path.GetFileName(path1);
                        var fr = nodes.First(z => z.Name == fname);
                        fr.AttachChild(node);
                    }
                }
                catch (Exception ex)
                {

                }
            }


            ret.Nodes = nodes.ToList();

            return ret;
        }

        void updateNodesSizes()
        {
            foreach (var item in Model.Nodes)
            {
                GraphNodeDrawInfo dd = new GraphNodeDrawInfo() { X = 0, Y = 0, Width = 800, Height = 60 };
                item.DrawTag = dd;

                item.DrawHeader = true;

            }
        }

        public GraphLayout CurrentLayout;


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ctx.sx = 0;
            ctx.sy = 0;
            ctx.zoom = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var pos = pictureBox1.PointToClient(Cursor.Position);

            GraphNode hovered2 = null;

            if (Model == null) return;

            foreach (var item in Model.Nodes.Union(Model.Groups))
            {
                var dtag = item.DrawTag as GraphNodeDrawInfo;
                if (dtag == null)
                    continue;

                var rr = ctx.Transform(dtag.Rect);
                var rr1 = ctx.GetRoundedRectangle(rr, (int)(40 * ctx.zoom));
                if (rr1.IsVisible(pos))
                {
                    hovered2 = item;
                    break;
                }
            }
            hovered = hovered2;
            //Redraw();

            RenderControl.Invalidate();
        }



        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_lastPath.EndsWith("onnx")) { MessageBox.Show("only onnx model supported", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }



        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            var ar = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (ar == null) return;
            //if (Model == null)
            {
                LoadModel(ar[0]);
                //return;
            }
            /* switch (MessageBox.Show("Load another process?", WindowCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
             {
                 case DialogResult.Yes:
                     Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location, $"\"{ar[0]}\"");
                     break;
                 case DialogResult.No:
                     LoadModel(ar[0]);
                     break;
                 case DialogResult.Cancel:
                     break;
             }*/

        }

        private void fromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        float renderTextWidth(GraphNode item)
        {
            var str = string.Empty;

            str = $"{item.Name}";


            var ms = TextRenderer.MeasureText(str, f);
            //var ms = ctx.Graphics.MeasureString(str, f);
            return ms.Width;
        }

        private void dagreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentLayout = new DagreGraphLayout();
            WaitDialog wd = new WaitDialog();
            drawEnabled = false;
            wd.Init(() =>
            {

                CurrentLayout.GetRenderTextWidth = renderTextWidth;
                CurrentLayout.Layout(Model);
                drawEnabled = true;
            });
            wd.ShowDialog();
            fitAll();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            CurrentLayout.RequestedGroups.Clear();
            LoadModel(ofd.FileName);
            fitAll();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Model == null) { MessageBox.Show("load model first", WindowCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = Model.Provider.SaveDialogFilter;
            if (sfd.ShowDialog() != DialogResult.OK) return;
            Model.Provider.SaveModel(Model, sfd.FileName);
        }




        void edit()
        {
            if (selected == null) return;
            if (selected is GroupNode g)
            {
                if (Extensions.ShowQuestion($"Expand group {g.Prefix}?", ParentForm.Text) == DialogResult.Yes)
                {
                    CurrentLayout.RequestedGroups.RemoveAll(z => z.Prefix == g.Prefix);
                    LoadModel(_lastPath, false);
                    return;
                }
            }
            bool exit = true;

        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            edit();
        }

        void fitAll()
        {
            List<PointF> pp = new List<PointF>();
            foreach (var item in Model.Nodes)
            {
                var dtag = item.DrawTag as GraphNodeDrawInfo;
                pp.Add(dtag.Rect.Location);
                pp.Add(new PointF(dtag.Rect.Right, dtag.Rect.Bottom));
            }

            ctx.FitToPoints(pp.ToArray(), 5);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            fitAll();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ctx.StopDrag();
            edit();
        }




        private void showVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentLayout.VerticalLayout = true;
            CurrentLayout.Layout(Model);
            fitAll();

            //if (File.Exists(_lastPath))
            //{
            //    LoadModel(_lastPath);
            //    fitAll();
            //}
        }

        private void showHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentLayout.VerticalLayout = false;
            CurrentLayout.Layout(Model);
            fitAll();

            //if (File.Exists(_lastPath))
            //{
            //    LoadModel(_lastPath);
            //    fitAll();
            //}
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in selected.Parents)
            {
                item.Childs.Remove(selected);
            }
            Model.Nodes.Remove(selected);
            CurrentLayout.Layout(Model);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            var dtag = selected.DrawTag as GraphNodeDrawInfo;
            if (dtag == null)
                return;

            var d = AutoDialog.DialogHelpers.StartDialog();
            d.AddNumericField("posx", "X", dtag.Rect.X, 100000, -100000);
            d.AddNumericField("posy", "Y", dtag.Rect.Y, 100000, -100000);
            d.AddNumericField("width", "X", dtag.Rect.Width, 100000, -100000);
            d.AddNumericField("height", "X", dtag.Rect.Height, 100000, -100000);

            if (!d.ShowDialog())
                return;

            dtag.X = (float)d.GetNumericField("posx");
            dtag.Y = (float)d.GetNumericField("posy");
            dtag.Width = (float)d.GetNumericField("width");
            dtag.Height = (float)d.GetNumericField("height");

        }
    }
}
