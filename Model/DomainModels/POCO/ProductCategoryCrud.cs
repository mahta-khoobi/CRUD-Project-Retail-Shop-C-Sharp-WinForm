using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModels.POCO
{
    public class ProductCategoryCrud
    {
        #region [-ctor-]
        public ProductCategoryCrud()
        {

        }
        #endregion

        #region [-Insert(DomainModels.DTO.EF.ProductCategory ref_ProductCategory)-]
        public void Insert(DomainModels.DTO.EF.ProductCategory ref_ProductCategory)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    
                    context.ProductCategory.Add(ref_ProductCategory);
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

        //one way is using data table in select sp
        #region [-DataTable SelectAll()-]
        //public DataTable SelectAll()
        //{
        //    using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
        //    {

        //        try
        //        {

        //            DataTable productCategory = new DataTable();
        //            productCategory.Columns.Add("Id",typeof(Int32));
        //            productCategory.Columns.Add("CategoryCode", typeof(Int32));
        //            productCategory.Columns.Add("CategoryName",typeof(string));
        //            var q = context.usp_ProductCategory_Select();
        //            foreach (DomainModels.DTO.EF.ProductCategory pc in q)
        //            {
        //                productCategory.Rows.Add(pc.Id,pc.CategoryCode,pc.CategoryName);
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
        //other way is using list
        #region [-List<DomainModels.DTO.EF.ProductCategory> SelectAll()-]
        public List<DomainModels.DTO.EF.ProductCategory> SelectAll()
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {

                try
                {
                    var q = context.usp_ProductCategory_Select().ToList();

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

        #region [-Remove(DomainModels.DTO.EF.ProductCategory ref_ProductCategory)-]
        public void Remove(DomainModels.DTO.EF.ProductCategory ref_ProductCategory)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    var itemToRemove = context.ProductCategory.SingleOrDefault(x => x.Id == ref_ProductCategory.Id);
                    if (itemToRemove != null)
                    {
                        context.ProductCategory.Remove(itemToRemove);
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

        #region [-Update(DomainModels.DTO.EF.ProductCategory ref_ProductCategory)-]
        public void Update(DomainModels.DTO.EF.ProductCategory ref_ProductCategory)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    var itemToUpdate = context.ProductCategory.SingleOrDefault(x => x.Id == ref_ProductCategory.Id);
                    if (itemToUpdate != null)
                    {
                        context.Entry(itemToUpdate).CurrentValues.SetValues(ref_ProductCategory);
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
