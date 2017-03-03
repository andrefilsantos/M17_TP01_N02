using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;

namespace M17_TP01_N02.Modal
{
    public class Database
    {
        private static Database _instance;
        private readonly SqlConnection _ligDb;

        public static Database Instance => _instance ?? (_instance = new Database());

        public Database()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["sql"].ToString();
            _ligDb = new SqlConnection(connectionString);
            _ligDb.Open();
        }

        ~Database() { try { _ligDb.Close(); } catch (Exception e) { Console.WriteLine(e.Message); } }

        #region Funções genéricas

        public DataTable SqlQuery(string sql)
        {
            var comando = new SqlCommand(sql, _ligDb);
            var registos = new DataTable();
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        public DataTable SqlQuery(string sql, List<SqlParameter> parametros)
        {
            var comando = new SqlCommand(sql, _ligDb);
            var registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        public DataTable SqlQuery(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            var comando = new SqlCommand(sql, _ligDb) { Transaction = transacao };
            var registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        public bool Command(string sql)
        {
            try
            {
                var comando = new SqlCommand(sql, _ligDb);
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Debug.WriteLine(erro.Message);
                return false;
            }
            return true;
        }

        public bool Command(string sql, List<SqlParameter> parametros)
        {
            try
            {
                var comando = new SqlCommand(sql, _ligDb);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                //throw erro;
                return false;
            }
            return true;
        }

        public bool Command(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            try
            {
                var comando = new SqlCommand(sql, _ligDb);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.Transaction = transacao;
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return false;
            }
            return true;
        }

        #endregion

        #region Utilizadores 

        public bool Login(string username, string password)
        {
            var data = SqlQuery("SELECT * FROM users WHERE username = '" + username + "' OR email='" + username + "' AND password='" + password + "' AND active = 1");
            if (data.Rows.Count <= 0 || data.Rows == null) return false;
            HttpContext.Current.Session["user"] = data.Rows[0][0].ToString();
            var cenas = HttpContext.Current.Session["user"];
            return true;
        }

        public DataTable AllUsers() => SqlQuery("SELECT username, email, name, birthday, address, gender, phone, newsletter, lastSign, role FROM users");
        public DataTable AllActiveUsers() => SqlQuery("SELECT * FROM users WHERE active=1");
        public DataTable AllInactiveUsers() => SqlQuery("SELECT * FROM users WHERE active = 0");
        public DataTable UserInfo(int id) => SqlQuery($"SELECT * FROM users WHERE idUser = {id}");
        public DataTable SearchUsers(string search)
        {
            const string sql = "SELECT * FROM users WHERE name LIKE @search OR username LIKE @search OR birthday LIKE @search OR address LIKE @search";
            var parametros = new List<SqlParameter>() { new SqlParameter() { ParameterName = "@search", SqlDbType = SqlDbType.NVarChar, Value = "%" + search + "%" } };
            return SqlQuery(sql, parametros);
        }
        public bool AddUser(string username, string password, string email, string name, string birthday, string address, string gender, string phone, string recoverPassword, string imgUrl, bool newsletter, int role)
        {
            const string sql = "INSERT INTO users(username, password, email, name, birthday, address, gender, phone, imgUrl, newsletter, role, addDate, lastUpdate, active) VALUES (@username, HASHBYTES('SHA2_512', @password), @email, @name, @birthday, @address, @gender, @phone, @imgUrl, @newsletter, @role, getDate(),  getDate(), 1)";
            var parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@username",   SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter() { ParameterName = "@password",   SqlDbType = SqlDbType.NVarChar, Value = password },
                new SqlParameter() { ParameterName = "@email",      SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter() { ParameterName = "@name",       SqlDbType = SqlDbType.NVarChar, Value = name },
                new SqlParameter() { ParameterName = "@birthday",   SqlDbType = SqlDbType.Date,     Value = birthday },
                new SqlParameter() { ParameterName = "@address",    SqlDbType = SqlDbType.NVarChar, Value = address },
                new SqlParameter() { ParameterName = "@gender",     SqlDbType = SqlDbType.Int,      Value = gender },
                new SqlParameter() { ParameterName = "@phone",      SqlDbType = SqlDbType.NVarChar, Value = phone },
                new SqlParameter() { ParameterName = "@imgUrl",     SqlDbType = SqlDbType.NVarChar, Value = imgUrl },
                new SqlParameter() { ParameterName = "@newsletter", SqlDbType = SqlDbType.Bit,      Value = newsletter },
                new SqlParameter() { ParameterName = "@role",       SqlDbType = SqlDbType.Int,      Value = role },

            };
            return Command(sql, parametros);
        }
        public bool EditUser(string username, string password, string email, string name, string birthday, string address, string gender, string phone, string recoverPassword, string imgUrl, bool newsletter, int role)
        {
            const string sql = "UPDATE users SET username=@username, password=HASHBYTES('SHA2_512', @password), email=@email, name=@name, birthday=@birthday, address=@address, gender=@gender, phone=@phone, imgUrl=@imgUrl, newsletter=@newsletter, role=@role, lastUpdate=getDate()";
            var parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@username",   SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter() { ParameterName = "@password",   SqlDbType = SqlDbType.NVarChar, Value = password },
                new SqlParameter() { ParameterName = "@email",      SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter() { ParameterName = "@name",       SqlDbType = SqlDbType.NVarChar, Value = name },
                new SqlParameter() { ParameterName = "@birthday",   SqlDbType = SqlDbType.Date,     Value = birthday },
                new SqlParameter() { ParameterName = "@address",    SqlDbType = SqlDbType.NVarChar, Value = address },
                new SqlParameter() { ParameterName = "@gender",     SqlDbType = SqlDbType.Int,      Value = gender },
                new SqlParameter() { ParameterName = "@phone",      SqlDbType = SqlDbType.NVarChar, Value = phone },
                new SqlParameter() { ParameterName = "@imgUrl",     SqlDbType = SqlDbType.NVarChar, Value = imgUrl },
                new SqlParameter() { ParameterName = "@newsletter", SqlDbType = SqlDbType.Bit,      Value = newsletter },
                new SqlParameter() { ParameterName = "@role",       SqlDbType = SqlDbType.Int,      Value = role },

            };
            return Command(sql, parametros);
        }
        public bool EnableDisableUser(int id)
        {
            var user = UserInfo(id);
            var active = int.Parse(user.Rows[0][16].ToString()) == 0 ? 1 : 0;
            return Command($"UPDATE users SET active = {active} WHERE id={active}");
        }
        #endregion

        #region Produtos

        public DataTable AllProducts() => SqlQuery("SELECT * FROM products");
        public DataTable AllProducts(string orderBy) => SqlQuery($"SELECT * FROM products ORDER BY {orderBy}");
        public DataTable AllAvailableProducts() => SqlQuery("SELECT * FROM products WHERE stock >= 0");
        public DataTable AllAvailableProducts(string orderBy) => SqlQuery($"SELECT * FROM products WHERE stock >= 0 ORDER BY {orderBy}");
        public DataTable ProductInfo(int id) => SqlQuery($"SELECT * FROM products WHERE id={id}");
        public DataTable SearchProducts(string search)
        {
            const string sql = "SELECT * FROM products WHERE productName LIKE @search OR shortDescription LIKE @search OR longDescription LIKE @search OR brand LIKE @search";
            var parametros = new List<SqlParameter>() { new SqlParameter() { ParameterName = "@search", SqlDbType = SqlDbType.NVarChar, Value = "%" + search + "%" } };
            return SqlQuery(sql, parametros);
        }
        public DataTable LatestProducts() => SqlQuery("SELECT TOP 8 * FROM products ORDER BY addDate");
        public bool AddProduct(string productName, string shortDescription, string longDescription, string brand, string category, int stock, string warnings, string imgUrl)
        {
            const string sql = "INSERT INTO products(productName, shortDescription, longDescription, brand, category, stock, warnings, imgUrl, addDate, lastUpdate, active) VALUES (@productName, @shortDescription, @longDescription, @brand, @category, @stock, @warnings, @imgUrl, getDate(),  getDate(), 1)";
            var parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@productName", SqlDbType = SqlDbType.NVarChar, Value = productName },
                new SqlParameter() { ParameterName = "@shortDescription", SqlDbType = SqlDbType.NVarChar, Value = shortDescription },
                new SqlParameter() { ParameterName = "@longDescription", SqlDbType = SqlDbType.NVarChar, Value = longDescription },
                new SqlParameter() { ParameterName = "@brand", SqlDbType = SqlDbType.NVarChar, Value = brand },
                new SqlParameter() { ParameterName = "@category", SqlDbType = SqlDbType.NVarChar, Value = category },
                new SqlParameter() { ParameterName = "@stock", SqlDbType = SqlDbType.NVarChar, Value = stock },
                new SqlParameter() { ParameterName = "@warnings", SqlDbType = SqlDbType.NVarChar, Value = warnings },
                new SqlParameter() { ParameterName = "@imgUrl", SqlDbType = SqlDbType.NVarChar, Value = imgUrl }

            };
            return Command(sql, parametros);
        }
        public bool EnableDisableProduct(int id)
        {
            var product = ProductInfo(id);
            var active = int.Parse(product.Rows[0][11].ToString()) == 0 ? 1 : 0;
            return Command($"UPDATE products SET active = {active} WHERE id={active}");
        }

        #endregion

        #region Comentários

        public DataTable AllProductComments(int id) => SqlQuery($"SELECT * FROM comments WHERE idProduct = {id} AND rating <> -1 AND active = 1");
        public DataTable AllCommentComments(int id) => SqlQuery($"SELECT * FROM comments WHERE idComments = {id}  AND rating <> -1 AND  active = 1");
        public int AvgOfRatings(int id) => int.Parse(SqlQuery($"SELECT AVG(rating) FROM comments WHERE idProduct = {id} AND rating <> -1").Rows[0][0].ToString());

        //public bool AddComment(int id)
        //{

        //}

        #endregion

        #region Marcas 

        public DataTable AllBrands() => SqlQuery("SELECT idBrand, name FROM brands");

        #endregion

        #region Categorias

        public DataTable AllCategories() => SqlQuery("SELECT idCategory, name FROM categories");

        #endregion
    }
}