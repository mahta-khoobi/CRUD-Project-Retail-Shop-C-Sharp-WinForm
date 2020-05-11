using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class OrderMasterViewModel
    {
        #region [-ctor-]
        public OrderMasterViewModel()
        {
            Ref_OrderMasterCrud = new Model.DomainModels.POCO.OrderMasterCrud();

         
        }
        #endregion

        public Model.DomainModels.POCO.OrderMasterCrud Ref_OrderMasterCrud { get; set; }
        public Model.DomainModels.DTO.EF.OrderMaster Ref_OrderMaster { get; set; }

        public Model.DomainModels.DTO.EF.Customer Ref_Customer { get; set; }
        public Model.DomainModels.DTO.EF.Product Ref_Product { get; set; }





        #region [-Save(int orderCode,DateTime orderDate, int customer_Ref, DataTable orderDetailsList)-]
        public void Save(int orderCode,DateTime orderDate, int customer_Ref, DataTable orderDetailsList)
        {
            Ref_OrderMaster = new Model.DomainModels.DTO.EF.OrderMaster();
            Ref_OrderMaster.OrderCode = orderCode;
            Ref_OrderMaster.OrderDate = orderDate;
            Ref_OrderMaster.Customer_Ref = customer_Ref;
            Ref_OrderMasterCrud.InsertOrderMasterDetails(Ref_OrderMaster, orderDetailsList);

        }
        #endregion

        //it could be list of OrderDetails instead of DataTable in Update methode
        #region [-Edit(int id,int orderCode,DateTime orderDate, int customer_Ref, DataTable orderDetailsList)-]
        public void Edit(int id, int orderCode,DateTime orderDate, int customer_Ref, DataTable orderDetailsList)
        {
            Ref_OrderMaster = new Model.DomainModels.DTO.EF.OrderMaster();
            Ref_OrderMaster.Id = id;
            Ref_OrderMaster.OrderCode = orderCode;
            Ref_OrderMaster.OrderDate = orderDate;
            Ref_OrderMaster.Customer_Ref = customer_Ref;
            Ref_OrderMasterCrud.UpdateOrderMasterDetails(Ref_OrderMaster, orderDetailsList);

        }
        #endregion

        #region [-Delete(int id)-]
        public void Delete(int id)
        {
            Ref_OrderMaster = new Model.DomainModels.DTO.EF.OrderMaster();
            Ref_OrderMaster.Id = id;
            Ref_OrderMasterCrud.Remove(Ref_OrderMaster);

        }
        #endregion

        #region [-dynamic FillGrid()-]
        public dynamic FillGrid()
        {
            return Ref_OrderMasterCrud.SelectAll();
        }
        #endregion

        #region [-GetOrderDetailsGrid(int id)-]
        public dynamic GetOrderDetailsGrid(int id)
        {
            Ref_OrderMaster = new Model.DomainModels.DTO.EF.OrderMaster();
            Ref_OrderMaster.Id = id;
            return Ref_OrderMasterCrud.SelectOrderDetailsGivenOrdeMasterId(Ref_OrderMaster);
        }
        #endregion

        #region [-GetOrderCode()-]
        public int GetOrderCode()
        {
            Ref_OrderMaster = new Model.DomainModels.DTO.EF.OrderMaster();
            return Ref_OrderMasterCrud.GenerateCode();
        } 
        #endregion

        #region [-GetCustomerName(int id)-]
        public string GetCustomerName(int id)
        {
            Ref_Customer = new Model.DomainModels.DTO.EF.Customer();
            Ref_Customer.Id = id;
            return Ref_OrderMasterCrud.SelectCustomerNameGivenId(Ref_Customer);
        }
        #endregion

        #region [-GetProductName(int id)-]
        public string GetProductName(int id)
        {
            Ref_Product = new Model.DomainModels.DTO.EF.Product();
            Ref_Product.Id = id;
            return Ref_OrderMasterCrud.SelectProductNameGivenId(Ref_Product);
        }
        #endregion

        #region [-GetCustomerInfoList()-]
        public dynamic GetCustomerInfoList()
        {

            return Ref_OrderMasterCrud.SelectCustomerInfo();
        }
        #endregion

        #region [-GetProductInfoList()-]
        public dynamic GetProductInfoList()
        {

            return Ref_OrderMasterCrud.SelectProductInfo();
        }
        #endregion

        #region [-GetCategoryCodeList()-]
        public dynamic GetCategoryCodeList()
        {

            return Ref_OrderMasterCrud.SelectCategoryCode();
        }
        #endregion











    }

}
