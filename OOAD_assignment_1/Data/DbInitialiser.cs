using OOAD_assignment_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_assignment_1.Data
{
    public static class DbInitialiser
    {
        public static void MigrateNorthwindData(NORTHWNDContext oldContext, ApplicationDbContext newContext)
        {
            Party northwind = new Party() { Name = "Northwind" };
            newContext.Parties.Add(northwind);
            newContext.SaveChanges();

            //accountabilitytypes
            var customerType = new AccountabilityType
            {
                Description = "Customers"
            };
            var employeeType = new AccountabilityType
            {
                Description = "Employees"
            };
            var shipperType = new AccountabilityType
            {
                Description = "Shippers"
            };
            var supplierType = new AccountabilityType
            {
                Description = "Suppliers"
            };
            newContext.AccountabilityTypes.Add(customerType);
            newContext.AccountabilityTypes.Add(employeeType);
            newContext.AccountabilityTypes.Add(shipperType);
            newContext.AccountabilityTypes.Add(supplierType);
            newContext.SaveChanges();


            List<Shippers> shippers = oldContext.Shippers.ToList();
            List<Customers> customers = oldContext.Customers.ToList();
            List<Suppliers> suppliers = oldContext.Suppliers.ToList();
            List<Employees> employees = oldContext.Employees.ToList();

            foreach (var shipper in shippers)
            {
                Party party = new Party() { Name = shipper.CompanyName };
                newContext.Parties.Add(party);
                newContext.Accountabilities.Add(new Accountability() { Accountable = party, Commissioner = northwind, AccountabilityType = shipperType });
            }

            foreach (var customer in customers)
            {
                Party party = new Party() { Name = customer.CompanyName };
                newContext.Parties.Add(party);
                newContext.Accountabilities.Add(new Accountability() { Accountable = northwind, Commissioner = party, AccountabilityType = customerType });
            }

            foreach (var supplier in suppliers)
            {
                Party party = new Party() { Name = supplier.CompanyName };
                newContext.Parties.Add(party);
                newContext.Accountabilities.Add(new Accountability() { Accountable = party, Commissioner = northwind, AccountabilityType = supplierType });
            }

            foreach (var employee in employees)
            {
                Party party = new Party() { Name = $"{ employee.FirstName } { employee.LastName }" };
                newContext.Parties.Add(party);
                newContext.Accountabilities.Add(new Accountability() { Accountable = party, Commissioner = northwind, AccountabilityType = employeeType });
            }

            newContext.SaveChanges();
        }
    }
}
