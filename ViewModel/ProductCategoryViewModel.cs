using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductCategoryViewModel
    {
        #region [-ctor-]
        public ProductCategoryViewModel()
        {
            Ref_ProductCategoryCrud = new Model.DomainModels.POCO.ProductCategoryCrud();
        }
        #endregion

        public Model.DomainModels.DTO.EF.ProductCategory Ref_ProductCateory { get; set; }
        public Model.DomainModels.POCO.ProductCategoryCrud Ref_ProductCategoryCrud { get; set; }

        #region [- Save(string categoryName) -]
        public void Save(string categoryName)
        {
            Ref_ProductCateory = new Model.DomainModels.DTO.EF.ProductCategory();
            Ref_ProductCateory.CategoryName = categoryName;
            Ref_ProductCategoryCrud.Insert(Ref_ProductCateory);

        }
        #endregion

        #region [-dynamic FillGrid()-]
        public dynamic FillGrid() //goftim dynamic k moshkele view dar refresh hal shavad va lazem nist list bargardanad
        {
            return Ref_ProductCategoryCrud.SelectAll();
        }
        #endregion

        #region [-Delete(int id)-]
        public void Delete(int id)
        {
            Ref_ProductCateory = new Model.DomainModels.DTO.EF.ProductCategory();
            Ref_ProductCateory.Id = id;

            Ref_ProductCategoryCrud.Remove(Ref_ProductCateory);

        }

        #endregion

        #region [- Edit(int id,  string categoryName) -]
        public void Edit(int id,  string categoryName)
        {
            Ref_ProductCateory = new Model.DomainModels.DTO.EF.ProductCategory();
            Ref_ProductCateory.Id = id;
          //  Ref_ProductCateory.CategoryCode = categoryCode;
            Ref_ProductCateory.CategoryName = categoryName;
            Ref_ProductCategoryCrud.Update(Ref_ProductCateory);

        }
        #endregion
    }
}
