/*
*TODO:
*Creare un helper per la validazione di tutti i casi d'uso
*Durante la prenotazione e la consegna del libro
*Input validationFunction: false => true //throw exception
*/

using Exceptions;

namespace Helpers;

public static class ValidatorHelper
{
    public static void CheckIsValid<T>(T parameter, Func<T, bool> validationFunction, BookingException.Exceptions exceptionType)
    {
        if (!validationFunction(parameter))
            throw new BookingException(exceptionType);
    }
}
