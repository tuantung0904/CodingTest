using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingTest.Models
{
    public class ToDoModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "DueDate is required")]
        //[Range(typeof(DateTime),DateTime.Now.ToString(), null)]
        public DateTime DueDate { get; set; }

        public int DayConstraint { get; set; }
    }
}
