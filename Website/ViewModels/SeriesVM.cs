using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public class SeriesVM
    {
        public int id { get; set; }
        [DisplayName("Title:")]
        [Required(ErrorMessage = "*This field is Required")]
        [StringLength(50, ErrorMessage = "Must contain max 50 characters")]
        public string Title { get; set; }
        [DisplayName("Description:")]
        [StringLength(50, ErrorMessage = "Must contain max 50 characters")]
        public string Description { get; set; }
        [DisplayName("Rating:")]
        public double Rating { get; set; }
        [DisplayName("ComicsId:")]
        public int ComicsId { get; set; }
        public ComicsVM Comics { get; set; }

        public SeriesVM() { }

        public SeriesVM(SeriesDTO seriesDto)
        {
            id= seriesDto.id;
            Title = seriesDto.Title;
            Description = seriesDto.Description;
            Rating = seriesDto.Rating;
            ComicsId = seriesDto.ComicsId;
            Comics = new ComicsVM();
        }

    }
}