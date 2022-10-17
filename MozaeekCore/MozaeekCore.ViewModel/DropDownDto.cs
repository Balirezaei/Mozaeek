namespace MozaeekCore.ViewModel
{
    public class DropDownDto
    {
        public DropDownDto( string value, string text)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}