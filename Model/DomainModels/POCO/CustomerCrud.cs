using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModels.POCO
{
   public class CustomerCrud
    {
        #region [-ctor-]
        public CustomerCrud()
        {

        }
        #endregion

        #region [-Insert(DomainModels.DTO.EF.Customer ref_Customert)-]
        public void Insert(DomainModels.DTO.EF.Customer ref_Customer)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    context.Customer.Add(ref_Customer);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }
        }
        #endregion

        #region [-DataTable SelectAll()-]
        //public DataTable SelectAll()
        //{
        //    using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
        //    {

        //        try
        //        {

        //            DataTable productCategory = new DataTable();
        //            productCategory.Columns.Add("Id", typeof(Int32));
        //            productCategory.Columns.Add("CustomerCode", typeof(Int32));
        //            productCategory.Columns.Add("FirstName", typeof(string));
        //            productCategory.Columns.Add("LastName", typeof(string));
        //            productCategory.Columns.Add("Adrs", typeof(string));
        //            productCategory.Columns.Add("City", typeof(string));
        //            productCategory.Columns.Add("Region", typeof(string));
        //            productCategory.Columns.Add("Country", typeof(string));
        //            productCategory.Columns.Add("PostalCode", typeof(string));
        //            productCategory.Columns.Add("Phone", typeof(string));
        //            productCategory.Columns.Add("Email", typeof(string));

        //            var q = context.usp_Customer_Select();
        //            foreach (DomainModels.DTO.EF.Customer customer in q)
        //            {
        //                productCategory.Rows.Add(customer.Id, customer.CustomerCode, customer.FirstName,customer.LastName,customer.Adrs,customer.City,customer.Region,customer.Country,customer.PostalCode,customer.Phone,customer.Email);
        //            }
        //            return productCategory;
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //        finally
        //        {

        //        }
        //    }

        //}
        #endregion

        #region [-List<DomainModels.DTO.EF.Customer> SelectAll()-]
        public List<DomainModels.DTO.EF.Customer> SelectAll()
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {

                try
                {
                    var q = context.usp_Customer_Select().ToList();
                    return q;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {

                }
            }

        } 
        #endregion

        #region [-Remove(DomainModels.DTO.EF.Customer ref_Customer)-]
        public void Remove(DomainModels.DTO.EF.Customer ref_Customer)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    var itemToRemove = context.Customer.SingleOrDefault(x => x.Id == ref_Customer.Id);
                    if (itemToRemove != null)
                    {
                        context.Customer.Remove(itemToRemove);
                        context.SaveChanges();
                    }
                }

                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }

                }
            }
        }
        #endregion

        #region [-Update(DomainModels.DTO.EF.Customer ref_Customer)-]
        public void Update(DomainModels.DTO.EF.Customer ref_Customer)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    var itemToUpdate = context.Customer.SingleOrDefault(x => x.Id == ref_Customer.Id);
                    if (itemToUpdate != null)
                    {
                        context.Entry(itemToUpdate).CurrentValues.SetValues(ref_Customer);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }
        }
        #endregion
    }
}
