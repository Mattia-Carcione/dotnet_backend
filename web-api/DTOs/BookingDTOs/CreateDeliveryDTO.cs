using System;

namespace DTOs.BookingDTOs;

public class CreateDeliveryDTO
{
    public required string User {get; set;}

    public required int BookingId {get; set;}

    public required int BookId {get; set;}
}
