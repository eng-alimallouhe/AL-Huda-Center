namespace LMS.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string massage) : base(massage) { }
    }
}
