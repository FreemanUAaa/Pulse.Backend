using AutoMapper;
using Pulse.Users.Core.Interfaces.Mapper;
using Pulse.Users.Core.Models;
using System;
using System.Collections.Generic;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsVm : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public string Website { get; set; }

        public List<Guid> MusicTypeIds { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<User, GetUserDetailsVm>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Email,
                    opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Website,
                    opt => opt.MapFrom(x => x.Website))
                .ForMember(x => x.Location,
                    opt => opt.MapFrom(x => x.Location))
                .ForMember(x => x.MusicTypeIds,
                    opt => opt.MapFrom(x => x.MusicTypeIds));
    }
}
