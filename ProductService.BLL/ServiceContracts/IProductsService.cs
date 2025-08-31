using ProductService.BLL.DTO;
using ProductService.DAL.Entities;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts;

public interface IProductsService
{
  /// <summary>
  /// Retrieves the list of products from the prodcts repository
  /// </summary>
  /// <returns>Returns list of ProductResponse objects</returns>
  Task<List<ProductResponse?>> GetProducts();


  /// <summary>
  /// Retrieves list of products matching with given condition
  /// </summary>

  Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);


  /// <summary>
  /// Returns a single product that matches with given condition
  /// </summary>

  Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);


  /// <summary>
  /// Adds (inserts) product into the table using products repository
  /// </summary>

  Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);


  /// <summary>
  /// Updates the existing product based on the ProductID
  /// </summary>

  Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);


  /// <summary>
  /// Deletes an existing product based on given product id
  /// </summary>
  Task<bool> DeleteProduct(Guid productID);
}
