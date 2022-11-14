using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public class CharacterVM
    {
        public int id { get; set; }
        [DisplayName("Name:")]
        [Required(ErrorMessage = "*This field is Required")]
        [StringLength(50, ErrorMessage = "Must contain max 50 characters")]
        public string Name { get; set; }
        [DisplayName("Age:")]
        public int Age { get; set; }
        [DisplayName("Gender:")]
        [StringLength(10, ErrorMessage = "Must contain max 10 characters")]
        public string Gender { get; set; }
        [DisplayName("Description:")]
        [StringLength(250, ErrorMessage = "Must contain max 250 characters")]
        public string Description { get; set; }
        [DisplayName("ComicsId:")]
        public int ComicsId { get; set; }
        [DisplayName("SeriesId:")]
        public int SeriesId { get; set; }
        public ComicsVM Comics { get; set; }
        public SeriesVM Series { get; set; }
    }
}