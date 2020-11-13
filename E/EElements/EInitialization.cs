using E.EObjects;

namespace E.EElements
{
    public class EInitialization : EElement
    {
        public EProperty Prop { get; }

        public EInitialization(string type, string name) : base(name)
        {
            Prop = new EProperty(type, name);
        }
    }
}
