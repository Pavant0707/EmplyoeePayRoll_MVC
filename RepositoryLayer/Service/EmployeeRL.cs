using ModelLayer.Employeemodel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RepositoryLayer.Service
{
    public class EmployeeRL : IEmployeeRL
    {
        string conneectingString = @"Data Source = Praveen\SQLEXPRESS;Initial Catalog = EMPLOYEEMVC; Integrated Security = True";

        public bool RegisterEmployee(RegisterModel registerModel)
        {
            using (SqlConnection con = new SqlConnection(conneectingString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("REGISTEREMPLOYEE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPLOYEENAME", registerModel.EMPLOYEENAME);
                    cmd.Parameters.AddWithValue("@PROFILEIMAGE", registerModel.PROFILEIMAGE);
                    cmd.Parameters.AddWithValue("@GENDER", registerModel.GENDER);
                    cmd.Parameters.AddWithValue("@DEPARTMENT", registerModel.DEPARTMENT);
                    cmd.Parameters.AddWithValue("@SALARY", registerModel.SALARY);
                    cmd.Parameters.AddWithValue("@STARTDATE", registerModel.StartDate);
                    cmd.Parameters.AddWithValue("@NOTES", registerModel.Notes);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    con.Close();

                }
                return false;
            }
        }
        public IEnumerable<RegisterModel> GetEmployees()
        {

            List<RegisterModel> employees = new List<RegisterModel>();
            SqlConnection conn = new SqlConnection(conneectingString);
            SqlCommand cmd = new SqlCommand("Employee_List_SP", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                RegisterModel model = new RegisterModel();
                model.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                model.EMPLOYEENAME = sdr["EMPLOYEENAME"].ToString();
                model.PROFILEIMAGE = sdr["PROFILEIMAGE"].ToString();
                model.GENDER = sdr["GENDER"].ToString();
                model.DEPARTMENT = sdr["DEPARTMENT"].ToString();
                model.SALARY = Convert.ToInt64(sdr["Salary"]);
                model.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                model.Notes = sdr["Notes"].ToString();
                employees.Add(model);
            }
            conn.Close();
            return employees;

        }

        public bool Update_employee(RegisterModel registerModel)
        {
            using (SqlConnection con = new SqlConnection(conneectingString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATEEMPLOYEE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", registerModel.EMPLOYEEID);
                    cmd.Parameters.AddWithValue("@EmpName", registerModel.EMPLOYEENAME);
                    cmd.Parameters.AddWithValue("@ProfileImage", registerModel.PROFILEIMAGE);
                    cmd.Parameters.AddWithValue("@Gender", registerModel.GENDER);
                    cmd.Parameters.AddWithValue("@Department", registerModel.DEPARTMENT);
                    cmd.Parameters.AddWithValue("@Salary", registerModel.SALARY);
                    cmd.Parameters.AddWithValue("@StartDate", registerModel.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", registerModel.Notes);
                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    con.Close();

                }
                return false;
            }
        }

        public RegisterModel GetById(int id)
        {
            SqlConnection con = new SqlConnection(conneectingString);

            try
            {
                RegisterModel model = new RegisterModel();
                SqlCommand cmd = new SqlCommand("GET_BY_id", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPLOYEEID", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    model.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                    model.EMPLOYEENAME = sdr["EMPLOYEENAME"].ToString();
                    model.PROFILEIMAGE = sdr["PROFILEIMAGE"].ToString();
                    model.GENDER = sdr["GENDER"].ToString();
                    model.DEPARTMENT = sdr["DEPARTMENT"].ToString();
                    model.SALARY = Convert.ToInt64(sdr["Salary"]);
                    model.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                    model.Notes = sdr["Notes"].ToString();
                }
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null;
        }

        public bool DeleteEmployee(int id)
        {
            SqlConnection con = new SqlConnection(conneectingString);
            try
            {
                SqlCommand cmd = new SqlCommand("DELETEEMPLOYE", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public RegisterModel GetByName(string name)
        {
            SqlConnection con = new SqlConnection(conneectingString);
            try
            {
                RegisterModel model = new RegisterModel();
                SqlCommand cmd = new SqlCommand("GET_EMPLOYEE_BY_NAME", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPLOYEENAME", name);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    model.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                    model.EMPLOYEENAME = sdr["EMPLOYEENAME"].ToString();
                    model.PROFILEIMAGE = sdr["PROFILEIMAGE"].ToString();
                    model.GENDER = sdr["GENDER"].ToString();
                    model.DEPARTMENT = sdr["DEPARTMENT"].ToString();
                    model.SALARY = Convert.ToInt64(sdr["Salary"]);
                    model.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                    model.Notes = sdr["Notes"].ToString();
                }
                return model;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null;
        }

        public RegisterModel LogIn(LoginModel loginModel)
        {


            using (SqlConnection conn = new SqlConnection(conneectingString))

            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("LOGIN_ps", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", loginModel.EMPLOYEEID);
                    cmd.Parameters.AddWithValue("@EmpName", loginModel.EMPLOYEENAME);


                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        RegisterModel empModel = new RegisterModel();
                        empModel.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                        empModel.EMPLOYEENAME = sdr["EMPLOYEEID"].ToString();

                        return empModel;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }

        }

        public RegisterModel GetName(string name)
        {
            using (SqlConnection conn = new SqlConnection(conneectingString))
            {
                try
                {
                    RegisterModel model = new RegisterModel();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GET_NAME", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPLOYEENAME", name);
                    SqlDataReader sdr = cmd.ExecuteReader();
                   
                    {

                        while (sdr.Read())
                        {
                            model.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                            model.EMPLOYEENAME = sdr["EMPLOYEENAME"].ToString();
                            model.PROFILEIMAGE = sdr["PROFILEIMAGE"].ToString();
                            model.GENDER = sdr["GENDER"].ToString();
                            model.DEPARTMENT = sdr["DEPARTMENT"].ToString();
                            model.SALARY = Convert.ToInt64(sdr["Salary"]);
                            model.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                            model.Notes = sdr["Notes"].ToString();
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        public IEnumerable<RegisterModel> GetNameList(string name)
        {
            using (SqlConnection conn = new SqlConnection(conneectingString))
            {
                try
                {
                    List<RegisterModel> models = new List<RegisterModel>();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GET_NAME", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPLOYEENAME", name);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        RegisterModel model = new RegisterModel();
                        model.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                        model.EMPLOYEENAME = sdr["EMPLOYEENAME"].ToString();
                        model.PROFILEIMAGE = sdr["PROFILEIMAGE"].ToString();
                        model.GENDER = sdr["GENDER"].ToString();
                        model.DEPARTMENT = sdr["DEPARTMENT"].ToString();
                        model.SALARY = Convert.ToInt64(sdr["Salary"]);
                        model.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                        model.Notes = sdr["Notes"].ToString();
                        models.Add(model);
                    }
                    return models;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }
       // take input of employeename and sow the details, if more than one employee of same name exist show list of their data
        public List<RegisterModel> GetSameNameList(string EmpName)
        {
            using (SqlConnection conn = new SqlConnection(conneectingString))
            {
                List<RegisterModel> models = new List<RegisterModel>();

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GET_SAME_NAME", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPLOYEENAME", EmpName);
                    SqlDataReader sdr = cmd.ExecuteReader();
                   
                  
                    while (sdr.Read())
                    {
                        RegisterModel model = new RegisterModel();
                        model.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                        model.EMPLOYEENAME = sdr["EMPLOYEENAME"].ToString();
                        model.PROFILEIMAGE = sdr["PROFILEIMAGE"].ToString();
                        model.GENDER = sdr["GENDER"].ToString();
                        model.DEPARTMENT = sdr["DEPARTMENT"].ToString();
                        model.SALARY = Convert.ToInt64(sdr["Salary"]);
                        model.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                        model.Notes = sdr["Notes"].ToString();
                        models.Add(model);
                        
                    }
                    return models;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;

            }
        }
        public RegisterModel GetSameName(string EmpName)
        {

          

            SqlConnection conn = new SqlConnection(conneectingString);
            try
            {
                RegisterModel model = new RegisterModel();
                SqlCommand cmd = new SqlCommand("GET_EMPLOYEE_BY_NAME", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPLOYEENAME", EmpName);
                conn.Open();

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                   
                   
                    model.EMPLOYEEID = Convert.ToInt32(sdr["EMPLOYEEID"]);
                    model.EMPLOYEENAME = sdr["EMPLOYEENAME"].ToString();
                    model.PROFILEIMAGE = sdr["PROFILEIMAGE"].ToString();
                    model.GENDER = sdr["GENDER"].ToString();
                    model.DEPARTMENT = sdr["DEPARTMENT"].ToString();
                    model.SALARY = Convert.ToInt64(sdr["Salary"]);
                    model.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                    model.Notes = sdr["Notes"].ToString();
                   
                }
                return model;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;

        }

        

        
    }
}