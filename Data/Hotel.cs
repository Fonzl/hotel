﻿

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class Hotel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Info { get; set; }
    public long CityId { get; set; }
    public  double Rating { get; set; }
    public City City { get; set; }
    public long? HotelСhainId {  get; set; }
    public HotelСhain? HotelСhain { get; set; }
    public long IdMedia { get; set; }
    public List<HotelRoom> HotelRooms { get; set; } = [];
}
public class HotelMap
{
    public HotelMap(EntityTypeBuilder<Hotel> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder.Property(x => x.Name).IsRequired();
        entityTypeBuilder.Property(x=> x.Info).IsRequired();
        entityTypeBuilder.
            HasMany(x => x.HotelRooms)
            .WithOne(x => x.Hotel)
            .HasForeignKey(x => x.HotelId).IsRequired(); 

    }
}
