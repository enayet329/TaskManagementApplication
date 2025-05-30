using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models
{
	public class TaskItem
	{
		public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		[StringLength(200)]
		public string Title { get; set; }

		[StringLength(1000)]
		public string? Description { get; set; }

		public bool IsCompleted { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DueDate { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		// Navigation property
		public IdentityUser User { get; set; }
	}
}
