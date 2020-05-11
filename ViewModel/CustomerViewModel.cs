using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CustomerViewModel
    {
        #region [-ctor-]
        public CustomerViewModel()
        {
            Ref_CustomerCrud = new Model.DomainModels.POCO.CustomerCrud();
        }
        #endregion
        //
        public Model.DomainModels.DTO.EF.Customer Ref_Customer { get; set; }
        public Model.DomainModels.POCO.CustomerCrud Ref_CustomerCrud { get; set; }

        #region [-Save(string firstName, string lastName, string adrs, string city, string region, string country, string postalcode, string phone, string email)-]
        public void Save(string firstName, string lastName, string adrs, string city, string region, string country, string postalcode, string phone, string email)
        {
            Ref_Customer = new Model.DomainModels.DTO.EF.Customer();
            Ref_Customer.FirstName = firstName;
            Ref_Customer.LastName = lastName;
            Ref_Customer.Adrs = adrs;
            Ref_Customer.City = city;
            Ref_Customer.Region = region;
            Ref_Customer.Country = country;
            Ref_Customer.PostalCode = postalcode;
            Ref_Customer.Phone = phone;
            Ref_Customer.Email = email;
            Ref_CustomerCrud.Insert(Ref_Customer);
        }
        #endregion

        #region [-dynamic FillGrid()-]
        public dynamic FillGrid() 
        {
            return Ref_CustomerCrud.SelectAll();
        }
        #endregion

        #region [-Delete(int id)-]
        public void Delete(int id)
        {
            Ref_Customer = new Model.DomainModels.DTO.EF.Customer();
            Ref_Customer.Id = id;

            Ref_CustomerCrud.Remove(Ref_Customer);

        }

        #endregion

        #region [-Edit(int id ,string firstName, string lastName, string adrs, string city, string region, string country, string postalcode, string phone, string email)-]
        public void Edit(int id,  string firstName, string lastName, string adrs, string city, string region, string country, string postalcode, string phone, string email)
        {
            Ref_Customer = new Model.DomainModels.DTO.EF.Customer();
            Ref_Customer.Id = id;
            Ref_Customer.FirstName = firstName;
            Ref_Customer.LastName = lastName;
            Ref_Customer.Adrs = adrs;
            Ref_Customer.City = city;
            Ref_Customer.Region = region;
            Ref_Customer.Country = country;
            Ref_Customer.PostalCode = postalcode;
            Ref_Customer.Phone = phone;
            Ref_Customer.Email = email;
            Ref_CustomerCrud.Update(Ref_Customer);
        }
        #endregion

    }
}
