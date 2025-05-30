using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers
{
	[Authorize]
	public class TasksController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;

		public TasksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(string statusFilter = "all", string sortBy = "duedate")
		{
			var userId = _userManager.GetUserId(User);
			var query = _context.TaskItems.Where(t => t.UserId == userId);

			// Apply status filter
			switch (statusFilter?.ToLower())
			{
				case "completed":
					query = query.Where(t => t.IsCompleted);
					break;
				case "pending":
					query = query.Where(t => !t.IsCompleted);
					break;
				default:
					statusFilter = "all";
					break;
			}

			// Apply sorting
			switch (sortBy?.ToLower())
			{
				case "title":
					query = query.OrderBy(t => t.Title);
					break;
				case "created":
					query = query.OrderByDescending(t => t.CreatedAt);
					break;
				case "duedate":
				default:
					query = query.OrderBy(t => t.DueDate ?? DateTime.MaxValue);
					sortBy = "duedate";
					break;
			}

			var tasks = await query.ToListAsync();
			var allUserTasks = await _context.TaskItems.Where(t => t.UserId == userId).ToListAsync();

			var viewModel = new TaskListViewModel
			{
				Tasks = tasks,
				StatusFilter = statusFilter,
				SortBy = sortBy,
				TotalCount = allUserTasks.Count,
				CompletedCount = allUserTasks.Count(t => t.IsCompleted),
				PendingCount = allUserTasks.Count(t => !t.IsCompleted)
			};

			return View(viewModel);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TaskViewModel model)
		{
			if (ModelState.IsValid)
			{
				var task = new TaskItem
				{
					UserId = _userManager.GetUserId(User),
					Title = model.Title,
					Description = model.Description,
					IsCompleted = model.IsCompleted,
					DueDate = model.DueDate
				};

				_context.Add(task);
				await _context.SaveChangesAsync();
				TempData["Success"] = "Task created successfully!";
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var userId = _userManager.GetUserId(User);
			var task = await _context.TaskItems
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

			if (task == null) return NotFound();

			var model = new TaskViewModel
			{
				Id = task.Id,
				Title = task.Title,
				Description = task.Description,
				IsCompleted = task.IsCompleted,
				DueDate = task.DueDate
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, TaskViewModel model)
		{
			if (id != model.Id) return NotFound();

			if (ModelState.IsValid)
			{
				var userId = _userManager.GetUserId(User);
				var task = await _context.TaskItems
					.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

				if (task == null) return NotFound();

				task.Title = model.Title;
				task.Description = model.Description;
				task.IsCompleted = model.IsCompleted;
				task.DueDate = model.DueDate;

				try
				{
					_context.Update(task);
					await _context.SaveChangesAsync();
					TempData["Success"] = "Task updated successfully!";
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TaskExists(task.Id))
						return NotFound();
					throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var userId = _userManager.GetUserId(User);
			var task = await _context.TaskItems
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

			if (task != null)
			{
				_context.TaskItems.Remove(task);
				await _context.SaveChangesAsync();
				TempData["Success"] = "Task deleted successfully!";
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ToggleComplete(int id)
		{
			var userId = _userManager.GetUserId(User);
			var task = await _context.TaskItems
				.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

			if (task != null)
			{
				task.IsCompleted = !task.IsCompleted;
				await _context.SaveChangesAsync();
				TempData["Success"] = $"Task marked as {(task.IsCompleted ? "completed" : "pending")}!";
			}

			return RedirectToAction(nameof(Index));
		}

		private bool TaskExists(int id)
		{
			return _context.TaskItems.Any(e => e.Id == id);
		}
	}
}