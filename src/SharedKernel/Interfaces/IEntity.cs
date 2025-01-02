namespace SharedKernel.Interfaces;

public interface IEntity
{ }

public interface IEntity<TId> : IEntity
    where TId : struct, IEquatable<TId>
{
    TId Id { get; }
}
