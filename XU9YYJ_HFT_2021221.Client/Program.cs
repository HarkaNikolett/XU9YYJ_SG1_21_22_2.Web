using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using XU9YYJ_HFT_2021221.Client.Infrastructure;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Models.Models;
using AveragePOValue = XU9YYJ_HFT_2021221.Models.Models.AveragePOValue;

namespace XU9YYJ_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Waiting for server..");
            Console.ReadLine();
            //OldTesting();
            var httpService = new HttpService("Order", "http://localhost:55271/api/"); //55271

            // Get All Orders
            var orders = httpService.GetAll<Order>();
            DisplayOrders(orders);

            // Get one Order
            var oneOrder = httpService.Get<Order, int>(orders.First().Id);
            DisplayOrder(oneOrder);

            // Create Order
            var newOrder = new Order()
            {
                ItemId = 2,
                Quantity = 4,
                UnitPrice = 76,
                Currency = "eur",
                Note = "Ez egy próba rendelés",
                SupplierId = 1,
                SupplierName = "SKF",
                Date = DateTime.Now,
            };

            var result = httpService.Create(newOrder);

            if (result.IsSuccess)
            {
                Console.WriteLine("Creation was succesfull");
            }
            else
            {
                Console.WriteLine("Incorrect data");
            }

            //// Check
            //orders = httpService.GetAll<Order>();
            //DisplayOrders(orders);

            // Update
            var orderForUpdate = orders.First();
            orderForUpdate.UnitPrice = 12;


            result = httpService.Update(orderForUpdate);

            if (result.IsSuccess)
            {
                Console.WriteLine("Update was successfull.");
            }
            else
            {
                Console.WriteLine("Updating failed.");
            }

            //// Check
            //orders = httpService.GetAll<Order>();
            //DisplayOrders(orders);

            // Delete
            result = httpService.Delete(orderForUpdate.Id);

            if (result.IsSuccess)
            {
                Console.WriteLine("Deletion was successfull.");
            }
            else
            {
                Console.WriteLine("Deletion failed.");
            }

            //// Check
            //orders = httpService.GetAll<Order>();
            //DisplayOrders(orders);

            //    // Get averages
            //    var averagePOValues = httpService.GetAll<AveragePOValue>("GetAveragePOValues");
            //    DisplayAvaragePOValueBySupplier(averagePOValues);

            //    Console.ReadLine();
            //}
            //private static void DisplayAvaragePOValueBySupplier(List<AveragePOValue> averagePOValues)
            //{
            //    Console.WriteLine();

            //    foreach (var averagePOValue in averagePOValues)
            //    {
            //        Console.WriteLine(averagePOValue);
            //    }
            //}
        }

        private static void DisplayOrder(Order order)
        {
            Console.WriteLine("Item Id: {0}, Supplier: {1}, Price: {2}, Qty: {3}, Date: {4}, Note: {5}", order.ItemId, order.SupplierName, order.UnitPrice, order.Quantity, order.Date, order.Note);
        }
        private static void DisplaySupplier(Supplier supplier)
        {
            Console.WriteLine("Id: {0}, Name: {1}, VAT Number: {2}, Address: {3} ", supplier.Id, supplier.Name, supplier.VATNumber, supplier.Address);
        }
        private static void DisplaySuppliers(List<Supplier> suppliers)
        {
            foreach (Supplier supplier in suppliers)
            {
                DisplaySupplier(supplier);
            }
        }
        private static void DisplayOrders(List<Order> orders)
        {
            Console.WriteLine();

            foreach (var order in orders)
            {
                DisplayOrder(order);
            }
        }
        private static void DisplayItem(Item item)
        {
            Console.WriteLine("Id: {0}, Name: {1}, UOM: {2}, Supplier ID: {3}", item.Id, item.Name, item.UnitOfMeasure, item.SupplierId);
        }
        private static void DisplayItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                DisplayItem(item);
            }
        }
        private static void OldTesting()
        {
            var jsonOption = new JsonSerializerOptions(JsonSerializerDefaults.Web); //kis és nagybetűk: id -> Id
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55271/api/");
                Console.WriteLine("----------------------------GET ALL SUPPLIER------------------------");
                var supplierResp = client.GetAsync("Supplier/GetAll").GetAwaiter().GetResult();
                var suppliers = JsonSerializer.Deserialize<List<Supplier>>(supplierResp.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                DisplaySuppliers(suppliers);

                Console.WriteLine("----------------------------GET ALL ITEM------------------------");
                var itemResp = client.GetAsync("Item/GetAll").GetAwaiter().GetResult();
                var items = JsonSerializer.Deserialize<List<Item>>(itemResp.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                DisplayItems(items);
                Console.WriteLine("----------------------------GET ALL ORDER------------------------");
                //Get All
                var response = client.GetAsync("Order/GetAll").GetAwaiter().GetResult();
                var orders = JsonSerializer.Deserialize<List<Order>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                DisplayOrders(orders);
                Console.WriteLine("----------------------------CREATE SUPPLIER------------------------");
                // Post
                var newSupplier = new Supplier()
                {
                    //Id = 45,
                    Name = "TestCompany",
                    VATNumber = 98463832,
                    Address = "OverTheHillsAndFarAway",
                };
                try
                {
                    response = client.PostAsJsonAsync<Supplier>("Supplier/Create", newSupplier).GetAwaiter().GetResult();

                }
                catch (Exception) { }

                var apiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (apiResult.IsSuccess)
                {
                    Console.WriteLine("Creation was successfull");
                }
                else
                {
                    Console.WriteLine("Incorrect object data!!!!");
                }
                Console.WriteLine("----------------------------CREATE ORDER------------------------");

                // Post
                var newOrder = new Order()
                {
                    ItemId = 2,
                    Quantity = 4,
                    UnitPrice = 76,
                    Currency = "eur",
                    Note = "Ez egy próba rendelés",
                    SupplierId = 1,
                    SupplierName = "SKF",
                    Date = DateTime.Now,

                };
                try
                {
                    response = client.PostAsJsonAsync<Order>("Order/Create", newOrder).GetAwaiter().GetResult();
                }
                catch (Exception)
                {
                }

               apiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);

                if (apiResult.IsSuccess)
                {
                    Console.WriteLine("Creation was successfull");
                }
                else
                {
                    Console.WriteLine("Incorrect object data");
                }



                Console.WriteLine("----------------------------CREATE ITEM------------------------");
                // Post
                var newItem = new Item()
                {
                    Name = "testItem",
                    UnitOfMeasure = "liter",
                    SupplierId = 4,
                };

                var response2 = client.PostAsJsonAsync<Item>("Item/Create", newItem).GetAwaiter().GetResult();
                var apiResult2 = JsonSerializer.Deserialize<ApiResult>(response2.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (apiResult2.IsSuccess)
                {
                    Console.WriteLine("Creation was successfull");
                }
                else
                {
                    Console.WriteLine("Incorrect object data");
                }
                Console.WriteLine("---------------------------UPDATE ORDER----------------------------");
                //Update
                var orderForUpdate = orders.First();
                orderForUpdate.UnitPrice = 250;
                orderForUpdate.Quantity = 1500;

                response = client.PutAsJsonAsync<Order>("Order/Update", orderForUpdate).GetAwaiter().GetResult();
                var updateApiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (updateApiResult.IsSuccess)
                {
                    Console.WriteLine("Update was successfull");
                }
                Console.WriteLine("---------------------------UPDATE SUPPLIER----------------------------");
                //Update
                var supplierForUpdate = suppliers.Last();
                supplierForUpdate.VATNumber = 250;

                response = client.PutAsJsonAsync<Supplier>("Supplier/Update", supplierForUpdate).GetAwaiter().GetResult();
                updateApiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (updateApiResult.IsSuccess)
                {
                    Console.WriteLine("Update was successfull");
                }
                Console.WriteLine("---------------------------UPDATE ITEM----------------------------");
                //Update
                var itemForUpdate = items.Last();
                itemForUpdate.SupplierId = 3;

                response = client.PutAsJsonAsync<Item>("Item/Update", itemForUpdate).GetAwaiter().GetResult();
                updateApiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (updateApiResult.IsSuccess)
                {
                    Console.WriteLine("Update was successfull");
                }

                Console.WriteLine("----------------------------CHECK- GET UPDATED ORDER------------------------"); // {orders.First().Id} 
                response = client.GetAsync($"Order/Get/{orders.First().Id}").GetAwaiter().GetResult();
                var checkOneOrder = JsonSerializer.Deserialize<Order>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                DisplayOrder(checkOneOrder);
                Console.WriteLine("----------------------------CHECK- GET UPDATED SUPPLIER------------------------"); // {orders.First().Id} 
                response = client.GetAsync($"Supplier/Get/{suppliers.Last().Id}").GetAwaiter().GetResult();
                var checkOneSupplier = JsonSerializer.Deserialize<Supplier>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                DisplaySupplier(checkOneSupplier);
                Console.WriteLine("----------------------------CHECK- GET UPDATED ITEM------------------------"); // {orders.First().Id} 
                response = client.GetAsync($"Item/Get/{items.Last().Id}").GetAwaiter().GetResult();
                var checkOneItem = JsonSerializer.Deserialize<Item>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                DisplayItem(checkOneItem);
                Console.WriteLine("----------------------------DELETE ORDER------------------------");
                response = client.DeleteAsync($"Order/Delete/{orderForUpdate.Id}").GetAwaiter().GetResult();
                var deleteApiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (deleteApiResult.IsSuccess)
                {
                    Console.WriteLine("Deletion was successfull");
                }
                Console.WriteLine("----------------------------DELETE SUPPLIER------------------------");
                response = client.DeleteAsync($"Supplier/Delete/{suppliers.Last().Id}").GetAwaiter().GetResult();
                deleteApiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (deleteApiResult.IsSuccess)
                {
                    Console.WriteLine("Deletion was successfull");
                }
                Console.WriteLine("----------------------------DELETE ITEM------------------------");
                response = client.DeleteAsync($"Item/Delete/{items.Last().Id}").GetAwaiter().GetResult();
                deleteApiResult = JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                if (deleteApiResult.IsSuccess)
                {
                    Console.WriteLine("Deletion was successfull");
                }

                Console.WriteLine("----------------------------AVERAGE PO VALUE------------------------");
                    var avgResponse = client.GetAsync("Order/AvaragePOValueBySupplier").GetAwaiter().GetResult();
                    var avgStringResponse = avgResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var avgResult = JsonSerializer.Deserialize<List<AveragePOValue>>(avgStringResponse, jsonOption);
                    foreach (var res in avgResult)
                    {
                        Console.WriteLine(res);
                    }
                    Console.WriteLine("----------------------------TOP 3 ORDERS------------------------");
                    response = client.GetAsync("Order/GetAll/Top3OrderByValue").GetAwaiter().GetResult();
                    var top3Orders = JsonSerializer.Deserialize<List<Order>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                    DisplayOrders(top3Orders);
                    Console.WriteLine("----------------------------PO LIST OF ITEM ID: 2 ------------------------");
                    var poListResponse = client.GetAsync("Order/GetAll/POListOfItem?itemid=2").GetAwaiter().GetResult();
                    var poList = JsonSerializer.Deserialize<List<Order>>(poListResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);

                    DisplayOrders(poList);
                    Console.WriteLine("----------------------------PO LIST OF SUPPLIER ------------------------");

                    var selectedSupplier = suppliers.Last().Id;
                    var poListbySupplierResponse = client.GetAsync($"Order/GetAll/POListOfSupplier?supplier={selectedSupplier}").GetAwaiter().GetResult();
                    poList = JsonSerializer.Deserialize<List<Order>>(poListbySupplierResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);

                    DisplayOrders(poList);

                    Console.WriteLine("----------------------------TOTAL VALUE OF ORDERS BY SUPPLIER ------------------------");
                    response = client.GetAsync("Supplier/TotalValueofOrdersBySupplier").GetAwaiter().GetResult();
                    var totalResult = JsonSerializer.Deserialize<List<TotalValue>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                    foreach (var res in totalResult)
                    {
                        Console.WriteLine(res);
                    }
                    Console.WriteLine("----------------------------QTY OF ITEMS BY SUPPLIER ------------------------");
                    response = client.GetAsync("Supplier/QtyOfItemsBySupplier").GetAwaiter().GetResult();
                    var qtyResult = JsonSerializer.Deserialize<List<QtyOfItems>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                    foreach (var res in qtyResult)
                    {
                        Console.WriteLine(res);
                    }
                    Console.WriteLine("----------------------------ITEM LIST OF SUPPLIER ------------------------");
                    int supplierId = suppliers.Last().Id;
                    var itemListbySupplierResponse = client.GetAsync($"Item/GetAll/ItemListOfSupplier?supplierId={supplierId}").GetAwaiter().GetResult();
                    var itemList = JsonSerializer.Deserialize<List<Item>>(itemListbySupplierResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);

                    DisplayItems(itemList);

                    Console.WriteLine("----------------------------TOTAL ORDERED QTY OF ITEM ------------------------");
                    int itemid = items.Last().Id;
                    var qty = client.GetAsync($"Order/GetAll/TotalQtyOfItem?itemid={itemid}").GetAwaiter().GetResult();
                    int totalqty = JsonSerializer.Deserialize<int>(qty.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);
                    Console.WriteLine("A {0}. számú termékből összesen {1} darabot rendeltünk.", itemid, totalqty);
                    Console.ReadLine();
                }

            }
        }
    }



