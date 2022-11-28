using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskDelegatingWebApp.Models;
using TaskDelegatingWebApp.Dtos;
using AutoMapper;
using TaskDelegatingWebApp.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace TaskDelegatingWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskDelegatingWebAppContext _context;
        private readonly IMapper _mapper;

        public TasksController(TaskDelegatingWebAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET /api/tasks
        public IEnumerable<TaskItemDto> GetTasks()
        {




            return _context.TaskItem.Include(e => e.Day).Include(e => e.Person).ToList().Select(_mapper.Map<TaskItem, TaskItemDto>);

        }

        // Get /api/tasks/1
        [Route("/api/[controller]/{id}")]
        public TaskItemDto GetTask(int id)
        {
            var task = _context.TaskItem.Include(e => e.Day).Include(e => e.Person).SingleOrDefault(c => c.TaskItemId == id);

            if (task == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return _mapper.Map<TaskItem, TaskItemDto>(task);

        }


        // POST /api/task/1
        [Route("api/[controller]/CreateTask")]
        [HttpPost]
        public TaskItemDto CreateTask(TaskItemDto taskDto)
        {
            if (!ModelState.IsValid)

                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());

            var task = _mapper.Map<TaskItemDto, TaskItem>(taskDto);
            _context.TaskItem.Add(task);
            _context.SaveChanges();

            taskDto.DayId = task.DayId;
            return taskDto;
        }


        // PUT /api/tasks/1

        [Route("api/[controller]/UpdateTask/{id}")]
        public void UpdateTask(int id, TaskItemDto taskDto)
        {
            if (!ModelState.IsValid)

                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            var taskInDb = _context.TaskItem.SingleOrDefault(c => c.TaskItemId == id);

            if (taskInDb == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }

            _mapper.Map(taskDto, taskInDb);

            

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [Route("api/[controller]/Delete/{id}")]
        [HttpDelete]
        public void DetleteTask(int id)
        {
            var taskitemindb = _context.TaskItem.SingleOrDefault(c => c.TaskItemId == id);
            if (taskitemindb == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _context.TaskItem.Remove(taskitemindb);
        }

    }



}
