namespace Store.Data.Entities
{
    public class BaseEntity<T>
    {

        public T Id { get; set; }
        public DateTime createdAt { get; set; }= DateTime.Now;        //default
    }
}