using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModels.POCO
{
    public class OrderMasterCrud
    {
        #region [-ctor-]
        public OrderMasterCrud()
        {

        }
        #endregion

        //one way to implement select sp is with Datatable

        #region [-DataTable SelectAll()-]
        //public DataTable SelectAll()
        //{
        //    using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
        //    {

        //        try
        //        {

        //            DataTable orderMasters = new DataTable();
        //            orderMasters.Columns.Add("Id", typeof(Int32));
        //            orderMasters.Columns.Add("OrderDate", typeof(DateTime));
        //            orderMasters.Columns.Add("Customer_Ref", typeof(Int32));
        //            var q = context.usp_OrderMaster_Select();
        //            foreach (DomainModels.DTO.EF.OrderMaster ordermaster in q)
        //            {
        //                orderMasters.Rows.Add(ordermaster.Id, ordermaster.OrderDate, ordermaster.Customer_Ref);
        //            }
        //            return orderMasters;
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

        //one way to implement select sp is with List

        #region [- SelectAll()-]
        public dynamic SelectAll()
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {

                try
                {


                    var q = context.usp_OrderMaster_Select().ToList();
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

        #region [-Remove(DomainModels.DTO.EF.OrderMaster ref_OrderMaster)-]
            public void Remove(DomainModels.DTO.EF.OrderMaster ref_OrderMaster)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    var itemToRemove = context.OrderMaster.SingleOrDefault(x => x.Id == ref_OrderMaster.Id);
                    if (itemToRemove != null)
                    {
                       // context.OrderMaster.Remove(itemToRemove);
                        context.usp_OrderMasterDetails_Delete(itemToRemove.Id);
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

        #region [-UpdateOrderMasterDetails(DomainModels.DTO.EF.OrderMaster ref_OrderMaster,DataTable ref_OrderDetails)-]
        public void UpdateOrderMasterDetails(DomainModels.DTO.EF.OrderMaster ref_OrderMaster, DataTable ref_OrderDetails)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {

                    var itemToUpdate = context.OrderMaster.SingleOrDefault(x => x.Id == ref_OrderMaster.Id);
                    if (itemToUpdate != null)
                    {
                        #region [ADO]
                        //context.Entry(itemToUpdate).CurrentValues.SetValues(ref_OrderMaster);
                        //SqlConnection sqlconn = new SqlConnection(context.Database.Connection.ConnectionString);
                        //SqlCommand command = new SqlCommand("usp_OrderMasterDetails_Update", sqlconn);
                        //command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@idMaster", ref_OrderMaster.Id);
                        //command.Parameters.AddWithValue("@orderDate", ref_OrderMaster.OrderDate);
                        //command.Parameters.AddWithValue("@customer_Ref", ref_OrderMaster.Customer_Ref);
                        //command.Parameters.AddWithValue("@udt_orderDetailsListUpdate", ref_OrderDetails);
                        //sqlconn.Open();
                        //command.ExecuteNonQuery();
                        //context.SaveChanges();
                        //sqlconn.Close();
                        //context.SaveChanges();
                        #endregion

                        var orderDetails = new SqlParameter("@udt_orderDetailsListUpdate", SqlDbType.Structured);
                        orderDetails.Value = ref_OrderDetails;
                        orderDetails.TypeName = "dbo.udt_OrderDetailsList_Update";

                        var idMaster = new SqlParameter("@idMaster", SqlDbType.Int);
                        idMaster.Value = ref_OrderMaster.Id;

                        var orderDate = new SqlParameter("@orderDate", SqlDbType.DateTime);
                        orderDate.Value = ref_OrderMaster.OrderDate;

                        var orderCode = new SqlParameter("@orderCode", SqlDbType.Int);
                        orderCode.Value = ref_OrderMaster.OrderCode;

                        var ref_Customer = new SqlParameter("@customer_Ref", SqlDbType.Int);
                        ref_Customer.Value = ref_OrderMaster.Customer_Ref;


                        context.Database.ExecuteSqlCommand("exec dbo.usp_OrderMasterDetails_Update @idMaster,@orderCode,@orderDate,@customer_Ref,@udt_orderDetailsListUpdate", idMaster,orderCode , orderDate, ref_Customer, orderDetails);
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

        #region [-InsertOrderMasterDetails(DomainModels.DTO.EF.OrderMaster ref_OrderMaster, DataTable ref_OrderDetails)-]
        public void InsertOrderMasterDetails(DomainModels.DTO.EF.OrderMaster ref_OrderMaster, DataTable ref_OrderDetails)
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    #region [ADO]
                    // // ref_OrderDetails.ForEach(x => orderDetails.Rows.Add(x));    
                    // SqlConnection sqlconn = new SqlConnection(context.Database.Connection.ConnectionString);
                    // SqlCommand command = new SqlCommand("dbo.usp_OrderMasterDetails_Insert", sqlconn);
                    // command.CommandType = CommandType.StoredProcedure;
                    // command.Parameters.AddWithValue("@udt_orderDetailsList", ref_OrderDetails);
                    //// command.Parameters.AddWithValue("@idMaster",ref_OrderMaster.Id);
                    // command.Parameters.AddWithValue("@orderDate", ref_OrderMaster.OrderDate);
                    // command.Parameters.AddWithValue("@customer_Ref", ref_OrderMaster.Customer_Ref);

                    // sqlconn.Open();
                    // command.ExecuteNonQuery();
                    // context.SaveChanges();
                    // sqlconn.Close();
                    // context.SaveChanges(); 
                    #endregion

                    var orderDetails = new SqlParameter("@udt_orderDetailsList", SqlDbType.Structured);
                    orderDetails.Value = ref_OrderDetails;
                    orderDetails.TypeName = "dbo.udt_OrderDetailsList";

                    var orderDate = new SqlParameter("@orderDate", SqlDbType.DateTime);
                    orderDate.Value = ref_OrderMaster.OrderDate;

                    var orderCode = new SqlParameter("@orderCode", SqlDbType.Int);
                    orderCode.Value = ref_OrderMaster.OrderCode;

                    var ref_Customer = new SqlParameter("@customer_Ref", SqlDbType.Int);
                    ref_Customer.Value = ref_OrderMaster.Customer_Ref;


                    context.Database.ExecuteSqlCommand("exec dbo.usp_OrderMasterDetails_Insert @orderCode,@udt_orderDetailsList,@orderDate,@customer_Ref",
                        orderCode,orderDetails, orderDate, ref_Customer);
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

        #region [-SelectOrderDetailsGivenOrdeMasterId(DomainModels.DTO.EF.OrderMaster ref_OrderMaster)-]
        public dynamic  SelectOrderDetailsGivenOrdeMasterId(DomainModels.DTO.EF.OrderMaster ref_OrderMaster)
        {

            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {


                    #region ADO.NET
                    //SqlConnection sqlConn = new SqlConnection(context.Database.Connection.ConnectionString);
                    //SqlCommand command = new SqlCommand("usp_GetOrderDetailsGivenOrderMasterId", sqlConn);
                    //command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@orderMasterId", ref_OrderMaster.Id);
                    //SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                    //DataTable orderDetails = new DataTable();
                    //sqlDA.Fill(orderDetails);
                    //return orderDetails; 
                    #endregion

                    var q = context.usp_GetOrderDetailsGivenOrderMasterId(ref_OrderMaster.Id).ToList();
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

        #region [-int GenerateCode()-]
        public int GenerateCode()
        {

            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {

                    var orderCode = new SqlParameter();
                    orderCode.ParameterName = "orderCode";
                    orderCode.SqlDbType = SqlDbType.Int;
                    orderCode.Direction = ParameterDirection.Output;

                    context.Database.ExecuteSqlCommand("exec dbo.usp_GenerateCodeForOrder @orderCode output", orderCode);

                    var result = (int)orderCode.Value;
                    return result;

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

        #region [-SelectCustomerNameGivenId(DomainModels.DTO.EF.Customer ref_Customer)-]
        public string SelectCustomerNameGivenId(DomainModels.DTO.EF.Customer ref_Customer)
        {

            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {

                    var id = new SqlParameter("id", SqlDbType.Int);
                    id.Value = ref_Customer.Id;

                    var firstName = new SqlParameter();
                    firstName.ParameterName = "firstName";
                    firstName.SqlDbType = SqlDbType.NVarChar;
                    //size is important in nvarchar and varchar
                    firstName.Size = 60;
                    firstName.Direction = ParameterDirection.Output;

                    var lastName = new SqlParameter();
                    lastName.ParameterName = "lastName";
                    lastName.SqlDbType = SqlDbType.NVarChar;
                    lastName.Size = 60;
                    lastName.Direction = ParameterDirection.Output;

                    context.Database.ExecuteSqlCommand("exec dbo.usp_SelectCustomerNameGivenId @id, @firstName output,@lastName output", id, firstName,lastName);

                    var result = (string)firstName.Value + " "+ (string)lastName.Value;
                    return result;

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

        #region [-SelectProductNameGivenId(DomainModels.DTO.EF.Product ref_Product)-]
        public string SelectProductNameGivenId(DomainModels.DTO.EF.Product ref_Product)
        {

            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {
                    

                    var id = new SqlParameter("id", SqlDbType.Int);
                    id.Value = ref_Product.Id;

                    var productName = new SqlParameter();
                    productName.ParameterName = "productName";
                    productName.SqlDbType = SqlDbType.NVarChar;
                    //size is important in nvarchar and varchar
                    productName.Size = 60;
                    productName.Direction = ParameterDirection.Output;

                   context.Database.ExecuteSqlCommand("exec dbo.usp_SelectProductNameGivenId @id, @productName output", id, productName);

                    var result = (string)productName.Value;
                    return result;

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

        #region [-dynamic SelectCustomerInfo()-]
        public dynamic SelectCustomerInfo()
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {

                    var q = context.usp_SelectCustomerInfo().ToList();
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

        #region [-dynamic SelectProductInfo()-]
        public dynamic SelectProductInfo()
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {

                    var q = context.usp_SelectProductInfo().ToList();
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

        #region [-dynamic SelectCategoryCode()-]
        public dynamic SelectCategoryCode()
        {
            using (var context = new DomainModels.DTO.EF.OnlineShopEntities())
            {
                try
                {

                    var q = context.usp_SelectCategoryCode().ToList();
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


    }


}

