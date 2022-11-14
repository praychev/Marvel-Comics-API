using Website.ComicsReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApplicationService.DTOs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels
{
    public class ComicsVM
    {
        public int idC { get; set; }

        [DisplayName("Title:")]
        [Required(ErrorMessage = "*This field is Required")]
        [StringLength(50,ErrorMessage ="Must contain max 50 characters")]
        public string Title { get; set; }
        [DisplayName("Issue Number:")]
        public int IssueNumber { get; set; }
        [DisplayName("Description:")]
        [StringLength(50, ErrorMessage = "Must contain max 50 characters")]
        public string Description { get; set; }
        [DisplayName("Page count:")]
        public int PageCount { get; set; }

        [DisplayName("ISBN:")]
        [Required(ErrorMessage = "*This field is Required")]
        [StringLength(10, ErrorMessage = "Must contain max 10 characters")]
        public string ISBN { get; set; }

        public ComicsVM() { }

        public ComicsVM(ComicsDTO comicsDto)
        {
            idC = comicsDto.idC;
            Title = comicsDto.Title;
            IssueNumber = comicsDto.IssueNumber;
            Description = comicsDto.Description;
            PageCount = comicsDto.PageCount;
            ISBN = comicsDto.ISBN;
        }
    }
}