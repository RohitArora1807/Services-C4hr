namespace Api_demo.Services
{
    public class HelloService : IHelloService
    {
        public string GetHelloMessage()
        {
            return "Hello";
        }
        public string ProcessHello(int id)
        {
            return $"Hello, your ID is {id}";
        }
    }
}
