using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItem> toDoItems = new List<ToDoItem>();

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetAll()
        {
            return Ok(toDoItems);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetById(int id)
        {
            var item = toDoItems.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ToDoItem> Create(ToDoItem newItem)
        {
            newItem.Id = toDoItems.Count > 0 ? toDoItems.Max(x => x.Id) + 1 : 1;
            toDoItems.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDoItem updatedItem)
        {
            var item = toDoItems.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            item.Title = updatedItem.Title;
            item.IsCompleted = updatedItem.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = toDoItems.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            toDoItems.Remove(item);
            return NoContent();
        }
    }
}
