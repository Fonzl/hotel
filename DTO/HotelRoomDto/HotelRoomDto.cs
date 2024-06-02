﻿

using Data;

namespace DTO.HotelRoomDto
{
    public class HotelRoomDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long NumberOfBeds { get; set; }
        public string Info { get; set; }
        public long PricePerNight { get; set; }
        public long HotelId { get; set; }
        public HotelDto.HotelDto Hotel { get; set; }
        public long TypeOfNumberId { get; set; }
        public TypeOfNumberDto.TypeOfNumberDto TypeOfNumber { get; set; }
    }
}
