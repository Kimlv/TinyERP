namespace App.Common.Mapping
{
    public interface ICustomMap<TFromEntity> where TFromEntity: class
    {
        void MapFrom(TFromEntity from);
    }
}
