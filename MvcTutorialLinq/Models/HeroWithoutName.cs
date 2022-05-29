namespace MvcTutorialLinq.Models
{
    public class HeroWithoutName
    {
        public int Id { get; set; }
        public int Atk { get; set; }
        public int Hp { get; set; }
    }
    public class HeroName
    {
        public int HeroId { get; set; }
        public string Name { get; set; } = null!;
    }
    public class HeroWithName
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Atk { get; set; }
        public int Hp { get; set; }
    }
}
