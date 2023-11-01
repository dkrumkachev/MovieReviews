namespace BusinessLayer.Exceptions
{
	public class IncorrectPasswordException : Exception
	{
        public IncorrectPasswordException() { }

		public IncorrectPasswordException(string message) : base(message) { }
    }
}
