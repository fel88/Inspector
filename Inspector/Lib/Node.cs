

namespace Inspector
{
    public class Node
    {
        public int Id;
        public static int NewId;
        public object Tag;
        public List<NodePin> Inputs = new List<NodePin>();
        public List<NodePin> Outputs = new List<NodePin>();
        public string Name { get; set; }

        public Node()
        {
            Id = NewId++;
        }

     
   

       
        public Exception LastException;
       
    }
}


