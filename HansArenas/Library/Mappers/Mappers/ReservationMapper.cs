using Dtos.BookDtos;
using Entities.Model;
using Dtos.ReservationDtos;

namespace Mapper.Reservations
{
    public static class ReservationMapper
    {
        public static ReservationDto ToReservationDto(this Reservation reservationModel)
        {
            return new ReservationDto
            {
                ReservationId = reservationModel.ReservationId,
                User = reservationModel.User,
                Reservation_Date = reservationModel.Reservation_Date,
                Return_Date = reservationModel.Return_Date,
                BookId = reservationModel.BookId,
            };
        }

        public static Reservation ToReservationFromCreateReservationDto(this ReservationDto reservationModel) 
        {

            return new Reservation
            {
                User = reservationModel.User,
                Reservation_Date = reservationModel.Reservation_Date,
                Return_Date = reservationModel.Return_Date,
                BookId = reservationModel.BookId,
            };
        }
    }
}
