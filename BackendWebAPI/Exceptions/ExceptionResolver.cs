namespace BackendWebAPI.Exceptions
{
    public class ExceptionResolver
    {
        public (int code, string error) Resolve(Exception ex) =>
            ex switch
            {
                //InvalidAuthenticationException e => (e.Code, e.Message ),
                _ => (StatusCodes.Status500InternalServerError, ex.Message),
            };
    }
}
