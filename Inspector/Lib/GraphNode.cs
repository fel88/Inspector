namespace Inspector
{
    public class GraphNode : ITag
    {
        public GraphNode()
        {
            lock (lock1)
            {
                Id = NewId++;
            }
        }

        public bool DrawHeader = false;
        public float HeaderHeight = 40;


        public static object lock1 = new object();
        public static long NewId = 0;
        public long Id;
      

        public string Name;
        public string FilePath;
        public List<GraphNode> Childs = new List<GraphNode>();
        public static bool ExceptionOnDuplicateChild = false;
        public void AttachChild(GraphNode child)
        {
            if (Childs.Contains(child))
            {
                if (ExceptionOnDuplicateChild)
                    throw new ArgumentException("duplicate child");
                else
                    return;
            }
            Childs.Add(child);
            child.Parents.Add(this);

        }
        public GraphNode Parent;
        public List<GraphNode> Parents = new List<GraphNode>();



        public List<InputData> Data = new List<InputData>();
        public object Tag { get; set; }
        public object DrawTag { get; set; }
        public string Input { get; internal set; }



    }


}
