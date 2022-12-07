using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Space.Models.ViewModels
{
    public class GalaxyGroupsCreateModel
    {
        public GalaxyGroupsCreateModel()
        {
            Galaxies = new HashSet<Galaxies>();
        }

        [DisplayName("Группа галактик")]
        public int GlxgroupId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }

        [Required(ErrorMessage = "Не указано название группы галактик")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string GlxgroupName { get; set; }

        [StringLength(11, ErrorMessage = "Длина строки должна быть до 11 символов")]
        [DisplayName("Тип")]
        public string GlxgroupType { get; set; }
        [DisplayName("Прямое восхождение")]
        public TimeSpan? GlxgroupRightAscension { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Склонение")]
        public string GlxgroupDeclination { get; set; }
        [DisplayName("Красное смещение")]
        public double? GlxgroupRedshift { get; set; }

        public IFormFile GlxgroupImage { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        public virtual ICollection<Galaxies> Galaxies { get; set; }
    }
}
