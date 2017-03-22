using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace M17_TP01_N02.Modal {
    public class Database {
        private static Database _instance;
        private readonly SqlConnection _ligDb;
        private string _sql;
        private List<SqlParameter> _parameters;

        public static Database Instance => _instance ?? (_instance = new Database());

        public Database() {
            var connectionString = ConfigurationManager.ConnectionStrings["sql"].ToString();
            _ligDb = new SqlConnection(connectionString);
            _ligDb.Open();
        }

        ~Database() { try { _ligDb.Close(); } catch (Exception e) { Console.WriteLine(e.Message); } }

        #region Funções genéricas

        public DataTable SqlQuery(string sql) {
            var comando = new SqlCommand(sql, _ligDb);
            var registos = new DataTable();
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        public DataTable SqlQuery(string sql, List<SqlParameter> parametros) {
            var comando = new SqlCommand(sql, _ligDb);
            var registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        public DataTable SqlQuery(string sql, List<SqlParameter> parametros, SqlTransaction transacao) {
            var comando = new SqlCommand(sql, _ligDb) { Transaction = transacao };
            var registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            registos.Dispose();
            comando.Dispose();
            return registos;
        }

        public bool Command(string sql) {
            try {
                var comando = new SqlCommand(sql, _ligDb);
                comando.ExecuteNonQuery();
                comando.Dispose();
            } catch (Exception erro) {
                Debug.WriteLine(erro.Message);
                return false;
            }
            return true;
        }

        public bool Command(string sql, List<SqlParameter> parametros) {
            try {
                var comando = new SqlCommand(sql, _ligDb);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.ExecuteNonQuery();
                comando.Dispose();
            } catch (Exception erro) {
                Console.Write(erro.Message);
                //throw erro;
                return false;
            }
            return true;
        }

        public bool Command(string sql, List<SqlParameter> parametros, SqlTransaction transacao) {
            try {
                var comando = new SqlCommand(sql, _ligDb);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.Transaction = transacao;
                comando.ExecuteNonQuery();
                comando.Dispose();
            } catch (Exception erro) {
                Console.Write(erro.Message);
                return false;
            }
            return true;
        }

        #endregion

        #region Utilizadores 

        public DataTable Login(string username, string password) {
            _sql = "SELECT * FROM users WHERE username=@username AND password=HASHBYTES('SHA2_512',@password) AND active=1";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter {ParameterName="@username",SqlDbType=SqlDbType.VarChar,Value=username },
                new SqlParameter {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password }
            };
            var utilizador = SqlQuery(_sql, _parameters);
            if (utilizador == null || utilizador.Rows.Count == 0)
                return null;
            return utilizador;
        }

        public DataTable AllUsers() => SqlQuery("SELECT idUser, username, email, name, birthday, address, gender, phone, newsletter, lastSign, role FROM users");

        public DataTable AllActiveUsers() => SqlQuery("SELECT * FROM users WHERE active=1");

        public DataTable AllInactiveUsers() => SqlQuery("SELECT * FROM users WHERE active = 0");

        public DataTable UserInfo(int id) => SqlQuery($"SELECT * FROM users WHERE idUser = {id}");

        public bool UsernameExist(string username) => SqlQuery($"SELECT * FROM users WHERE username = {username}").Rows.Count != 0;

        public DataTable SearchUsers(string search) {
            _sql = "SELECT * FROM users WHERE name LIKE @search OR username LIKE @search OR birthday LIKE @search OR address LIKE @search";
            _parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@search", SqlDbType = SqlDbType.NVarChar, Value = "%" + search + "%" } };
            return SqlQuery(_sql, _parameters);
        }

        public int AddUser(string username, string password, string email, string name, string birthday, string address, string gender, string phone, bool newsletter, int role) {
            _sql = "INSERT INTO users(username, password, email, name, birthday, address, gender, phone, newsletter, role, addDate, lastUpdate, active) VALUES (@username, HASHBYTES('SHA2_512', @password), @email, @name, @birthday, @address, @gender, @phone, @newsletter, @role, getDate(),  getDate(), 1)";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@username",   SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter { ParameterName = "@password",   SqlDbType = SqlDbType.NVarChar, Value = password },
                new SqlParameter { ParameterName = "@email",      SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter { ParameterName = "@name",       SqlDbType = SqlDbType.NVarChar, Value = name },
                new SqlParameter { ParameterName = "@birthday",   SqlDbType = SqlDbType.Date,     Value = birthday },
                new SqlParameter { ParameterName = "@address",    SqlDbType = SqlDbType.NVarChar, Value = address },
                new SqlParameter { ParameterName = "@gender",     SqlDbType = SqlDbType.Int,      Value = gender },
                new SqlParameter { ParameterName = "@phone",      SqlDbType = SqlDbType.NVarChar, Value = phone },
                new SqlParameter { ParameterName = "@newsletter", SqlDbType = SqlDbType.Bit,      Value = newsletter },
                new SqlParameter { ParameterName = "@role",       SqlDbType = SqlDbType.Int,      Value = role }

            };
            var comando = new SqlCommand(_sql, _ligDb);
            comando.Parameters.AddRange(_parameters.ToArray());
            var id = (int)comando.ExecuteScalar();
            comando.Dispose();
            return id;
        }

        public bool EditUser(int id, string username, string password, string email, string name, string birthday, string address, string gender, string phone, string imgUrl, int role) {
            _sql = "UPDATE users SET username=@username, password=HASHBYTES('SHA2_512', @password), email=@email, name=@name, birthday=@birthday, address=@address, gender=@gender, phone=@phone, imgUrl=@imgUrl, role=@role, lastUpdate=getDate() WHERE idUser=@id";

            _parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@username",   SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter { ParameterName = "@password",   SqlDbType = SqlDbType.NVarChar, Value = password },
                new SqlParameter { ParameterName = "@email",      SqlDbType = SqlDbType.NVarChar, Value = email },
                new SqlParameter { ParameterName = "@name",       SqlDbType = SqlDbType.NVarChar, Value = name },
                new SqlParameter { ParameterName = "@birthday",   SqlDbType = SqlDbType.Date,     Value = birthday },
                new SqlParameter { ParameterName = "@address",    SqlDbType = SqlDbType.NVarChar, Value = address },
                new SqlParameter { ParameterName = "@gender",     SqlDbType = SqlDbType.Int,      Value = gender },
                new SqlParameter { ParameterName = "@phone",      SqlDbType = SqlDbType.NVarChar, Value = phone },
                new SqlParameter { ParameterName = "@imgUrl",     SqlDbType = SqlDbType.NVarChar, Value = imgUrl },
                new SqlParameter { ParameterName = "@role",       SqlDbType = SqlDbType.Int,      Value = role },
                new SqlParameter { ParameterName = "@id",         SqlDbType = SqlDbType.Int,      Value = id }

            };
            return Command(_sql, _parameters);
        }

        public bool EnableDisableUser(int id) {
            var user = UserInfo(id);
            var active = int.Parse(user.Rows[0][16].ToString()) == 0 ? 1 : 0;
            return Command($"UPDATE users SET active = {active} WHERE id={active}");
        }
        #endregion

        #region Produtos

        public DataTable AllProducts() => SqlQuery("SELECT idProduct AS 'ID', productName AS 'Nome', shortDescription AS 'Descrição Curta', price AS 'Preço', brands.name AS 'Marca', categories.name AS 'Categoria', stock AS 'Stock', warnings AS 'Alertas', products.active AS 'Ativo' FROM products LEFT JOIN brands ON products.brand = brands.idBrand LEFT JOIN categories ON products.category = categories.idCategory");

        public DataTable AllActiveProducts() => SqlQuery("SELECT idProduct AS 'ID', productName AS 'Nome', shortDescription AS 'Descrição Curta', price AS 'Preço', brands.name AS 'Marca', categories.name AS 'Categoria', stock AS 'Stock', warnings AS 'Alertas', products.active AS 'Ativo' FROM products LEFT JOIN brands ON products.brand = brands.idBrand LEFT JOIN categories ON products.category = categories.idCategory WHERE products.active = 1");

        public DataTable AllInactiveProducts() => SqlQuery("SELECT idProduct AS 'ID', productName AS 'Nome', shortDescription AS 'Descrição Curta', price AS 'Preço', brands.name AS 'Marca', categories.name AS 'Categoria', stock AS 'Stock', warnings AS 'Alertas', products.active AS 'Ativo' FROM products LEFT JOIN brands ON products.brand = brands.idBrand LEFT JOIN categories ON products.category = categories.idCategory WHERE products.active = 0");

        public DataTable AllWhithoutStockProducts() => SqlQuery("SELECT idProduct AS 'ID', productName AS 'Nome', shortDescription AS 'Descrição Curta', price AS 'Preço', brands.name AS 'Marca', categories.name AS 'Categoria', stock AS 'Stock', warnings AS 'Alertas', products.active AS 'Ativo' FROM products LEFT JOIN brands ON products.brand = brands.idBrand LEFT JOIN categories ON products.category = categories.idCategory WHERE products.stock > 0");

        public DataTable AllProducts(string orderBy) => SqlQuery($"SELECT * FROM products ORDER BY {orderBy}");

        public DataTable AllAvailableProducts() => SqlQuery("SELECT * FROM products WHERE stock >= 0");

        public DataTable AllAvailableProducts(string orderBy) => SqlQuery($"SELECT * FROM products WHERE stock >= 0 ORDER BY {orderBy}");

        public DataTable ProductInfo(int id) => SqlQuery($"SELECT * FROM products WHERE idProduct={id}");

        public DataTable SearchProductByName(string search) => SqlQuery($"SELECT * FROM products LEFT JOIN brands ON products.brand = brands.idBrand LEFT JOIN categories ON products.category = categories.idCategory WHERE productName LIKE '%{search}%'");

        public DataTable SearchProductByDescription(string search) => SqlQuery($"SELECT * FROM products LEFT JOIN brands ON products.brand = brands.idBrand LEFT JOIN categories ON products.category = categories.idCategory WHERE longDescription LIKE '%{search}%' or shortDescription LIKE '%{search}%'");

        public DataTable SearchProductByPrice(double min, double max) => SqlQuery($"SELECT * FROM products LEFT JOIN brands ON products.brand = brands.idBrand LEFT JOIN categories ON products.category = categories.idCategory WHERE price BETWEEN {min} AND {max}");

        public DataTable SearchProducts(string search) {
            _sql = "SELECT * FROM products WHERE productName LIKE @search OR shortDescription LIKE @search OR longDescription LIKE @search OR brand LIKE @search";
            _parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@search", SqlDbType = SqlDbType.NVarChar, Value = "%" + search + "%" } };
            return SqlQuery(_sql, _parameters);
        }

        public DataTable LatestProducts() => SqlQuery("SELECT TOP 8 * FROM products ORDER BY addDate");

        public DataTable SimilarProducts(double price) => SqlQuery($"SELECT TOP 8 * FROM products WHERE price <= {price}");

        public bool AddProduct(string productName, string shortDescription, string longDescription, double price, string brand, string category, int stock, string warnings, string imgUrl) {
            _sql = "INSERT INTO products(productName, shortDescription, longDescription, price, brand, category, stock, warnings, imgUrl, addDate, lastUpdate, active) VALUES (@productName, @shortDescription, @longDescription, @price, @brand, @category, @stock, @warnings, @imgUrl, getDate(), getDate(), 1)";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@productName", SqlDbType = SqlDbType.NVarChar, Value = productName },
                new SqlParameter { ParameterName = "@shortDescription", SqlDbType = SqlDbType.NVarChar, Value = shortDescription },
                new SqlParameter { ParameterName = "@longDescription", SqlDbType = SqlDbType.NVarChar, Value = longDescription },
                new SqlParameter { ParameterName = "@price", SqlDbType = SqlDbType.Decimal, Value = decimal.Parse(price.ToString(CultureInfo.InvariantCulture)) },
                new SqlParameter { ParameterName = "@brand", SqlDbType = SqlDbType.Int, Value = brand },
                new SqlParameter { ParameterName = "@category", SqlDbType = SqlDbType.Int, Value = category },
                new SqlParameter { ParameterName = "@stock", SqlDbType = SqlDbType.Int, Value = stock },
                new SqlParameter { ParameterName = "@warnings", SqlDbType = SqlDbType.NVarChar, Value = warnings },
                new SqlParameter { ParameterName = "@imgUrl", SqlDbType = SqlDbType.NVarChar, Value = imgUrl }
            };
            return Command(_sql, _parameters);
        }

        public bool EnableDisableProduct(int id) {
            var product = ProductInfo(id);
            var active = int.Parse(product.Rows[0][11].ToString()) == 0 ? 1 : 0;
            return Command($"UPDATE products SET active = {active} WHERE id={active}");
        }

        #endregion

        #region Comentários

        public DataTable AllProductComments(int id) => SqlQuery($"SELECT users.name, title, comment FROM comments INNER JOIN users ON comments.idUser = users.idUser  WHERE comments.idProduct = {id} AND comments.active = 1");
        public DataTable AllCommentComments(int id) => SqlQuery($"SELECT * FROM comments WHERE idComments = {id}  AND rating <> -1 AND  active = 1");
        public int AvgOfRatings(int id) => int.Parse(SqlQuery($"SELECT AVG(rating) FROM comments WHERE idProduct = {id} AND rating <> -1").Rows[0][0].ToString());

        public bool AddComment(int idUser, int idProduct, string title, string comment) {

            _sql = "INSERT INTO comments(idUser, idProduct, title, comment, addDate, lastUpdate, active) VALUES (@idUser, @idProduct, @title, @comment, getDate(), getDate(), 1)";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@idUser",        SqlDbType = SqlDbType.NVarChar, Value = idUser },
                new SqlParameter { ParameterName = "@idProduct", SqlDbType = SqlDbType.NVarChar, Value = idProduct },
                new SqlParameter { ParameterName = "@title",        SqlDbType = SqlDbType.NVarChar, Value = title },
                new SqlParameter { ParameterName = "@comment", SqlDbType = SqlDbType.NVarChar, Value = comment }
            };
            return Command(_sql, _parameters);
        }

        #endregion

        #region Marcas 

        public DataTable AllBrands() => SqlQuery("SELECT idBrand AS 'ID', name AS 'Nome', description AS 'Descrição', active AS 'Ativo' FROM brands");
        public DataTable AllActiveBrands() => SqlQuery("SELECT idBrand AS 'ID', name AS 'Nome', description AS 'Descrição', active AS 'Ativo' FROM brands WHERE active = 1");
        public DataTable AllInactiveBrands() => SqlQuery("SELECT idBrand AS 'ID', name AS 'Nome', description AS 'Descrição', active AS 'Ativo' FROM brands WHERE active = 0");

        public bool AddBrand(string name, string description) {
            _sql = "INSERT INTO brands(name, description, addDate, lastUpdate, active) VALUES (@brandName, @brandDescription, getDate(), getDate(), 1)";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@brandName",        SqlDbType = SqlDbType.NVarChar, Value = name },
                new SqlParameter { ParameterName = "@brandDescription", SqlDbType = SqlDbType.NVarChar, Value = description }
            };
            return Command(_sql, _parameters);
        }

        public void UpdateBrand(int id, string name, string description) {
            _sql = "UPDATE brands SET name=@name, description=@description WHERE id=@id";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@name",        SqlDbType = SqlDbType.NVarChar, Value = name },
                new SqlParameter { ParameterName = "@description", SqlDbType = SqlDbType.NVarChar, Value = description },
                new SqlParameter { ParameterName = "@id",          SqlDbType = SqlDbType.Int,      Value = id }
            };
        }

        public string BrandNameById(int id) {
            _sql = "SELECT name FROM brands WHERE idBrand=@id";
            _parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id } };
            var data = SqlQuery(_sql, _parameters);
            return data.Rows[0][0].ToString();
        }

        #endregion

        #region Categorias

        public DataTable AllCategories() => SqlQuery("SELECT idCategory AS 'ID', name AS 'Nome', active as 'Ativo' FROM categories");
        public DataTable AllActiveCategories() => SqlQuery("SELECT idCategory AS 'ID', name AS 'Nome', active as 'Ativo' FROM categories WHERE active = 1");

        public DataTable AllProductsByCategory(int category) {
            _sql = "SELECT * FROM products WHERE category=@cat";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter {ParameterName="@cat",SqlDbType=SqlDbType.NVarChar,Value=category.ToString() }
            };
            return SqlQuery(_sql, _parameters);
        }

        public int CategoryIdByName(string category) {
            try {
                _sql = "SELECT idCategory FROM categories WHERE name=@cat";
                var parametros = new List<SqlParameter> { new SqlParameter { ParameterName = "@cat", SqlDbType = SqlDbType.NVarChar, Value = category } };
                var data = SqlQuery(_sql, parametros);
                return int.Parse(data.Rows[0][0].ToString());
            } catch {
                return 0;
            }
        }

        public string CategoryNameById(int id) {
            _sql = "SELECT name FROM categories WHERE idCategory=@id";
            _parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id } };
            var data = SqlQuery(_sql, _parameters);
            return data.Rows[0][0].ToString();
        }


        public bool AddCategory(string name, string description) {
            _sql = "INSERT INTO categories(name, description, addDate, lastUpdate, active) VALUES (@productName, @productDescription, getDate(), getDate(), 1)";
            _parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@productName",        SqlDbType = SqlDbType.NVarChar, Value = name },
                new SqlParameter { ParameterName = "@productDescription", SqlDbType = SqlDbType.NVarChar, Value = description }
            };
            return Command(_sql, _parameters);
        }

        #endregion

        public bool GetState(string table, int id) {
            try {
                _sql = $"SELECT active FROM {table} WHERE ";
                if (table == "users")
                    _sql += "idUser = ";
                else if (table == "products")
                    _sql += "idProduct = ";
                else if (table == "brands")
                    _sql += "idBrand = ";
                else if (table == "categories")
                    _sql += "idCategory = ";
                else
                    return false;
                _sql += "@id";
                _parameters = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id }
                };
                return (bool)SqlQuery(_sql, _parameters).Rows[0][0];
            } catch (Exception err) {
                var c = err.Message;
                return false;
            }
        }

        public bool Delete(string table, int id) {
            try {
                _sql = $"UPDATE {table} SET active = 0 WHERE ";
                if (table == "users")
                    _sql += "idUser = ";
                else if (table == "products")
                    _sql += "idProduct = ";
                else if (table == "brands")
                    _sql += "idBrand = ";
                else if (table == "categories")
                    _sql += "idCategory = ";
                else
                    return false;
                _sql += "@id";
                _parameters = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id }
                };
                return Command(_sql, _parameters);
            } catch (Exception err) {
                var c = err.Message;
                return false;
            }
        }

        public bool Recover(string table, int id) {
            try {
                _sql = $"UPDATE {table} SET active = 1 WHERE ";
                if (table == "users")
                    _sql += "idUser = ";
                else if (table == "products")
                    _sql += "idProduct = ";
                else if (table == "brands")
                    _sql += "idBrand = ";
                else if (table == "categories")
                    _sql += "idCategory = ";
                else
                    return false;
                _sql += "@id";
                _parameters = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id }
                };
                return Command(_sql, _parameters);
            } catch (Exception err) {
                var c = err.Message;
                return false;
            }
        }

        public DataTable AllNewsletter() => SqlQuery("SELECT email FROM users WHERE newsletter = 1");
    }
}