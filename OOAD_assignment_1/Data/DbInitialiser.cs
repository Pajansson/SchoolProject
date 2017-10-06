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
            if (!newContext.AccountabilityTypes.Any())
            {
                //accountabilitytypes
                AccountabilityType customerType = new AccountabilityType { Description = "Customers" };
                AccountabilityType employeeType = new AccountabilityType { Description = "Employees" };
                AccountabilityType shipperType = new AccountabilityType { Description = "Shippers" };
                AccountabilityType supplierType = new AccountabilityType { Description = "Suppliers" };
                newContext.AccountabilityTypes.Add(customerType);
                newContext.AccountabilityTypes.Add(employeeType);
                newContext.AccountabilityTypes.Add(shipperType);
                newContext.AccountabilityTypes.Add(supplierType);
                newContext.SaveChanges();
            }

            if (!newContext.Parties.Any())
            {
                Party northwind = new Party() { Name = "Northwind" };
                newContext.Parties.Add(northwind);
                newContext.SaveChanges();

                foreach (var shipper in oldContext.Shippers.ToList())
                {
                    Party party = new Party() { Name = shipper.CompanyName };
                    newContext.Parties.Add(party);
                    newContext.Accountabilities.Add(new Accountability() { Accountable = party, Commissioner = northwind, AccountabilityType = shipperType });
                }

                foreach (var customer in oldContext.Customers.ToList())
                {
                    Party party = new Party() { Name = customer.CompanyName };
                    newContext.Parties.Add(party);
                    newContext.Accountabilities.Add(new Accountability() { Accountable = northwind, Commissioner = party, AccountabilityType = customerType });
                }

                foreach (var supplier in oldContext.Suppliers.ToList())
                {
                    Party party = new Party() { Name = supplier.CompanyName };
                    newContext.Parties.Add(party);
                    newContext.Accountabilities.Add(new Accountability() { Accountable = party, Commissioner = northwind, AccountabilityType = supplierType });
                }

                foreach (var employee in oldContext.Employees.ToList())
                {
                    Party party = new Party() { Name = $"{ employee.FirstName } { employee.LastName }" };
                    newContext.Parties.Add(party);
                    newContext.Accountabilities.Add(new Accountability() { Accountable = party, Commissioner = northwind, AccountabilityType = employeeType });
                }

                newContext.SaveChanges();
            }
        }
    }
}
