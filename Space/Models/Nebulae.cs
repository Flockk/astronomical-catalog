#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class Nebulae
    {
        [DisplayName("Туманность")]
        public int NebulaId { get; set; }
        [DisplayName("Созвездие")]
        public int ConsId { get; set; }
        [DisplayName("Галактика")]
        public int? GlxId { get; set; }
        [DisplayName("Название")]
        public string NebulaName { get; set; }
        [DisplayName("Тип")]
        public string NebulaType { get; set; }
        [DisplayName("Прямое восхождение")]
        public TimeSpan? NebulaRightAscension { get; set; }
        [DisplayName("Склонение")]
        public string NebulaDeclination { get; set; }
        [DisplayName("Расстояние")]
        public int? NebulaDistance { get; set; }

        public string NebulaImage { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
    }
}