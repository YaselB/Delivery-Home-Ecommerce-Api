using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Worker.Create;

public class CreateWorkerEntityEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string WorkerEmail {get ;}
    public string WorkerId {get;}
    public CreateWorkerEntityEvent(string workerEmail , string workerId)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        WorkerEmail = workerEmail;
        WorkerId = workerId;
    }
}