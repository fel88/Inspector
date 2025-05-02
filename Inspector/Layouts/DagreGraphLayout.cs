using Dagre;
using Inspector.Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Inspector.Layouts
{
    public class DagreGraphLayout : GraphLayout
    {
        public override bool FlashHoveredRelatives { get; set; } = false;
        public override bool DrawHeadersAllowed { get; set; } = true;
        public override bool EdgesDrawAllowed { get; set; } = true;

        void updateNodesSizes(GraphModel model)
        {
            foreach (var item in model.Nodes)
            {
                GraphNodeDrawInfo dd = new GraphNodeDrawInfo() { X = 0, Y = 0, Width = 300, Height = 100 };
                item.DrawTag = dd;

                dd.Width = 500;
                if (GetRenderTextWidth != null)
                {
                    dd.Width = Math.Max(20 + GetRenderTextWidth(item), 220);
                }

                dd.Height = 80;
            }
        }


        public void ExperimentalGroupLayout(GraphModel model)
        {
            DagreInputGraph d = new DagreInputGraph();
            d.VerticalLayout = VerticalLayout;
            updateNodesSizes(model);

            model.Nodes = model.Nodes.Where(z => (z.Childs.Any() || z.Parent != null || z.Parents.Any())).ToList();
            model.Groups.Clear();
            var list1 = model.Nodes.ToList();

            foreach (var rgrp in RequestedGroups)
            {


                var group1 = model.Nodes.Where(z => z.Name.StartsWith(rgrp.Prefix)).ToArray();
                //replace group with big rectangle here?


                list1 = list1.Except(group1).ToList();
                var gnode = new GroupNode()
                {
                    Prefix = rgrp.Prefix,
                    Name = "group" + (1 + RequestedGroups.IndexOf(rgrp)),
                    DrawTag = new GraphNodeDrawInfo() { Width = 800, Height = 800 },
                    Nodes = group1.ToArray()
                };

                model.Groups.Add(gnode);
                list1.Add(gnode);

                foreach (var gg in list1)
                {
                    var tag = (gg.DrawTag as GraphNodeDrawInfo);
                    d.AddNode(gg, tag.Rect.Width, tag.Rect.Height);
                }
                foreach (var gg in list1)
                {
                    bool add = false;
                    foreach (var item in gg.Childs)
                    {
                        if (group1.Contains(item))
                        {
                            add = true;
                            break;
                        }
                    }

                    if (add)
                    {
                        gg.AttachChild(gnode);
                        gg.Childs.RemoveAll(z => group1.Contains(z));
                    }
                }
                foreach (var gg in list1)
                {
                    bool add = false;

                    foreach (var item in gg.Parents)
                    {
                        if (group1.Contains(item))
                        {
                            add = true;
                            break;
                        }
                    }
                    if (add)
                    {
                        gg.Parents.Add(gnode);
                        gg.Parents.RemoveAll(z => group1.Contains(z));
                    }
                }
                foreach (var gg in group1)
                {

                    foreach (var item in gg.Childs)
                    {
                        if (!group1.Contains(item))
                        {
                            gnode.Childs.Add(item);
                        }
                    }

                }
            }
            foreach (var gg in list1)
            {
                foreach (var item in gg.Childs)
                {
                    var nd1 = d.GetNode(gg);
                    var nd2 = d.GetNode(item);
                    var minlen = (item.Parents.Count == 0 || item.Childs.Count == 0 || gg.Parents.Count == 0 || gg.Childs.Count == 0) ? 30 : 10;
                    d.AddEdge(nd1, nd2, minlen);
                }
            }

            d.Layout();

            //back
            foreach (var n in model.Nodes.Union(model.Groups))
            {
                var nd = d.GetNode(n);
                if (nd == null) continue;
                var tag = (n.DrawTag as GraphNodeDrawInfo);
                var xx = nd.X;
                var yy = nd.Y;
                tag.X = xx;
                tag.Y = yy;
            }


            List<EdgeNode> enodes = new List<EdgeNode>();
            foreach (var item in d.Edges())
            {
                var pnts = item.Points;
                List<PointF> rr = new List<PointF>();
                foreach (var itemz in pnts)
                {
                    rr.Add(new PointF(itemz.X, itemz.Y));
                }

                enodes.Add(new EdgeNode(rr.ToArray()));
            }
            model.Edges = enodes.ToArray();
        }
        public override void Layout(GraphModel model)
        {
            if (RequestedGroups.Any())
            {
                ExperimentalGroupLayout(model);
                return;
            }
            DagreInputGraph d = new DagreInputGraph();
            d.VerticalLayout = VerticalLayout;
            

            updateNodesSizes(model);
            var temp = model.Nodes.ToList();

            model.Nodes = model.Nodes.Where(z=>z.Childs.Any() || z.Parents.Any()).ToList();
            



            var list1 = model.Nodes.ToList();


            foreach (var gg in list1)
            {
                var tag = (gg.DrawTag as GraphNodeDrawInfo);
                d.AddNode(gg, tag.Rect.Width, tag.Rect.Height);
            }

            foreach (var gg in list1)
            {
                foreach (var item in gg.Childs)
                {
                    var nd1 = d.GetNode(gg);
                    var nd2 = d.GetNode(item);
                    var minlen =
                        5;
                    d.AddEdge(nd1, nd2, minlen);
                }
            }

            try
            {
                d.Layout();
            }
            catch (Exception ex)
            {
                Extensions.ShowError(ex.Message, "Error");
            }

            //back
            foreach (var n in model.Nodes)
            {
                var nd = d.GetNode(n);
                if (nd == null)
                    continue;

                var tag = (n.DrawTag as GraphNodeDrawInfo);
                var xx = nd.X;
                var yy = nd.Y;
                tag.X = xx;
                tag.Y = yy;
            }


            List<EdgeNode> enodes = new List<EdgeNode>();
            foreach (var item in d.Edges())
            {
                var pnts = item.Points;
                if (item.Points == null)
                    continue;

                List<PointF> rr = new List<PointF>();
                foreach (var itemz in pnts)
                {
                    rr.Add(new PointF(itemz.X, itemz.Y));
                }

                enodes.Add(new EdgeNode(rr.ToArray()));
            }
            model.Edges = enodes.ToArray();
            model.Nodes = temp;
        }
    }
}
