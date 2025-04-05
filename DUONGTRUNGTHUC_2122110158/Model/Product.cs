namespace DUONGTRUNGTHUC_2122110158.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public int CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
