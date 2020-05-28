using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();

            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();

            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();
        }
    }
}
