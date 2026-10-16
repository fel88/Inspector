namespace Inspector.Extensions
{
    public static class DialogsHelpers
    {
        public static void ShowError(this string msg, string caption) => MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static bool ShowQuestion(this string msg, string caption) => MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        public static DialogResult ShowQuestion(this string msg, string caption, MessageBoxButtons buttons) => MessageBox.Show(msg, caption, buttons, MessageBoxIcon.Question);

        public static void ShowInfo(string msg, string caption) => MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
