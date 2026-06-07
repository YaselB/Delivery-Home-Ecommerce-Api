using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Worker.Delete;

public class DeleteWorkerEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt { get ;}
    public string WorkerId {get ;}
    public string WorkerEmail {get ;}
    public DeleteWorkerEntityEvent(string workerId , string workerEmail)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        WorkerId = workerId;
        WorkerEmail = workerEmail;
    }
}