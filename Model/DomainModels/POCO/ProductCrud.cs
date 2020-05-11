using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModels.POCO
{
    public class ProductCrud
    {
        #region [- ctor -]
        public ProductCrud()
        {

        }
        #endregion

        #region [-Insert(DomainModels.DTO.EF.Product ref_Product)-]
        public void Insert(DomainModels.DTO.EF.Product ref_Product)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    context.Product.Add(ref_Product);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    //if (context != null)
                    //{
                    //    context.Dispose();
                    //}
                }
            }
        }
        #endregion

        //one way to implement select sp with datatable
        #region [-DataTable SelectAll()-]
        //public DataTable SelectAll()
        //{
        //    using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
        //    {

        //        try
        //        {

        //            DataTable product = new DataTable();
        //            product.Columns.Add("Id", typeof(Int32));
        //            product.Columns.Add("ProductCode", typeof(Int32));
        //            product.Columns.Add("ProductName", typeof(string));
        //            product.Columns.Add("ProductDescription", typeof(string));
        //            product.Columns.Add("UnitPrice", typeof(decimal));
        //            product.Columns.Add("CategoryCode_Ref", typeof(Int32));
        //            var q = context.usp_Product_Select();
        //            foreach (DomainModels.DTO.EF.Product p in q)
        //            {
        //                product.Rows.Add(p.Id, p.ProductCode, p.ProductName,p.ProductDescription,p.UnitPrice,p.Category_Ref);
        //            }
        //            return product;
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

        #region [-dynamic SelectAll()-]
        public dynamic SelectAll()
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {

                try
                {

                    var q = context.usp_Product_SelectWithCategoryCode().ToList();
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

        #region [-Remove(DomainModels.DTO.EF.Product ref_Product)-]
        public void Remove(DomainModels.DTO.EF.Product ref_Product)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    var itemToRemove = context.Product.SingleOrDefault(x => x.Id == ref_Product.Id);
                    if (itemToRemove != null)
                    {
                        context.Product.Remove(itemToRemove);
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

        #region [-Update(DomainModels.DTO.EF.Product ref_Product)-]
        public void Update(DomainModels.DTO.EF.Product ref_Product)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    var itemToUpdate = context.Product.SingleOrDefault(x => x.Id == ref_Product.Id);
                    if (itemToUpdate != null)
                    {
                        context.Entry(itemToUpdate).CurrentValues.SetValues(ref_Product);
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
