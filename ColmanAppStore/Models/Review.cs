﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ColmanAppStore.Models
{
    public class Review
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter title")]
        public string Title { get; set; }

        public string Body  { get; set; }


        [Required(ErrorMessage = "You must enter raiting")]
        public float Raiting { get; set; }

        public DateTime PublishDate { get; set; }

        public User UserName { get; set; }






    }
}
