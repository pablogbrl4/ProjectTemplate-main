namespace ProjectTemplate.Application.Requests
{
    public class PagesClienteRequest
    {
        public int Limit { get; set; } = 50;
        public int Page { get; set; } = 1;
        public string Nome { get; set; }
    }
}
