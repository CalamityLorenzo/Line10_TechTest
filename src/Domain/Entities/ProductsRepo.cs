using Domain.Db;

namespace Domain.Entities
{
    public class ProductsRepo
    {
        private IDbRepository db;

        private void ValidateProduct(Product Product)
        {
            if (String.IsNullOrEmpty(Product.Name)) throw new ArgumentOutOfRangeException("Product must have a FIRST name");
            if (String.IsNullOrEmpty(Product.Description)) throw new ArgumentOutOfRangeException("Product must have a LAST name");
            if (String.IsNullOrEmpty(Product.SKU)) throw new ArgumentOutOfRangeException("Product must have a PHONE");
        }

        public ProductsRepo(IDbRepository db)
        {
            this.db = db;
        }

        public Product Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id does not match a valid Product");
            return db.Products.Get(id);
        }

        public Product Add(Product Product)
        {
            if (Product.Id > 0) throw new InvalidDataException("Product already exists");
            ValidateProduct(Product);

            return db.Products.Add(Product);
        }

        public Product Update(Product Product)
        {
            if (Product.Id <= 0) throw new ArgumentOutOfRangeException("Id does not match a valid Product");
            return db.Products.Update(Product);
        }

        public void Delete(Product Product)
        {
            if (Product.Id <= 0) throw new ArgumentOutOfRangeException("Id does not match a valid Product");
            db.Products.Delete(Product);
        }
    }
}