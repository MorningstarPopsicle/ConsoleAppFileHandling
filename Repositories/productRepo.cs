using System;
using ECommerceApp.Models;
using ECommerceApp.Enums;
namespace ECommerceApp.Repositories
{
    public class ProductRepo
    {
        public static int myIndex = 0;
        private static int count = 1;
        public static Products[] products = new Products[50];
        public ProductRepo()
        {
            var product = new Products(1,"01","Sardine", 700, 12);
            products[0] = product;
            myIndex++;
            count++;

        }
        public string AddProduct(string productName, decimal price, int quantity)
        {
             string productNo =  $"{count.ToString("00")}";
            var newProduct = new Products(count, productNo, productName, price, quantity);
            if (myIndex == 0)
            {
                products[0] = newProduct;
                count++;
                myIndex++;
            }
            else
            {
                bool isProductExist = false;
                for (int i = 0; i < myIndex; i++)
                {
                    if (products[i].ProductName == productName)
                    {
                        isProductExist = true;
                        products[i].Quantity += quantity;
                        break;
                    }
                }

                if (!isProductExist)
                {
                    products[myIndex] = newProduct;
                    count++;
                    myIndex++;
                    Console.WriteLine(myIndex);

                }
            }

            Console.WriteLine($"You have successfully added {newProduct.ProductName}");
            return newProduct.ProductNo;
        }
        public void RemoveProduct(string productName)
        {
            for (int i = 0; i < myIndex; i++)
            {
                if (products[i].ProductName == productName)
                {
                    products[i] = null;
                }
            }
            Console.WriteLine($"You have successfully removed a product ");

        }

        public void EditProduct(string productNo, string productName, decimal price, int quantity)
        {
            var product = GetProduct(productNo);
            product.Price = price;
            product.ProductName = productName;
            product.Quantity = quantity;
            Console.WriteLine($"The product has been edited. It is now{product.ProductName} {product.Price} {product.Quantity}");
        }


        public void ReduceQuantity(int quantity, string productid)
        {
            var product = GetProduct(productid);
            product.Quantity -= quantity;
        }
        public void ReturnProducts()
        {
            Console.WriteLine($"MyIndex is {myIndex}");
            for (int i = 0; i < myIndex; i++)
            {
                Console.WriteLine($"{i + 1}. Product Name: {products[i].ProductName} Price: {products[i].Price} Quantity: {products[i].Quantity} ");
            }

        }
        public Products GetProduct(string productName)
        {
            for (int i = 0; i < myIndex; i++)
            {
                if (products[i] != null && products[i].ProductName.ToUpper() == productName.ToUpper())
                {
                    return products[i];
                }
            }
            return null;
        }
    }
}