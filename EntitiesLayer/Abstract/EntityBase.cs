
namespace EntitiesLayer.Abstract
{
    public abstract class EntityBase<TId>
    {
        public virtual TId Id { get; set; }
    }
}
