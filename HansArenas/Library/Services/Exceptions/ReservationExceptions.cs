using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Model;

namespace Services.Exceptions
{
    public enum ErrorCode
    {
        UserWithActiveReservation,
        BookAlredyReservedByUser,
        BookNotFound,
        FinishedCopies,
        ReservationNotFound,
        ReservationNotActive,
        UserWithActiveReservationNotFound

    }
    public class ReservationExeption : Exception
    {
        public ErrorCode ErrorCode { get; }


        public ReservationExeption(string message, ErrorCode errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
    public class ReturnException : Exception
    {
        public ErrorCode ErrorCode { get; }
        public Book Book { get; }
        public ReturnException(string message, ErrorCode erroreCode)
            : base(message)
        {
            ErrorCode = erroreCode;
        }
    }
}
