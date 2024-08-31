/*
*TODO:
*Creare un helper per la validazione di tutti i casi d'uso
*Durante la prenotazione e la consegna del libro
*Input validationFunction: false => true //throw exception
*/

using Exceptions;

namespace Helpers;

/// <summary>
/// Provides helper validator to validate a specified expression.
/// </summary>
public static class ValidatorHelper
{
    /// <summary>
    /// Validates an entity of type <typeparamref name="T"/> by a specified lambda expression <see cref="Func{T, TResult}"/>,
    /// if validation is false a specified <see cref="BookingException"/> is thrown.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="parameter">The entity being validated.</param>
    /// <param name="validationFunction">A lambda expression <see cref="Func{T, TResult}"/> representing a function that must be validated.
    /// The function returns <see langword="true"/> if the entity is valid; otherwise, <see langword="false"/>.</param>
    /// <param name="exceptionType">A type of <see cref="BookingException.Exceptions"/> to be thrown if validation fails.</param>
    /// <exception cref="BookingException">Thrown when a validation function returns <see langword="false"/></exception>
    public static void CheckIsValid<T>(T parameter, Func<T, bool> validationFunction, BookingException.Exceptions exceptionType)
    {
        if (!validationFunction(parameter))
            throw new BookingException(exceptionType);
    }
}
