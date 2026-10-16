namespace Inspector.Extensions
{

    public static class AllExtensions
    {
     
        public static byte[] ReadFully(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
      
        public static bool AllowParallelProcessing = false;

        public static void SetStyle(this TextBox textBox, TextBoxStyle style)
        {
            switch (style)
            {
                case TextBoxStyle.Error:
                    textBox.BackColor = Color.Red;
                    textBox.ForeColor = Color.White;
                    break;
                case TextBoxStyle.Default:
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.Black;
                    break;
                case TextBoxStyle.Warning:
                    textBox.BackColor = Color.Yellow;
                    textBox.ForeColor = Color.Blue;
                    break;
            }
        }
    }
}
