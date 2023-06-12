namespace CRUD.Models
{
    public class KlaiduModelis
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}