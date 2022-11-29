#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Space.Models
{
    public partial class BlackHoles
    {
        [DisplayName("Чёрная дыра")]
        public int BlackHoleId { get; set; }
        [DisplayName("Созвездие")]
        public int ConsId { get; set; }
        [DisplayName("Галактика")]
        public int? GlxId { get; set; }
        [DisplayName("Название")]
        public string BlackholeName { get; set; }
        [DisplayName("Тип")]
        public string BlackholeType { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Прямое восхождение")]
        public TimeSpan? BlackholeRightAscension { get; set; }
        [DisplayName("Склонение")]
        public string BlackholeDeclination { get; set; }
        [DisplayName("Расстояние (кпк)")]
        public double? BlackholeDistance { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
    }
}