/*
 * Author: Sakthi Santhosh
 * Created on: 24/04/2024
 */
namespace Challenge1.Library.Exceptions;

public class ModelEntityNotFoundException : Exception
{
    public ModelEntityNotFoundException() : base(message: "No entity with the ID is found.")
    {
    }

    public ModelEntityNotFoundException(string message) : base(message)
    {
    }

    public ModelEntityNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class ModelEntityInactiveException : Exception
{
    public ModelEntityInactiveException() : base(message: "The entity is inactive.")
    {
    }

    public ModelEntityInactiveException(string message) : base(message)
    {
    }

    public ModelEntityInactiveException(string message, Exception inner) : base(message, inner)
    {
    }
}
