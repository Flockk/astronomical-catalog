#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Space.Models
{
    public partial class GalaxyClusters
    {
        public GalaxyClusters()
        {
            Galaxies = new HashSet<Galaxies>();
        }

        [DisplayName("Скопление галактик")]
        public int GlxclusterId { get; set; }
        [DisplayName("Созвездие")]
        public int ConsId { get; set; }

        [Remote("IsGlxclusterNameExist", "GalaxyClusters", ErrorMessage = "Данное название скопления галактик уже существует!")]
        [Required(ErrorMessage = "Не указано название скопления галактик")]
        [StringLength(30, ErrorMessage = "Длина строки должна быть до 30 символов")]
        [DisplayName("Название")]
        public string GlxclusterName { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Тип")]
        public string GlxclusterType { get; set; }

        [DisplayName("Прямое восхождение")]
        public TimeSpan? GlxclusterRightAscension { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Склонение")]
        public string GlxclusterDeclination { get; set; }

        [DisplayName("Красное смещение")]
        public double? GlxclusterRedshift { get; set; }

        public string GlxclusterImage { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }

		public virtual ICollection<Galaxies> Galaxies { get; set; }
    }
}