using System;
using System.Collections.Generic;

namespace Inspector
{
    public class GraphModel
    {
        public GraphModel()
        {

        }
        public string Name;
        public string Path;

        public ModelProvider Provider;
        public List<GraphNode> Nodes = new List<GraphNode>();
        public EdgeNode[] Edges;
        public List<GroupNode> Groups = new List<GroupNode>();
    }
}
