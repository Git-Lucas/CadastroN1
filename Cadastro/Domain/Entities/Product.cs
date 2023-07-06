namespace Cadastro.Domain.Entities
{
    public class Product: BaseModel
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public bool Active { get; set; }
        public int IdClient { get; set; }
        public virtual Client Client { get; set; }
    }
}