namespace LocateMyCarWeb.Models
{
    public class MessageResponse<T> where T:class
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public T Entity { get; set; }
    }
}
