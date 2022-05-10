using AutoMapper;

namespace Pulse.Users.Core.Interfaces.Mapper
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
