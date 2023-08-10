namespace news24h.Models
{
    public class SavesItem
    {
        public Post _post { get; set; }
        
    }

    public class Saves
    {
        List<SavesItem> _items=new List<SavesItem>();

        public IEnumerable<SavesItem> Items
        {
            get { return _items; }
        }

        public void Add_Post_Save(Post post)
        {
            var item = Items.FirstOrDefault(s=>s._post.Id==post.Id);
            if (item == null)
            {
                _items.Add(new SavesItem { _post = post });
            }
           

        }
    }
}
