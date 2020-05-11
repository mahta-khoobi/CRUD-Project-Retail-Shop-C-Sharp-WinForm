using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductViewModel
    {
        #region [- ctor -]
        public ProductViewModel()
        {
            Ref_ProductCrud = new Model.DomainModels.POCO.ProductCrud();
        }
        #endregion
        public Model.DomainModels.DTO.EF.Product Ref_Product { get; set; }
        public Model.DomainModels.POCO.ProductCrud Ref_ProductCrud { get; set; }

        #region [- Save(string productName, string productDescription, decimal unitPrice, int category_ref) -]
        public void Save(string productName, string productDescription, decimal unitPrice, int category_ref)
        {
            Ref_Product = new Model.DomainModels.DTO.EF.Product();
            Ref_Product.ProductName = productName;
            Ref_Product.ProductDescription = productDescription;
            Ref_Product.UnitPrice = unitPrice;
            Ref_Product.Category_Ref = category_ref;
            Ref_ProductCrud.Insert(Ref_Product);

        }
        #endregion

        #region [-dynamic FillGrid()-]
        public dynamic FillGrid() //goftim dynamic k moshkele view dar refresh hal shavad va lazem nist list bargardanad
        {
            return Ref_ProductCrud.SelectAll();
        }
        #endregion

        #region [-Delete(int id)-]
        public void Delete(int id)
        {
            Ref_Product = new Model.DomainModels.DTO.EF.Product();
            Ref_Product.Id = id;

            Ref_ProductCrud.Remove(Ref_Product);

        }

        #endregion

        #region [- Edit(int id,string productName, string productDescription, decimal unitPrice, int category_ref) -]
        public void Edit(int id,string productName, string productDescription, decimal unitPrice, int category_ref)
        {
            Ref_Product = new Model.DomainModels.DTO.EF.Product();
            Ref_Product.Id = id;
            Ref_Product.ProductName = productName;
            Ref_Product.ProductDescription = productDescription;
            Ref_Product.UnitPrice = unitPrice;
            Ref_Product.Category_Ref = category_ref;
            Ref_ProductCrud.Update(Ref_Product);

        }
        #endregion




    }
}
