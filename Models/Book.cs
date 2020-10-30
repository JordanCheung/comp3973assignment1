using Assignment1.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class Book
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Authors")]
        public string Authors { get; set; }
        [Display(Name = "Publisher")]
        public string Publisher { get; set; }
        [Display(Name = "Published Date")]
        public string PublishedDate { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "ISBN 10")]
        public string Isbn10 { get; set; }
        [Display(Name = "Image")]
        public string SmallThumbnail { get; set; }
    }
}
