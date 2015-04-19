namespace AngularJSAuthentication.API.Models
{
    public class Item
    {
        public Item() { }

        public Item(string label, long id)
        {
            Label = label;
            Id = id;
        }

        public string Label { get; set; }

        public long Id { get; set; }
    }
}