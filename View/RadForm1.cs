using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI;


//each Methodes and actions are surrounded with rpv[Name of rad page view]/rpvPage[Name of the page]/btn & gv & cmb
//independent methodes are in the block [-Methodes-]




namespace View
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        public RadForm1()
        {
            InitializeComponent();
            Ref_ProductViewModel = new ViewModel.ProductViewModel();
            Ref_ProductCategoryViewModel = new ViewModel.ProductCategoryViewModel();
            Ref_CustomerViewModel = new ViewModel.CustomerViewModel();
            Ref_OrderMasterViewModel = new ViewModel.OrderMasterViewModel();
        }
        #region [-props-]
        public ViewModel.ProductViewModel Ref_ProductViewModel { get; set; }
        public ViewModel.ProductCategoryViewModel Ref_ProductCategoryViewModel { get; set; }
        public ViewModel.CustomerViewModel Ref_CustomerViewModel { get; set; }
        public ViewModel.OrderMasterViewModel Ref_OrderMasterViewModel { get; set; } 
        #endregion


        #region [-RadForm1_Load-]
        private void RadForm1_Load(object sender, EventArgs e)
        {
            this.gvOrderMaster.AllowSearchRow = true;
            rpvOrder.Pages.Remove(rpvPageOrderSubmit);
            rpvOrder.Pages.Remove(rpvPageOrderMasterDetails);;
           // InitializeComboBoxes();
           // InitializeGridViews();
    
        }
        #endregion



        #region [-rpvMenu-]

        #region [-rpvPageProduct-]

        #region [-btnSaveProduct_Click-]
        private void btnSaveProduct_Click(object sender, EventArgs e)
        {

            if (txtProductName.Text != "" && txtProductDescription.Text != "" && txtProductUnitPrice.Text != "" && listProductCategory.Text != "")
            {
                Ref_ProductViewModel.Save(txtProductName.Text, txtProductDescription.Text, Convert.ToDecimal(txtProductUnitPrice.Text), Convert.ToInt32(listProductCategory.SelectedValue));

                MessageBox.Show(string.Format(".کالای جدید ذخیره شد"), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.productTableAdapter.Fill(onlineShopDataSet.Product);

                gvProduct.DataSource = Ref_ProductViewModel.FillGrid();

                txtProductName.Text = "";
                txtProductDescription.Text = "";
                txtProductUnitPrice.Text = "";
                listProductCategory.Text = "";



            }
            else
            {
                MessageBox.Show("خطا! ابتدا تمام مشخصات کالا را مشخص کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

        }
        #endregion

        #region [-btnEditProduct_Click-]
        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            var selectedRows = gvProduct.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای ویرایش انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnEditProduct.Enabled = true;



                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int id = Convert.ToInt32(gvProduct.SelectedRows[i].Cells[0].Value);



                    DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کد {0}  ویرایش شود؟", id), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        Ref_ProductViewModel.Edit(id, txtProductName.Text, txtProductDescription.Text, Convert.ToDecimal(txtProductUnitPrice.Text), Convert.ToInt32(listProductCategory.SelectedValue));
                        MessageBox.Show(string.Format("کد {0} تغییر یافت", id), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                //this.productTableAdapter.Fill(onlineShopDataSet.Product);

                gvProduct.DataSource = Ref_ProductViewModel.FillGrid();

            }
        }
        #endregion

        #region [-btnRefreshProduct_Click-]
        private void btnRefreshProduct_Click(object sender, EventArgs e)
        {
            //DataTable Product = new DataTable();
            //Product = Ref_ProductViewModel.FillGrid();
            //foreach (DataRow row in Product.Rows)
            //{

            //    gvProduct.Rows.Add(row.ItemArray);

            //}
            gvProduct.DataSource = Ref_ProductViewModel.FillGrid();
        }
        #endregion

        #region [-btnDeleteProduct_Click-]
        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            var selectedRows = gvProduct.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای حذف انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnDeleteProduct.Enabled = true;

                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int id = Convert.ToInt32(gvProduct.SelectedRows[i].Cells[0].Value);

                    DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کد {0}  حذف شود؟", id), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        Ref_ProductViewModel.Delete(id);
                        MessageBox.Show(string.Format("کالا با کد {0}  حذف شد", id), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            //this.productTableAdapter.Fill(onlineShopDataSet.Product);

            gvProduct.DataSource = Ref_ProductViewModel.FillGrid();
        }
        #endregion

        #region [-gvProduct_SelectionChanged-]
        private void gvProduct_SelectionChanged(object sender, EventArgs e)
        {
            txtProductCode.Text = gvProduct.SelectedRows[0].Cells[1].Value.ToString();
            txtProductName.Text = gvProduct.SelectedRows[0].Cells[2].Value.ToString();
            txtProductDescription.Text = gvProduct.SelectedRows[0].Cells[3].Value.ToString();
            txtProductUnitPrice.Text = gvProduct.SelectedRows[0].Cells[4].Value.ToString();
            listProductCategory.Text = gvProduct.SelectedRows[0].Cells[5].Value.ToString();
        }
        #endregion

        #region [-listProductCategory_Click-]
        private void listProductCategory_Click(object sender, EventArgs e)
        {
            listProductCategory.DataSource = Ref_OrderMasterViewModel.GetCategoryCodeList();
            listProductCategory.DisplayMember = "CategoryCode";
            listProductCategory.ValueMember = "Id";

        }
        #endregion


        #endregion

        #region [-rpvPageProductCategory-]

        #region [-btnSaveCategory_Click-]
        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text != "")
            {
                Ref_ProductCategoryViewModel.Save(txtCategoryName.Text);

                MessageBox.Show(string.Format(".گروه کالای جدید ذخیره شد"), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.productCategoryTableAdapter.Fill(onlineShopDataSet1.ProductCategory);
                gvProductCategory.DataSource = Ref_ProductCategoryViewModel.FillGrid();


                foreach (Telerik.WinControls.UI.GridViewRowInfo item in gvProductCategory.Rows)
                {
                    if (item.Index == gvProductCategory.RowCount - 1)
                    {
                        item.IsSelected = true;

                    }
                }
                txtCategoryName.Text = " ";




            }
            else
            {
                MessageBox.Show("خطا! ابتدا نام گروه کالا را مشخص کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        #endregion

        #region [-btnDeleteCategory_Click-]
        private void btnDeleteCategory_Click(object sender, EventArgs e)

        {
            var selectedRows = gvProductCategory.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای حذف انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnDeleteCategory.Enabled = true;



                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int id = Convert.ToInt32(gvProductCategory.SelectedRows[i].Cells[0].Value);

                    DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کد {0}  حذف شود؟", id), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {
                        // e.Cancel = true;
                        Ref_ProductCategoryViewModel.Delete(id);
                        MessageBox.Show(string.Format("کد {0}  حذف شد", id), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }


            }

            // this.productCategoryTableAdapter.Fill(onlineShopDataSet1.ProductCategory);
            gvProductCategory.DataSource = Ref_ProductCategoryViewModel.FillGrid();



        }
        #endregion

        #region [-btnRefreshCategory_Click-]
        private void btnRefreshCategory_Click(object sender, EventArgs e)
        {

            //DataTable ProductCategory = new DataTable();
            //ProductCategory = Ref_ProductCategoryViewModel.FillGrid();
            //foreach (DataRow row in ProductCategory.Rows)
            //{

            //    gvProductCategory.Rows.Add(row.ItemArray);

            //}

            gvProductCategory.DataSource = Ref_ProductCategoryViewModel.FillGrid();

        }
        #endregion

        #region [-btnEditCategory_Click-]
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            var selectedRows = gvProductCategory.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای ویرایش انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnEditCategory.Enabled = true;



                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int id = Convert.ToInt32(gvProductCategory.SelectedRows[i].Cells[0].Value);
                    //int categoryCode = Convert.ToInt32(gvProductCategory.SelectedRows[i].Cells[1].Value);


                    DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کد {0}  ویرایش شود؟", id), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        Ref_ProductCategoryViewModel.Edit(id, txtCategoryName.Text);
                        MessageBox.Show(string.Format("کد {0} تغییر یافت", id), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                // this.productCategoryTableAdapter.Fill(onlineShopDataSet1.ProductCategory);
                gvProductCategory.DataSource = Ref_ProductCategoryViewModel.FillGrid();


            }
        }
        #endregion

        #region [-gvProductCategory_SelectionChanged-]
        private void gvProductCategory_SelectionChanged(object sender, EventArgs e)
        {
            txtCategoryCode.Text = gvProductCategory.SelectedRows[0].Cells[1].Value.ToString();
            txtCategoryName.Text = gvProductCategory.SelectedRows[0].Cells[2].Value.ToString();
        }
        #endregion


        #endregion

        #region [-rpvPageCustomer-]

        #region [-btnSaveCustomer_Click-]
        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {

            if (txtCustomerFirstName.Text != "" && txtCustomerLastName.Text != "" && txtCustomerCity.Text != "" && txtCustomerCountry.Text != "" && txtCustomerRegion.Text != "" && txtCustomerPostalCode.Text != "" && txtCustomerPhone.Text != "" && txtCustomerAdrs.Text != "" && txtCustomerEmail.Text != "")
            {
                Ref_CustomerViewModel.Save(txtCustomerFirstName.Text, txtCustomerLastName.Text, txtCustomerAdrs.Text, txtCustomerCity.Text, txtCustomerRegion.Text, txtCustomerCountry.Text, txtCustomerPostalCode.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);

                MessageBox.Show(string.Format(".مشتری جدید ذخیره شد"), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

                gvCustomer.DataSource = Ref_CustomerViewModel.FillGrid();



                foreach (Telerik.WinControls.UI.GridViewRowInfo item in gvCustomer.Rows)
                {
                    if (item.Index == gvCustomer.RowCount - 1)
                    {
                        item.IsSelected = true;

                    }
                }


            }
            else
            {
                MessageBox.Show("خطا! ابتدا مشخصات مشتری باید مشخص شود");

            }

        }

        #endregion

        #region [-btnDeleteCustomer_Click-]
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {

            var selectedRows = gvCustomer.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای حذف انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnDeleteCustomer.Enabled = true;

                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int id = Convert.ToInt32(gvCustomer.SelectedRows[i].Cells[0].Value);

                    DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کد {0}  حذف شود؟", id), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        Ref_CustomerViewModel.Delete(id);
                        MessageBox.Show(string.Format("مشتری با کد {0}  حذف شد", id), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            gvCustomer.DataSource = Ref_CustomerViewModel.FillGrid();

        }

        #endregion

        #region [-btnRefreshCustomer_Click-]
        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {

            gvCustomer.DataSource = Ref_CustomerViewModel.FillGrid();
            //DataTable Customer = new DataTable();
            //Customer = Ref_CustomerViewModel.FillGrid();
            //foreach (DataRow row in Customer.Rows)
            //{

            //    gvCustomer.Rows.Add(row.ItemArray);

            //}
        }


        #endregion

        #region [-btnEditCustomer_Click-]
        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            var selectedRows = gvCustomer.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای ویرایش انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnEditCustomer.Enabled = true;



                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int id = Convert.ToInt32(gvCustomer.SelectedRows[i].Cells[0].Value);



                    DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کد {0}  ویرایش شود؟", id), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        Ref_CustomerViewModel.Edit(id, txtCustomerFirstName.Text, txtCustomerLastName.Text, txtCustomerAdrs.Text, txtCustomerCity.Text, txtCustomerRegion.Text, txtCustomerCountry.Text, txtCustomerPostalCode.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);
                        MessageBox.Show(string.Format("کد {0} تغییر یافت", id), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }

                gvCustomer.DataSource = Ref_CustomerViewModel.FillGrid();
            }
        }

        #endregion

        #region [-gvCustomer_SelectionChanged-]
        private void gvCustomer_SelectionChanged(object sender, EventArgs e)
        {

            txtCustomerCode.Text = gvCustomer.SelectedRows[0].Cells[1].Value.ToString();
            txtCustomerFirstName.Text = gvCustomer.SelectedRows[0].Cells[2].Value.ToString();
            txtCustomerLastName.Text = gvCustomer.SelectedRows[0].Cells[3].Value.ToString();
            txtCustomerAdrs.Text = gvCustomer.SelectedRows[0].Cells[4].Value.ToString();
            txtCustomerCountry.Text = gvCustomer.SelectedRows[0].Cells[5].Value.ToString();
            txtCustomerCity.Text = gvCustomer.SelectedRows[0].Cells[6].Value.ToString();
            txtCustomerRegion.Text = gvCustomer.SelectedRows[0].Cells[7].Value.ToString();
            txtCustomerPostalCode.Text = gvCustomer.SelectedRows[0].Cells[8].Value.ToString();
            txtCustomerPhone.Text = gvCustomer.SelectedRows[0].Cells[9].Value.ToString();
            txtCustomerEmail.Text = gvCustomer.SelectedRows[0].Cells[10].Value.ToString();
        }







        #endregion

        #endregion

        #endregion



        #region [-rpvOrder-]

        #region [-rpvPageOrderMasterList-]

        #region [btnListOrderIdSearch]
        //#region [-listOrderId_SelectedIndexChanged-]
        //private void listOrderId_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{

        //    string searchedStr = listOrderId.SelectedValue.ToString();
        //    for (int r = 0; r < gvOrderMaster.RowCount; r++)
        //    {
        //        if (gvOrderMaster.Rows[r].Cells[0].Value.ToString().ToUpper().Equals(searchedStr.ToUpper()))
        //        {

        //            gvOrderMaster.Rows[r].IsSelected = true;
        //            gvOrderMaster.Rows[r].IsExpanded = true;

        //        }
        //    }

        //}
        //#endregion 
        #endregion

        #region [-gvOrderMaster_CellDoubleClick-]
        private void gvOrderMaster_CellDoubleClick(object sender, GridViewCellEventArgs e)

        {   //clearing txtboxes
            ClearPageOrderMasterDetails();
            //btnVisibilty
            btnEditTemporaryOrder.Visible = true;
            btnSaveTemporaryOrder.Visible = false;
            btnOrderDetailsSave.Visible = false;
            btnOrderDetailsDelete.Visible = false;
            btnOrderSaveSubmit.Visible = false;
            btnOrderEditSubmit.Visible = true;
            btnCancel.Visible = true;
            
            //txtEnabling
            EnableOrderMasterListPageTxtBoxes(false);
            txtOrderCode.ReadOnly = true;
            //adding tabs
            while (rpvOrder.Pages.Count > 1)
            {
                rpvOrder.Pages.Remove(rpvPageOrderMasterDetails);
            }

            rpvOrder.Pages.Add(rpvPageOrderMasterDetails);
            rpvOrder.SelectedPage = rpvPageOrderMasterDetails;

            //filling blanks with order master 
            #region [multipleRows]
            //var selectedRows = gvOrderMaster.SelectedRows.ToArray();

            ////filling blanks with details of order
            //for (int i = 0; i < selectedRows.Length; i++)
            //{
            //    txtOrderId.Text = gvOrderMaster.SelectedRows[i].Cells[0].Value.ToString();
            //    dateTimePickerOrder.Value = Convert.ToDateTime(gvOrderMaster.SelectedRows[i].Cells[1].Value);
            //    listCustomerCode.Text = gvOrderMaster.SelectedRows[i].Cells[3].Value.ToString();

            //} 
            #endregion

            txtOrderCode.Text = gvOrderMaster.SelectedRows[0].Cells[1].Value.ToString();
            dateTimePickerOrder.Value = Convert.ToDateTime(gvOrderMaster.SelectedRows[0].Cells[2].Value);
            cmbCustomer.Text = gvOrderMaster.SelectedRows[0].Cells[4].Value.ToString();
            cmbCustomer.SelectedValue = gvOrderMaster.SelectedRows[0].Cells[3].Value;

            //filling grid view temporary order details with original one
            ClearGridView(gvOrderDetailsTemporary);
            for (int i = 0; i < gvorderDetailsList.RowCount; i++)
            {
                gvOrderDetailsTemporary.Rows.Add(gvorderDetailsList.Rows[i].Cells[0].Value, gvorderDetailsList.Rows[i].Cells[1].Value, gvorderDetailsList.Rows[i].Cells[2].Value, gvorderDetailsList.Rows[i].Cells[3].Value, gvorderDetailsList.Rows[i].Cells[4].Value, gvorderDetailsList.Rows[i].Cells[5].Value, gvorderDetailsList.Rows[i].Cells[6].Value, gvorderDetailsList.Rows[i].Cells[7].Value);
            }



        }
        #endregion

        #region [-btnAddNewOrder_Click-]
        private void btnAddNewOrder_Click(object sender, EventArgs e)
        {
            //btnVisibilty
            btnOrderDetailsSave.Visible = true;
            btnEditTemporaryOrder.Visible = false;
            btnSaveTemporaryOrder.Visible = true;
            btnOrderDetailsDelete.Visible = true;
            btnOrderSaveSubmit.Visible = true;
            btnOrderEditSubmit.Visible = false;
            btnCancel.Visible = true;
            //txtEnability
            EnableOrderMasterListPageTxtBoxes(true);
            txtOrderCode.ReadOnly = false;
            //clearing
            ClearPageOrderMasterDetails();
            ClearGridView(gvOrderDetailsTemporary);
            //tabbing
            while (rpvOrder.Pages.Count > 1)
            {
                rpvOrder.Pages.Remove(rpvPageOrderMasterDetails);
            }
            rpvOrder.Pages.Add(rpvPageOrderMasterDetails);
            rpvOrder.SelectedPage = rpvPageOrderMasterDetails;
            //generateCode
            txtOrderCode.Text=Ref_OrderMasterViewModel.GetOrderCode().ToString();
        }
        #endregion

        #region [-btnDeleteSelectedOrders_Click-]
        private void btnDeleteSelectedOrders_Click(object sender, EventArgs e)
        {
            var selectedRows = gvOrderMaster.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای حذف انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //btnDeleteProduct.Enabled = true;

                for (int i = 0; i < selectedRows.Length; i++)
                {
                    int id = Convert.ToInt32(gvOrderMaster.SelectedRows[i].Cells[0].Value);
                    int code = Convert.ToInt32(gvOrderMaster.SelectedRows[i].Cells[1].Value);

                    DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کد {0}  حذف شود؟ با حذف این سفارش تمامی جزئیات اقلام سفارش حذف خواهند شد", id), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        Ref_OrderMasterViewModel.Delete(id);
                        MessageBox.Show(string.Format("سفارش با کد {0}  حذف شد", code), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            //this.orderMasterTableAdapter1.Fill(this.onlineShopDataSet13.OrderMaster);
        }
        #endregion

        #region [-gvOrderMaster_CellClick-]
        private void gvOrderMaster_CellClick(object sender, GridViewCellEventArgs e)
        {
            //other way is implementin datatable: 
            #region [useing DataTable]

            //ClearGridView(gvorderDetailsList);
            //int id = Convert.ToInt32(gvOrderMaster.SelectedRows[0].Cells[0].Value);
            //DataTable orderDetails = new DataTable();
            //orderDetails = Ref_OrderMasterViewModel.GetOrderDetailsGrid(id);
            //foreach (DataRow row in orderDetails.Rows)
            //{
            //    gvorderDetailsList.Rows.Add(row.ItemArray);
            //} 
            #endregion

            int id = Convert.ToInt32(gvOrderMaster.SelectedRows[0].Cells[0].Value);
            gvorderDetailsList.DataSource = Ref_OrderMasterViewModel.GetOrderDetailsGrid(id);


        }
        #endregion

        #region [-btnRefreshOrderMaster_Click-]
        private void btnRefreshOrderMaster_Click(object sender, EventArgs e)
        {   //one way is using datatable :
            #region [using datatable]
            //DataTable OrderMasters = new DataTable();
            //OrderMasters = Ref_OrderMasterViewModel.FillGrid();
            //foreach (DataRow row in OrderMasters.Rows)
            //{

            //    gvOrderMaster.Rows.Add(row.ItemArray);

            //} 
            #endregion

            gvOrderMaster.DataSource = Ref_OrderMasterViewModel.FillGrid();

        }
        #endregion

        #endregion

        #region [-rpvPageOrderMasterDetails-]

        #region [-btnSaveTemporaryOrder_Click-]
        private void btnSaveTemporaryOrder_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.Text != "" && txtOrderCode.Text!="" )

            {
                if (CheckCodeValidation(Convert.ToInt32(txtOrderCode.Text), gvOrderMaster, 1))
                {
                    if (gvOrderDetailsTemporary.RowCount != 0)
                    {
                        while (rpvOrder.Pages.Count > 2)
                        {
                            rpvOrder.Pages.Remove(rpvPageOrderSubmit);
                        }
                        rpvOrder.Pages.Add(rpvPageOrderSubmit);
                        ClearGridView(gvOrderSubmit);
                        rpvOrder.SelectedPage = rpvPageOrderSubmit;

                        gvOrderSubmit.Rows.Add(txtOrderCode.Text, CalculateTotalCost(), txtOrderCustomerName.Text);
                    }
                    else
                    {
                        MessageBox.Show("خطا! سفارش  باید شامل اقلام سفارش باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    txtOrderCode.Text = Ref_OrderMasterViewModel.GetOrderCode().ToString();
                }
            }

            else
            {
                MessageBox.Show("خطا! ابتدا تمام مشخصات سفارش را مشخص کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        #endregion

        #region [-btnEditTemporaryOrder_Click-]
        private void btnEditTemporaryOrder_Click(object sender, EventArgs e)
        {

            if (cmbCustomer.Text != "" && txtOrderCode.Text != "")
            {//checkcodevalidation
                if (true)
                {
                    if (gvOrderDetailsTemporary.RowCount != 0)
                    {
                        while (rpvOrder.Pages.Count > 2)
                        {
                            rpvOrder.Pages.Remove(rpvPageOrderSubmit);
                        }
                        rpvOrder.Pages.Add(rpvPageOrderSubmit);
                        ClearGridView(gvOrderSubmit);
                        rpvOrder.SelectedPage = rpvPageOrderSubmit;

                        gvOrderSubmit.Rows.Add(txtOrderCode.Text, CalculateTotalCost(), txtOrderCustomerName.Text);
                    }
                    else
                    {
                        MessageBox.Show("خطا! سفارش  باید شامل اقلام سفارش باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    txtOrderCode.Text = gvOrderMaster.SelectedRows[0].Cells[1].Value.ToString();
                }
            }

            else
            {
                MessageBox.Show("خطا! ابتدا تمام مشخصات سفارش را مشخص کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        #endregion

        #region [-btnOrderDetailsSave_Click_1-]
        private void btnOrderDetailsSave_Click_1(object sender, EventArgs e)
        {
            bool errorFlag = false;

            if (cmbProduct.Text != "" && txtUnitPrice.Text != "" && txtDiscount.Text != "" && txtTaxRate.Text != "" && txtQuantity.Text != "")
            {
                #region [ForAddingNewRow]

                //ClearGridView(gvOrderDetailsTemporary);

                //GridViewDataRowInfo rowInfoFromOldOrders = new GridViewDataRowInfo(this.gvOrderDetailsTemporary.MasterView);
                //foreach (GridViewDataRowInfo row in gvorderDetailsList.Rows)
                //{

                //    rowInfoFromOldOrders.Cells[1].Value = row.Cells[1].Value;
                //    rowInfoFromOldOrders.Cells[2].Value = row.Cells[2].Value;
                //    rowInfoFromOldOrders.Cells[3].Value = row.Cells[3].Value;
                //    rowInfoFromOldOrders.Cells[4].Value = row.Cells[4].Value;
                //    rowInfoFromOldOrders.Cells[7].Value = row.Cells[7].Value;
                //    rowInfoFromOldOrders.Cells[5].Value = row.Cells[5].Value;
                //    rowInfoFromOldOrders.Cells[6].Value = row.Cells[6].Value;
                //    gvOrderDetailsTemporary.Rows.Add(rowInfoFromOldOrders);
                //    rowInfoFromOldOrders.IsSelected = false;
                //    rowInfoFromOldOrders.IsCurrent = false;
                //} 
                #endregion
                //checking nonrepeated product
                for (int i = 0; i < gvOrderDetailsTemporary.RowCount; i++)
                {
                    if (cmbProduct.Text == gvOrderDetailsTemporary.Rows[i].Cells[7].Value.ToString())
                    {

                        errorFlag = true;
                        break;
                    }

                }
                if (errorFlag)
                {
                    MessageBox.Show("خطا! این کالا قبلا انتخاب شده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    GridViewDataRowInfo rowInfo = new GridViewDataRowInfo(this.gvOrderDetailsTemporary.MasterView);

                    rowInfo.Cells[1].Value = txtUnitPrice.Text;
                    rowInfo.Cells[2].Value = txtDiscount.Text;
                    rowInfo.Cells[3].Value = txtTaxRate.Text;
                    rowInfo.Cells[4].Value = txtQuantity.Text;
                    rowInfo.Cells[7].Value = cmbProduct.Text;
                    rowInfo.Cells[5].Value = cmbProduct.SelectedValue;
                    gvOrderDetailsTemporary.Rows.Add(rowInfo);
                }

                cmbProduct.Text = "";
                txtOrderProductName.Text = "";
                txtUnitPrice.Text = "";
                txtDiscount.Text = "";
                txtTaxRate.Text = "";
                txtQuantity.Text = "";



            }
            else
            {
                MessageBox.Show("خطا! ابتدا تمام مشخصات کالای سفارشی را مشخص کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        #endregion

        #region [-btnOrderDetailsEdit_Click_1-]
        private void btnOrderDetailsEdit_Click_1(object sender, EventArgs e)
        {
            var selectedRows = gvOrderDetailsTemporary.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای ویرایش انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(cmbProduct.Text!=""&& txtUnitPrice.Text != "" && txtDiscount.Text != "" && txtTaxRate.Text != "" && txtQuantity.Text != "")
            {
                gvOrderDetailsTemporary.SelectedRows[0].Cells[7].Value = cmbProduct.Text;
                gvOrderDetailsTemporary.SelectedRows[0].Cells[1].Value = txtUnitPrice.Text;
                gvOrderDetailsTemporary.SelectedRows[0].Cells[2].Value = txtDiscount.Text;
                gvOrderDetailsTemporary.SelectedRows[0].Cells[3].Value = txtTaxRate.Text;
                gvOrderDetailsTemporary.SelectedRows[0].Cells[4].Value = txtQuantity.Text;
                gvOrderDetailsTemporary.SelectedRows[0].Cells[5].Value = cmbProduct.SelectedValue.ToString();
            }
            else
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای ویرایش انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region [-btnOrderDetailsDelete_Click-]
        private void btnOrderDetailsDelete_Click(object sender, EventArgs e)
        {
            var selectedRows = gvOrderDetailsTemporary.SelectedRows.ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("خطا! ابتدا یک مورد را برای حذف انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(string.Format("آیا میخواهید کالای سفارشی با کد {0}  حذف شود؟", gvOrderDetailsTemporary.SelectedRows[0].Cells[7].Value.ToString()), "اخطار", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {

                    int index = gvOrderDetailsTemporary.SelectedRows[0].Index;
                    gvOrderDetailsTemporary.Rows.RemoveAt(index);
                }
                
            }
        }
        #endregion

        #region [-gvOrderDetailsTemporary_CellClick-]
        private void gvOrderDetailsTemporary_CellClick(object sender, GridViewCellEventArgs e)

        {
            EnableOrderMasterListPageTxtBoxes(true);
            cmbProduct.Text = gvOrderDetailsTemporary.SelectedRows[0].Cells[7].Value.ToString();

            cmbProduct.SelectedValue = gvOrderDetailsTemporary.SelectedRows[0].Cells[5].Value;

            txtUnitPrice.Text = gvOrderDetailsTemporary.SelectedRows[0].Cells[1].Value.ToString();

            txtDiscount.Text = gvOrderDetailsTemporary.SelectedRows[0].Cells[2].Value.ToString();

            txtTaxRate.Text = gvOrderDetailsTemporary.SelectedRows[0].Cells[3].Value.ToString();

            txtQuantity.Text = gvOrderDetailsTemporary.SelectedRows[0].Cells[4].Value.ToString();

        }
        #endregion

        private void btnOrderAddCustomer_Click(object sender, EventArgs e)
        {
            // rpvMain.SelectedPage = rpvPageCustomer;
            // rpvMenu.SelectedPage = rpvPageCustomer;


        }

        private void btnOrderAddProduct_Click(object sender, EventArgs e)
        {

            // rpvMenu.SelectedPage = rpvPageProduct;
            // rpvMain.SelectedPage = rpvPageProduct;
        }

        #region [-cmbCustomer_SelectedValueChanged-]
        private void cmbCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            GridViewDataRowInfo selectedRow = (GridViewDataRowInfo)cmbCustomer.SelectedItem;
            txtOrderCustomerName.Text = string.Format(selectedRow.Cells[2].Value.ToString() + " " + selectedRow.Cells[3].Value.ToString());
        }
        #endregion

        #region [-cmbProduct_SelectedValueChanged-]
        private void cmbProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            GridViewDataRowInfo selectedRow = (GridViewDataRowInfo)cmbProduct.SelectedItem;
            txtOrderProductName.Text = string.Format(selectedRow.Cells[2].Value.ToString());
            txtUnitPrice.Text = selectedRow.Cells[4].Value.ToString();
        }
        #endregion

        #endregion

        #region [-rpvPageOrderSubmit-]

        #region [-btnOrderSaveSubmit_Click-]
        private void btnOrderSaveSubmit_Click(object sender, EventArgs e)
        {

            DataTable orderDetails = new DataTable();
            orderDetails.Columns.Add("UnitPrice", typeof(decimal));
            orderDetails.Columns.Add("Discount", typeof(decimal));
            orderDetails.Columns.Add("TaxRate", typeof(decimal));
            orderDetails.Columns.Add("Quantity", typeof(Int32));
            orderDetails.Columns.Add("Product_Ref", typeof(Int32));
            for (int i = 0; i < gvOrderDetailsTemporary.RowCount; i++)
            {
                orderDetails.Rows.Add(Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[1].Value), Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[2].Value), Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[3].Value), Convert.ToInt32(gvOrderDetailsTemporary.Rows[i].Cells[4].Value), Convert.ToInt32(gvOrderDetailsTemporary.Rows[i].Cells[5].Value));
            }

            Ref_OrderMasterViewModel.Save(Convert.ToInt32(txtOrderCode.Text), dateTimePickerOrder.Value, Convert.ToInt32(cmbCustomer.SelectedValue), orderDetails);
            MessageBox.Show(string.Format("سفارش ثبت شد"), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnOrderSaveSubmit.Visible = false;
            btnCancel.Visible = false;

        }
        #endregion 

        #region [-btnOrderEditSubmit_Click-]
        private void btnOrderEditSubmit_Click(object sender, EventArgs e)
        {
            DataTable orderDetails = new DataTable();
            orderDetails.Columns.Add("Id", typeof(Int32));
            orderDetails.Columns.Add("UnitPrice", typeof(decimal));
            orderDetails.Columns.Add("Discount", typeof(decimal));
            orderDetails.Columns.Add("TaxRate", typeof(decimal));
            orderDetails.Columns.Add("Quantity", typeof(Int32));
            orderDetails.Columns.Add("Product_Ref", typeof(Int32));
            orderDetails.Columns.Add("OrderMaster_Ref", typeof(Int32));
            for (int i = 0; i < gvOrderDetailsTemporary.RowCount; i++)
            {

                orderDetails.Rows.Add(Convert.ToInt32(gvOrderDetailsTemporary.Rows[i].Cells[0].Value),
                    Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[1].Value),
                    Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[2].Value),
                    Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[3].Value),
                    Convert.ToInt32(gvOrderDetailsTemporary.Rows[i].Cells[4].Value),
                    Convert.ToInt32(gvOrderDetailsTemporary.Rows[i].Cells[5].Value),
                    Convert.ToInt32(gvOrderDetailsTemporary.Rows[i].Cells[6].Value));
            }

            Ref_OrderMasterViewModel.Edit(Convert.ToInt32(gvOrderMaster.SelectedRows[0].Cells[0].Value),
                Convert.ToInt32(txtOrderCode.Text), dateTimePickerOrder.Value, Convert.ToInt32(cmbCustomer.SelectedValue), orderDetails);
            MessageBox.Show(string.Format("سفارش ویرایش شد"), "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnOrderEditSubmit.Visible = false;
            btnCancel.Visible = false;
        }
        #endregion

        #region [-btnBack_Click-]
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (btnOrderEditSubmit.Visible == false || btnOrderSaveSubmit.Visible == false)
            {
                while (rpvOrder.Pages.Count > 1)
                {
                    rpvOrder.Pages.Remove(rpvPageOrderSubmit);
                    rpvOrder.Pages.Remove(rpvPageOrderMasterDetails);
                }
                rpvOrder.SelectedPage = rpvPageOrderMasterList;
            }
            else
            {
                rpvOrder.SelectedPage = rpvPageOrderMasterDetails;
            }
        }
        #endregion

        #region [-btnCancel_Click-]
        private void btnCancel_Click(object sender, EventArgs e)
        {
            while (rpvOrder.Pages.Count > 1)
            {
                rpvOrder.Pages.Remove(rpvPageOrderSubmit);
                rpvOrder.Pages.Remove(rpvPageOrderMasterDetails);
            }
            rpvOrder.SelectedPage = rpvPageOrderMasterList;
        }
        #endregion 
        #endregion

        #endregion




        #region [-Methodes-]

        #region [-ClearPageOrderMasterDetails()-]
        private void ClearPageOrderMasterDetails()
        {
            cmbCustomer.Text = "";
            txtOrderCustomerName.Text = "";
            txtOrderCode.Text = "";
            cmbProduct.Text = "";
            txtOrderProductName.Text = "";
            txtUnitPrice.Text = "";
            txtTaxRate.Text = "";
            txtQuantity.Text = "";
            txtDiscount.Text = "";
            ClearGridView(gvOrderDetailsTemporary);
        }
        #endregion


        #region [-ClearGridView(RadGridView gridView)-]
        private void ClearGridView(RadGridView gridView)
        {

            gridView.Rows.Clear();
        }
        #endregion

        #region [-CalculateTotalCost()-]
        private decimal CalculateTotalCost()
        {
            decimal totalCost = 0;

            for (int i = 0; i < gvOrderDetailsTemporary.RowCount; i++)
            {
                decimal unitPrice = Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[1].Value);
                decimal discount = Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[2].Value);
                decimal taxRate = Convert.ToDecimal(gvOrderDetailsTemporary.Rows[i].Cells[3].Value);
                int quantity = Convert.ToInt32(gvOrderDetailsTemporary.Rows[i].Cells[4].Value);
                decimal unitPriceWithDiscountTax = unitPrice - (unitPrice * (discount / 100)) + (unitPrice * (taxRate / 100));
                totalCost = totalCost + (unitPriceWithDiscountTax * quantity);
            }
            return totalCost;
        }
        #endregion

        #region [-InitializeGridViews()-]
        private void InitializeGridViews()
        {
            gvOrderMaster.DataSource = Ref_OrderMasterViewModel.FillGrid();

        } 
        #endregion

        #region [-InitializeComboBoxes()-]
        private void InitializeComboBoxes()
        {

            #region [cmbCustomer]
            cmbCustomer.DataSource = Ref_CustomerViewModel.FillGrid();
            cmbCustomer.DisplayMember = "CustomerCode";
            cmbCustomer.ValueMember = "Id";
            
            #endregion

            #region [cmbProduct]
            cmbProduct.DataSource = Ref_ProductViewModel.FillGrid();
            cmbProduct.DisplayMember = "ProductCode";
            cmbProduct.ValueMember = "Id";
            #endregion



        }
        #endregion

        #region [-EnableOrderMasterListPageTxtBoxes(bool enablity)-]
        private void EnableOrderMasterListPageTxtBoxes(bool enablity)
        {
            cmbProduct.Enabled = enablity;
            txtOrderProductName.Enabled = enablity;
            txtUnitPrice.Enabled = enablity;
            txtQuantity.Enabled = enablity;
            txtDiscount.Enabled = enablity;
            txtTaxRate.Enabled = enablity;
            btnOrderDetailsEdit.Enabled = enablity;
        }

        #endregion

        #region [-CheckCodeValidation(int code, RadGridView gridview, int indexOfColumn)-]
        private bool CheckCodeValidation(int code ,RadGridView gridview, int indexOfColumn)
        {
            bool permission = true;
            //gridview.DataSource = ref_object.FillGrid();
            for (int i = 0; i < gridview.RowCount; i++)
            {
                if (Convert.ToInt32(gridview.Rows[i].Cells[indexOfColumn].Value) == code)
                {
                    MessageBox.Show("خطا! این کد از قبل وجود دارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    permission = false;
                    break;
                }
            }
            return permission;
        }


        #endregion

        #endregion

        private void rpvMenu_SelectedPageChanged(object sender, EventArgs e)
        {

        }
    }
  
    } 
    

