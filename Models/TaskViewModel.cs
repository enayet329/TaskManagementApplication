using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models
{
	public class TaskViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		[StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
		public string Title { get; set; }

		[StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
		public string? Description { get; set; }

		public bool IsCompleted { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DueDate { get; set; }
	}

	public class TaskListViewModel
	{
		public List<TaskItem> Tasks { get; set; } = new();
		public string StatusFilter { get; set; } = "all";
		public string SortBy { get; set; } = "duedate";
		public int CompletedCount { get; set; }
		public int PendingCount { get; set; }
		public int TotalCount { get; set; }
	}
}
