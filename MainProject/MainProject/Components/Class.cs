namespace DropList
{
    public class DropList
    {
        public bool ShowDiv { get; set; } = true;
        public bool click()
        {
            ShowDiv =! ShowDiv;
            return ShowDiv;
        }
    }
}
