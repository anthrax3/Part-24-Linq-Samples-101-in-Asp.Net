﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LINQ_query_examples
{
	public LINQ_query_examples()
	{
    //LINQ query examples
	}

    //Simple Where clause

    //Join and simple Where clause
    
    //Use the Distinct Operator
    
    //Simple inner join
    
    //Self-join
    
    //Double and multiple joins
    
    //Join using entity fields
    
    //Late-binding left join
    
    //Use the Equals operator
    
    //Use the Not Equals operator
    
    //Use a method-based LINQ query with a Where clause
    
    //Use the Greater Than operator
    
    //Use the Greater Than or Equals and Less Than or Equals operators
    
    //Use the Contains operator
    
    //Use the Does Not Contain operator
    
    //Use the StartsWith and EndsWith operators
    
    //Use the And and Or operators
    
    //Use the OrderBy operator
    
    //Use the First and Single operators
    
    //Retrieving formatted values
    
    //Use the Skip and Take operators without paging
    
    //Use the FirstOrDefault and SingleOrDefault operators
    
    //Use a self-join with a condition on the linked entity
    
    //Use a transformation in the Where Clause
    
    //Use a paging sort
    
    //Retrieve related entity columns for 1 to N relationships
    
    //Use .value to retrieve the value of an attribute
    
    //Multiple projections, new data type casting to different types
    
    //Use the GetAttributeValue method
    
    //Use Math methods
    
    //Use Multiple Select and Where clauses
    
    //Use SelectMany
    
    //Use string operations
    
    //Use two Where clauses
    
    //Use LoadProperty to retrieve related records



    //Simple Where clause

    //The following sample shows how to retrieve a list of accounts where the Name contains “Contoso”.

    using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_where1 = from a in svcContext.AccountSet
                    where a.Name.Contains("Contoso")
                    select a;
 foreach (var a in query_where1)
 {
  System.Console.WriteLine(a.Name + " " + a.Address1_City);
 }
}

//The following sample shows how to retrieve a list of accounts where the Name contains “Contoso” and Address1_City is “Redmond”.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_where2 = from a in svcContext.AccountSet
                    where a.Name.Contains("Contoso")
                    where a.Address1_City == "Redmond"
                    select a;

 foreach (var a in query_where2)
 {
  System.Console.WriteLine(a.Name + " " + a.Address1_City);
 }
}

//Join and simple Where clause

//The following sample shows how to retrieve the account Name and the contact LastName where the account Name contains “Contoso” and the contact LastName contains “Smith” and the contact is the Primary Contact for the account.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_where3 = from c in svcContext.ContactSet
                    join a in svcContext.AccountSet
                    on c.ContactId equals a.PrimaryContactId.Id
                    where a.Name.Contains("Contoso")
                    where c.LastName.Contains("Smith")
                    select new
                    {
                     account_name = a.Name,
                     contact_name = c.LastName
                    };

 foreach (var c in query_where3)
 {
  System.Console.WriteLine("acct: " +
   c.account_name +
   "\t\t\t" +
   "contact: " +
   c.contact_name);
 }
}

//Use the Distinct Operator

//The following sample shows how to retrieve a distinct list of contact last names. Although there may be duplicates, each name will be listed only once.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_distinct = (from c in svcContext.ContactSet
                       select c.LastName).Distinct();
 foreach (var c in query_distinct)
 {
  System.Console.WriteLine(c);
 }
}

//Simple inner join

//The following sample shows how to retrieve information about an account and the contact listed as the primary contact for the account.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_join1 = from c in svcContext.ContactSet
                   join a in svcContext.AccountSet
                  on c.ContactId equals a.PrimaryContactId.Id
                   select new
                   {
                    c.FullName,
                    c.Address1_City,
                    a.Name,
                    a.Address1_Name
                   };
 foreach (var c in query_join1)
 {
  System.Console.WriteLine("acct: " +
   c.Name +
   "\t\t\t" +
   "contact: " +
   c.FullName);
 }
}

//Self-join

//The following sample shows how to retrieve information about accounts where an account is the parent account for an account.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_join5 = from a in svcContext.AccountSet
                   join a2 in svcContext.AccountSet
                   on a.ParentAccountId.Id equals a2.AccountId

                   select new
                   {
                    account_name = a.Name,
                    account_city = a.Address1_City
                   };
 foreach (var c in query_join5)
 {
  System.Console.WriteLine(c.account_name + "  " + c.account_city);
 }
}

//Double and multiple joins

//The following sample shows how to retrieve information from account, contact and lead where the contact is the primary contact for the account and the lead was the originating lead for the account.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_join4 = from a in svcContext.AccountSet
                   join c in svcContext.ContactSet
                   on a.PrimaryContactId.Id equals c.ContactId
                   join l in svcContext.LeadSet
                   on a.OriginatingLeadId.Id equals l.LeadId
                   select new
                   {
                    contact_name = c.FullName,
                    account_name = a.Name,
                    lead_name = l.FullName
                   };
 foreach (var c in query_join4)
 {
  System.Console.WriteLine(c.contact_name +
   "  " +
   c.account_name +
   "  " +
   c.lead_name);
 }
}

//The following sample shows how to retrieve account and contact information where an account is the parent account for an account and the contact is the primary contact for the account.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_join6 = from c in svcContext.ContactSet
                   join a in svcContext.AccountSet
                   on c.ContactId equals a.PrimaryContactId.Id
                   join a2 in svcContext.AccountSet
                   on a.ParentAccountId.Id equals a2.AccountId
                   select new
                   {
                    contact_name = c.FullName,
                    account_name = a.Name
                   };
 foreach (var c in query_join6)
 {
  System.Console.WriteLine(c.contact_name + "  " + c.account_name);
 }
}

//Join using entity fields

//The following sample shows how to retrieve information about accounts from a list 

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var list_join = (from a in svcContext.AccountSet
                  join c in svcContext.ContactSet
                  on a.PrimaryContactId.Id equals c.ContactId
                  where a.Name == "Contoso Ltd" &amp;&amp;
                  a.Address1_Name == "Contoso Pharmaceuticals"
                  select a).ToList();
 foreach (var c in list_join)
 {
  System.Console.WriteLine("Account " + list_join[0].Name
      + " and it's primary contact "
      + list_join[0].PrimaryContactId.Id);
 }
}

//Late-binding left join

//The following sample shows a left join. A left join is designed to return parents with and without children from two sources. There is a correlation between parent and child, but no child may actually exist.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_join8 = from a in svcContext.AccountSet
                   join c in svcContext.ContactSet
                   on a.PrimaryContactId.Id equals c.ContactId
                   into gr
                   from c_joined in gr.DefaultIfEmpty()
                   select new
                   {
                    contact_name = c_joined.FullName,
                    account_name = a.Name
                   };
 foreach (var c in query_join8)
 {
  System.Console.WriteLine(c.contact_name + "  " + c.account_name);
 }
}

//Use the Equals operator

//The following sample shows how to retrieve a list of contacts where the FirstName is “Colin”.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_equals1 = from c in svcContext.ContactSet
                     where c.FirstName.Equals("Colin")
                     select new
                     {
                      c.FirstName,
                      c.LastName,
                      c.Address1_City
                     };
 foreach (var c in query_equals1)
 {
  System.Console.WriteLine(c.FirstName +
   " " + c.LastName +
   " " + c.Address1_City);
 }
}

//The following sample shows how to retrieve a list of contacts where the FamilyStatusCode is 3. This corresponds to the Marital Status option of Divorced.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_equals2 = from c in svcContext.ContactSet
                     where c.FamilyStatusCode.Equals(3)
                     select new
                     {
                      c.FirstName,
                      c.LastName,
                      c.Address1_City
                     };
 foreach (var c in query_equals2)
 {
  System.Console.WriteLine(c.FirstName +
   " " + c.LastName +
   " " + c.Address1_City);
 }
}

//Use the Not Equals operator

//The following sample shows how to retrieve a list of contacts where the Address1_City is not “Redmond”.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_ne1 = from c in svcContext.ContactSet
                 where c.Address1_City != "Redmond"
                 select new
                 {
                  c.FirstName,
                  c.LastName,
                  c.Address1_City
                 };
 foreach (var c in query_ne1)
 {
  System.Console.WriteLine(c.FirstName + " " +
   c.LastName + " " + c.Address1_City);
 }
}

//The following sample shows how to retrieve a list of contacts where the FirstName is not “Colin”.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_ne2 = from c in svcContext.ContactSet
                 where !c.FirstName.Equals("Colin")
                 select new
                 {
                  c.FirstName,
                  c.LastName,
                  c.Address1_City
                 };

 foreach (var c in query_ne2)
 {
  System.Console.WriteLine(c.FirstName + " " +
   c.LastName + " " + c.Address1_City);
 }
}

//Use a method-based LINQ query with a Where clause

//The following sample shows how to retrieve a list of contacts where the LastName is “Smith” or contains “Smi”.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var methodResults = svcContext.ContactSet
  .Where(a => a.LastName == "Smith");
 var methodResults2 = svcContext.ContactSet
  .Where(a => a.LastName.StartsWith("Smi"));
 Console.WriteLine();
 Console.WriteLine("Method query using Lambda expression");
 Console.WriteLine("---------------------------------------");
 foreach (var a in methodResults)
 {
  Console.WriteLine("Name: " + a.FirstName + " " + a.LastName);
 }
 Console.WriteLine("---------------------------------------");
 Console.WriteLine("Method query 2 using Lambda expression");
 Console.WriteLine("---------------------------------------");
 foreach (var a in methodResults2)
 {
  Console.WriteLine("Name: " + a.Attributes["firstname"] +
   " " + a.Attributes["lastname"]);
 }
}

//Use the Greater Than operator

//The following sample shows how to retrieve a list of contacts with an Anniversary date later than February 5, 2010.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_gt1 = from c in svcContext.ContactSet
                 where c.Anniversary > new DateTime(2010, 2, 5)
                 select new
                 {
                  c.FirstName,
                  c.LastName,
                  c.Address1_City
                 };

 foreach (var c in query_gt1)
 {
  System.Console.WriteLine(c.FirstName + " " +
   c.LastName + " " + c.Address1_City);
 }
}

//The following sample shows how to retrieve contacts with a CreditLimit greater than $20,000. 


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_gt2 = from c in svcContext.ContactSet
                 where c.CreditLimit.Value > 20000
                 select new
                 {
                  c.FirstName,
                  c.LastName,
                  c.Address1_City
                 };
 foreach (var c in query_gt2)
 {
  System.Console.WriteLine(c.FirstName + " " +
   c.LastName + " " + c.Address1_City);
 }
}

//Use the Greater Than or Equals and Less Than or Equals operators

//The following sample shows how to retrieve contacts with a CreditLimit greater than $200 and less than $400. 


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_gele1 = from c in svcContext.ContactSet
                   where c.CreditLimit.Value >= 200 &amp;&amp;
                   c.CreditLimit.Value <= 400
                   select new
                   {
                    c.FirstName,
                    c.LastName
                   };
 foreach (var c in query_gele1)
 {
  System.Console.WriteLine(c.FirstName + " " + c.LastName);
 }
}

//Use the Contains operator

//The following sample shows how to retrieve contacts where the Description contains “Alpine”.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_contains1 = from c in svcContext.ContactSet
                       where c.Description.Contains("Alpine")
                       select new
                       {
                        c.FirstName,
                        c.LastName
                       };
 foreach (var c in query_contains1)
 {
  System.Console.WriteLine(c.FirstName + " " + c.LastName);
 }
}

//Use the Does Not Contain operator

//The following sample shows how to retrieve contacts where the Description does not contain “Coho”.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_contains2 = from c in svcContext.ContactSet
                       where !c.Description.Contains("Coho")
                       select new
                       {
                        c.FirstName,
                        c.LastName
                       };
 foreach (var c in query_contains2)
 {
  System.Console.WriteLine(c.FirstName + " " + c.LastName);
 }
}

//Use the StartsWith and EndsWith operators

//The following sample shows how to retrieve contacts where FirstName starts with “Bri”.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_startswith1 = from c in svcContext.ContactSet
                         where c.FirstName.StartsWith("Bri")
                         select new
                         {
                          c.FirstName,
                          c.LastName
                         };
 foreach (var c in query_startswith1)
 {
  System.Console.WriteLine(c.FirstName + " " + c.LastName);
 }
}

//The following sample shows how to retrieve contacts where LastName ends with “cox”.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_endswith1 = from c in svcContext.ContactSet
                       where c.LastName.EndsWith("cox")
                       select new
                       {
                        c.FirstName,
                        c.LastName
                       };
 foreach (var c in query_endswith1)
 {
  System.Console.WriteLine(c.FirstName + " " + c.LastName);
 }
}
//Use the And and Or operators

//The following sample shows how to retrieve contacts where Address1_City is “Redmond” or “Bellevue” and a CreditLimit that is greater than $200.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_andor1 = from c in svcContext.ContactSet
                    where ((c.Address1_City == "Redmond" ||
                    c.Address1_City == "Bellevue") &amp;&amp;
                    (c.CreditLimit.Value != null &amp;&amp;
                    c.CreditLimit.Value >= 200))
                    select c;

 foreach (var c in query_andor1)
 {
  System.Console.WriteLine(c.LastName + ", " + c.FirstName + " " +
   c.Address1_City + " " + c.CreditLimit.Value);
 }
}

//Use the OrderBy operator

//The following sample shows how to retrieve contacts ordered by CreditLimit in descending order.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_orderby1 = from c in svcContext.ContactSet
                      where !c.CreditLimit.Equals(null)
                      orderby c.CreditLimit descending
                      select new
                      {
                       limit = c.CreditLimit,
                       first = c.FirstName,
                       last = c.LastName
                      };
 foreach (var c in query_orderby1)
 {
  System.Console.WriteLine(c.limit.Value + " " +
   c.last + ", " + c.first);
 }
}

//The following sample shows how to retrieve contacts ordered by LastName in descending order and FirstName in ascending order.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_orderby2 = from c in svcContext.ContactSet
                      orderby c.LastName descending,
                      c.FirstName ascending
                      select new
                      {
                       first = c.FirstName,
                       last = c.LastName
                      };

 foreach (var c in query_orderby2)
 {
  System.Console.WriteLine(c.last + ", " + c.first);
 }
}

//Use the First and Single operators

//The following sample shows how to retrieve only the first contact record returned and retrieve only one contact record that matches the criterion.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 Contact firstcontact = svcContext.ContactSet.First();

 Contact singlecontact = svcContext.ContactSet.Single(c => c.ContactId == _contactId1);
 System.Console.WriteLine(firstcontact.LastName + ", " +
  firstcontact.FirstName + " is the first contact");
 System.Console.WriteLine("==========================");
 System.Console.WriteLine(singlecontact.LastName + ", " +
  singlecontact.FirstName + " is the single contact");
}


//Retrieving formatted values

//The following sample shows how to retrieve the label for an optionset option, in this case the value for the current record status.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var list_retrieve1 = from c in svcContext.ContactSet
                      where c.ContactId == _contactId1
                      select new { StatusReason = c.FormattedValues["statuscode"] };
 foreach (var c in list_retrieve1)
 {
  System.Console.WriteLine("Status: " + c.StatusReason);
 }
}

//Use the Skip and Take operators without paging

//The following sample shows how to retrieve just two records after skipping two records where the LastName is not “Parker” using the Skip and Takeoperators.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{

 var query_skip = (from c in svcContext.ContactSet
                   where c.LastName != "Parker"
                   orderby c.FirstName
                   select new
                       {
                        last = c.LastName,
                        first = c.FirstName
                       }).Skip(2).Take(2);
 foreach (var c in query_skip)
 {
  System.Console.WriteLine(c.first + " " + c.last);
 }
}


//Use the FirstOrDefault and SingleOrDefault operators

//The FirstOrDefault operator returns the first element of a sequence, or a default value if no element is found. The SingleOrDefault operator returns a single, specific element of a sequence, or a default value if that element is not found. The following sample shows how to use these operators.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{

 Contact firstorcontact = svcContext.ContactSet.FirstOrDefault();

 Contact singleorcontact = svcContext.ContactSet
  .SingleOrDefault(c => c.ContactId == _contactId1);


 System.Console.WriteLine(firstorcontact.FullName +
  " is the first contact");
 System.Console.WriteLine("==========================");
 System.Console.WriteLine(singleorcontact.FullName +
  " is the single contact");
}

//Use a self-join with a condition on the linked entity

//The following sample shows how to retrieve the names of two accounts where one account is the parent account of the other.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_joincond = from a1 in svcContext.AccountSet
                      join a2 in svcContext.AccountSet
                      on a1.ParentAccountId.Id equals a2.AccountId
                      where a2.AccountId == _accountId1
                      select new { Account = a1, Parent = a2 };
 foreach (var a in query_joincond)
 {
  System.Console.WriteLine(a.Account.Name + " " + a.Parent.Name);
 }
}
//Use a transformation in the Where Clause

//The following sample shows how to retrieve a specific contact where the anniversary date is later than January 1, 2010.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_wheretrans = from c in svcContext.ContactSet
                        where c.ContactId == _contactId1 &amp;&amp;
                        c.Anniversary > DateTime.Parse("1/1/2010")
                        select new
                        {
                         c.FirstName,
                         c.LastName
                        };
 foreach (var c in query_wheretrans)
 {
  System.Console.WriteLine(c.FirstName + " " + c.LastName);
 }
}

//Use a paging sort

//The following sample shows a multi-column sort with an extra condition.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_pagingsort1 = (from c in svcContext.ContactSet
                          where c.LastName != "Parker"
                          orderby c.LastName ascending,
                          c.FirstName descending
                          select new { c.FirstName, c.LastName })
                          .Skip(2).Take(2);
 foreach (var c in query_pagingsort1)
 {
  System.Console.WriteLine(c.FirstName + " " + c.LastName);
 }
}

//The following sample shows a paging sort where the column being sorted is different from the column being retrieved.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_pagingsort2 = (from c in svcContext.ContactSet
                          where c.LastName != "Parker"
                          orderby c.FirstName descending
                          select new { c.FirstName }).Skip(2).Take(2);
 foreach (var c in query_pagingsort2)
 {
  System.Console.WriteLine(c.FirstName);
 }
}

//The following sample shows how to retrieve just the first 10 records.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_pagingsort3 = (from c in svcContext.ContactSet
                          where c.LastName.StartsWith("W")
                          orderby c.MiddleName ascending,
                          c.FirstName descending
                          select new
                          {
                           c.FirstName,
                           c.MiddleName,
                           c.LastName
                          }).Take(10);
 foreach (var c in query_pagingsort3)
 {
  System.Console.WriteLine(c.FirstName + " " +
   c.MiddleName + " " + c.LastName);
 }
}

//Retrieve related entity columns for 1 to N relationships

//The following sample shows how to retrieve columns from related account and contact records.


using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_retrieve1 = from c in svcContext.ContactSet
                       join a in svcContext.AccountSet
                       on c.ContactId equals a.PrimaryContactId.Id
                       where c.ContactId != _contactId1
                       select new { Contact = c, Account = a };
 foreach (var c in query_retrieve1)
 {
  System.Console.WriteLine("Acct: " + c.Account.Name +
   "\t\t" + "Contact: " + c.Contact.FullName);
 }

}
//Use .value to retrieve the value of an attribute

//The following sample shows usage of Value to access the value of an attribute.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{

 var query_value = from c in svcContext.ContactSet
                   where c.ContactId != _contactId2
                   select new
                   {
                    ContactId = c.ContactId != null ?
                     c.ContactId.Value : Guid.Empty,
                    NumberOfChildren = c.NumberOfChildren != null ?
                     c.NumberOfChildren.Value : default(int),
                    CreditOnHold = c.CreditOnHold != null ?
                     c.CreditOnHold.Value : default(bool),
                    Anniversary = c.Anniversary != null ?
                     c.Anniversary.Value : default(DateTime)
                   };

 foreach (var c in query_value)
 {
  System.Console.WriteLine(c.ContactId + " " + c.NumberOfChildren + 
   " " + c.CreditOnHold + " " + c.Anniversary);
 }
}

//Multiple projections, new data type casting to different types

//The following sample shows multiple projections and how to cast values to a different type.

using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_projections = from c in svcContext.ContactSet
                         where c.ContactId == _contactId1
                         &amp;&amp; c.NumberOfChildren != null &amp;&amp; 
                         c.Anniversary.Value != null
                         select new
                         {
                          Contact = new Contact { 
                           LastName = c.LastName, 
                           NumberOfChildren = c.NumberOfChildren 
                          },
                          NumberOfChildren = (double)c.NumberOfChildren,
                          Anniversary = c.Anniversary.Value.AddYears(1),
                         };
 foreach (var c in query_projections)
 {
  System.Console.WriteLine(c.Contact.LastName + " " + 
   c.NumberOfChildren + " " + c.Anniversary);
 }
}

//Use the GetAttributeValue method

//The following sample shows how to use the GetAttributeValue<T> method.



using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_getattrib = from c in svcContext.ContactSet
                       where c.GetAttributeValue<Guid>("contactid") != _contactId1
                       select new
                       {
                        ContactId = c.GetAttributeValue<Guid?>("contactid"),
                        NumberOfChildren = c.GetAttributeValue<int?>("numberofchildren"),
                        CreditOnHold = c.GetAttributeValue<bool?>("creditonhold"),
                        Anniversary = c.GetAttributeValue<DateTime?>("anniversary"),
                       };

 foreach (var c in query_getattrib)
 {
  System.Console.WriteLine(c.ContactId + " " + c.NumberOfChildren + 
   " " + c.CreditOnHold + " " + c.Anniversary);
 }
}

//Use Math methods

//The following sample shows how to use various Math methods.



using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_math = from c in svcContext.ContactSet
                  where c.ContactId != _contactId2
                  &amp;&amp; c.Address1_Latitude != null &amp;&amp; 
                  c.Address1_Longitude != null
                  select new
                  {
                   Round = Math.Round(c.Address1_Latitude.Value),
                   Floor = Math.Floor(c.Address1_Latitude.Value),
                   Ceiling = Math.Ceiling(c.Address1_Latitude.Value),
                   Abs = Math.Abs(c.Address1_Latitude.Value),
                  };
 foreach (var c in query_math)
 {
  System.Console.WriteLine(c.Round + " " + c.Floor + 
   " " + c.Ceiling + " " + c.Abs);
 }
}

//Use Multiple Select and Where clauses

//The following sample shows multiple select and where clauses using a method-based query syntax.



using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_multiselect = svcContext.IncidentSet
                        .Where(i => i.IncidentId != _incidentId1)
                        .Select(i => i.incident_customer_accounts)
                        .Where(a => a.AccountId != _accountId2)
                        .Select(a => a.account_primary_contact)
                        .OrderBy(c => c.FirstName)
                        .Select(c => c.ContactId);
 foreach (var c in query_multiselect)
 {
  System.Console.WriteLine(c.GetValueOrDefault());
 }
}

//Use SelectMany

//The following sample shows how to use the SelectMany Method.



using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_selectmany = svcContext.ContactSet
                        .Where(c => c.ContactId != _contactId2)
                        .SelectMany(c => c.account_primary_contact)
                        .OrderBy(a => a.Name);
 foreach (var c in query_selectmany)
 {
  System.Console.WriteLine(c.AccountId + " " + c.Name);
 }
}

//Use string operations

//The following sample shows how to use various String methods.



using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_string = from c in svcContext.ContactSet
                    where c.ContactId == _contactId2
                    select new
                    {
                     IndexOf = c.FirstName.IndexOf("contact"),
                     Insert = c.FirstName.Insert(1, "Insert"),
                     Remove = c.FirstName.Remove(1, 1),
                     Substring = c.FirstName.Substring(1, 1),
                     ToUpper = c.FirstName.ToUpper(),
                     ToLower = c.FirstName.ToLower(),
                     TrimStart = c.FirstName.TrimStart(),
                     TrimEnd = c.FirstName.TrimEnd(),
                    };

 foreach (var c in query_string)
 {
  System.Console.WriteLine(c.IndexOf + "\n" + c.Insert + "\n" + 
   c.Remove + "\n" + c.Substring + "\n"
                           + c.ToUpper + "\n" + c.ToLower + 
                           "\n" + c.TrimStart + " " + c.TrimEnd);
 }
}

//Use two Where clauses

//The following sample shows how to use two Where clauses.



using (ServiceContext svcContext = new ServiceContext(_serviceProxy))
{
 var query_twowhere = from a in svcContext.AccountSet
                      join c in svcContext.ContactSet 
                      on a.PrimaryContactId.Id equals c.ContactId
                      where c.LastName == "Smith" &amp;&amp; c.CreditOnHold != null
                      where a.Name == "Contoso Ltd"
                      orderby a.Name
                      select a;
 foreach (var c in query_twowhere)
 {
  System.Console.WriteLine(c.AccountId + " " + c.Name);
 }
}

//Use LoadProperty to retrieve related records

//The following sample shows how to LoadProperty to access related records.



Contact benAndrews = svcContext.ContactSet.Where(c => c.FullName == "Ben Andrews").FirstOrDefault();
if (benAndrews != null)
{
 //benAndrews.Contact_Tasks is null until LoadProperty is used.
 svcContext.LoadProperty(benAndrews, "Contact_Tasks");
 Task benAndrewsFirstTask = benAndrews.Contact_Tasks.FirstOrDefault();
 if (benAndrewsFirstTask != null)
 {
  Console.WriteLine("Ben Andrews first task with Subject: '{0}' retrieved.", benAndrewsFirstTask.Subject);
 }
}


}