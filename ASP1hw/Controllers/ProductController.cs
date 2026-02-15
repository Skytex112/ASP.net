using ASP1hw.DTO;
using ASP1hw.Models;
using Microsoft.AspNetCore.Mvc;

public class ProductController : Controller
{
    private static List<Product> products = new List<Product>();

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(ProductDTO dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price
        };

        products.Add(product);
        return RedirectToAction("Search");
    }

    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
            products.Remove(product);


        return RedirectToAction("Search");
    }
    public IActionResult Search(string name)
    {
        var results = string.IsNullOrEmpty(name)
            ? new List<Product>()
            : products.Where(p =>
                p.Name != null &&
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
              .ToList();

        return View(results);
    }
}
