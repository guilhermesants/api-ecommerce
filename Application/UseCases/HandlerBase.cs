using Domain.Interfaces.Repositories;

namespace Application.UseCases;

public abstract class HandlerBase
{
    protected readonly IUnitOfWork _uow;

    protected HandlerBase(IUnitOfWork uow) => _uow = uow;
}
