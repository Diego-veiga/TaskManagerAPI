namespace TaskManager.Core.Interfaces
{
    public interface IUnitOFWork
    {
        ITaskRepository taskRepository { get; }
      
        Task Commit();
    }
}
