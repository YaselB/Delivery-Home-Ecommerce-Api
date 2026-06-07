using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Worker.UpdatePassword;

public class UpdateWorkerPasswordEvent : IDomainEvent, INotification
{
    public string id {get ; }

    public DateTime CreatedAt {get ;}
    public string WorkerId {get ;}
    public string WorkerEmail {get ;}
    public UpdateWorkerPasswordEvent(string workerEmail , string workerId)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        WorkerId = workerId;
        WorkerEmail = workerEmail;
    }
}