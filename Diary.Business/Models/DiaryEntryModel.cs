﻿using System.ComponentModel.DataAnnotations;

namespace Diary.Business.Models
{
    public class DiaryEntryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Please enter your entry content")]
        public required string Content { get; set; }

        [Required(ErrorMessage = "Please enter a date")]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
