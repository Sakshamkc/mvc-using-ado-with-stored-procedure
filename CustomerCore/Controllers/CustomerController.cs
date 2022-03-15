using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerCore.Data;
using CustomerCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CustomerCore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Customer
        public IActionResult Index()
        {
            DataTable da = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sdd = new SqlDataAdapter("ViewAll", sqlConnection);
                sdd.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdd.Fill(da);
            }
            return View(da);
        }
        // GET: Customer/Edit/5
        public IActionResult AddOrEdit(int? id)
        {
            Customers customers = new Customers();
            if(id>0)
            {
                customers = FetchById(id);
            }
            return View(customers);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(Customers customers)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("AddOrEdit", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CustomerID",customers.CustomerID);
                    cmd.Parameters.AddWithValue("Firstname", customers.Firstname);
                    cmd.Parameters.AddWithValue("Middlename", customers.Middlename);
                    cmd.Parameters.AddWithValue("Lastname", customers.Lastname);
                    cmd.Parameters.AddWithValue("Address", customers.Address);
                    cmd.ExecuteNonQuery();

                }
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customer/Delete/5
        public IActionResult Delete(int? id)
        {
            Customers customers = FetchById(id);
            return View(customers);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("DeleteByID", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }


        [NonAction]

        public Customers FetchById(int? id)
        {
            Customers customers = new Customers();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                DataTable da = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sdd = new SqlDataAdapter("ViewByID", sqlConnection);
                sdd.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdd.SelectCommand.Parameters.AddWithValue("CustomerID", id);
                sdd.Fill(da);

                if(da.Rows.Count == 1)
                {
                    customers.CustomerID = Convert.ToInt32(da.Rows[0]["CustomerID"].ToString());
                    customers.Firstname = da.Rows[0]["Firstname"].ToString();
                    customers.Middlename = da.Rows[0]["Middlename"].ToString();
                    customers.Lastname = da.Rows[0]["Lastname"].ToString();
                    customers.Address = da.Rows[0]["Address"].ToString();
                }
                return customers;
            }
            

        }

    }
}
