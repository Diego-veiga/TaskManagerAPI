using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOFWork
    {
        private TaskRepository _taskRepository;

        public TaskManagerDbContext _context;

        public UnitOfWork(TaskManagerDbContext context)
        {
            _context = context;
        }

        public ITaskRepository taskRepository
        {
            get
            {
                return _taskRepository = _taskRepository ?? new TaskRepository(_context);
            }
        }

        
       

       

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
