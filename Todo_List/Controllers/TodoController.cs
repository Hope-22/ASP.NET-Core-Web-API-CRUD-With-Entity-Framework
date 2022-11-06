using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_List.Data;
using Todo_List.Models;

namespace Todo_List.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TodoController : Controller
    {
        private readonly TodoAPIDbContext dbContext;

        public TodoController(TodoAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {

            return Ok(await dbContext.Todos.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTodo([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();

            }
            return Ok(todo); 
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoRequest addTodoRequest)
        {
            var todo = new Todo()
            {
                Id = Guid.NewGuid(),

                Content = addTodoRequest.Content
            };

            await dbContext.Todos.AddAsync(todo);
            await dbContext.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpPut]
        [Route ("{id:guid}")]
        public async Task <IActionResult> UpdateTodo([FromRoute] Guid id, UpdateTodoRequest updateTodoRequest)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo != null)
            {
                todo.Content = updateTodoRequest.Content;

                await dbContext.SaveChangesAsync();

                return Ok(todo);  
            };

            return NotFound();

           
        }

        [HttpDelete]
        [Route ("{id:guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
           var todo =  await dbContext.Todos.FindAsync(id);

            if ( todo != null)
            {
                dbContext.Remove(todo);
                await dbContext.SaveChangesAsync();
                return Ok(todo);
            }

            return NotFound();
        }   
    }
}
