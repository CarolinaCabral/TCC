namespace CoffeeExperience.Domain.TO
{

    public class Response
    {
        public Response(bool _Error)
        {
            this.Error = _Error;
            Message = "Sucesso";
        }
        public Response(bool _Error, string _Message)
        {
            this.Error = _Error;
            this.Message = _Message;
        }
        public bool Error { get;  protected set; }
        public string Message { get; protected set; }
    }
}
