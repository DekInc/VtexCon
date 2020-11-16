using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess;

namespace Vital.Oracle.Services
{
    public class OracleHelper
    {
        /// <summary>
        /// Inserta un nuevo Producto en la tabla Producto
        /// </summary>
        /// <param name="producto">Entidad contenedora de los valores a insertar</param>
        /// <autor>FerNet</autor>
        //////public void Insert(C_PRODUCTS producto)
        //////{
        //////    //using (OleDbConnection cnx = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fer\OneDrive\Proyectos\CARGAS.accdb"))
        //////    using (OleDbConnection cnx = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\B021319\Documents\CARGAS.accdb"))
        //////    {
        //////        cnx.Open();
        //////        //Declaramos nuestra consulta de Acción Sql parametrizada
        //////        const string sqlQuery =
        //////            //"INSERT INTO Producto (Descripcion, Marca, Precio) VALUES (@descripcion, @marca, @precio)";
        //////            "INSERT INTO C_PRODUCTS (Description_1, Description_2, Code, Commerce, Category, Subcategory, Price, DischargeDate) " +
        //////            "VALUES (@Description_1, @Description_2, @Code, @Commerce, @Category, @Subcategory, @Price, @DischargeDate)";
        //////        using (OleDbCommand cmd = new OleDbCommand(sqlQuery, cnx))
        //////        {
        //////            cmd.Parameters.AddWithValue("@Description_1", producto.Description_1);
        //////            cmd.Parameters.AddWithValue("@Description_2", producto.Description_2);
        //////            cmd.Parameters.AddWithValue("@Code", producto.Code);
        //////            cmd.Parameters.AddWithValue("@Commerce", producto.Commerce);
        //////            cmd.Parameters.AddWithValue("@Category", producto.Category);
        //////            cmd.Parameters.AddWithValue("@Subcategory", producto.Subcategory);
        //////            cmd.Parameters.AddWithValue("@Price", producto.Price);
        //////            cmd.Parameters.AddWithValue("@DischargeDate", DateTime.Today.ToString());

        //////            cmd.ExecuteNonQuery();
        //////        }
        //////    }
        //////}

        /// <summary>
        /// Devuelve una lista de Productos ordenados por el campo Id de manera Ascendente
        /// </summary>
        /// <returns>Lista de productos</returns>
        /// <autor>José Luis García Bautista</autor>
        //////public List<C_PRODUCTS> GetAll()
        //////{
        //////    //Declaramos una lista del objeto EProducto la cual será la encargada de
        //////    //regresar una colección de los elementos que se obtengan de la BD
        //////    //
        //////    //La lista substituye a DataTable utilizado en el proyecto de ejemplo
        //////    List<C_PRODUCTS> productos = new List<C_PRODUCTS>();

        //////    using (OleDbConnection cnx = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fer\OneDrive\Proyectos\CARGAS.accdb"))
        //////    //using (OleDbConnection cnx = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\B021319\Documents\CARGAS.accdb"))
        //////    {
        //////        cnx.Open();

        //////        //const string sqlQuery = "SELECT * FROM Producto ORDER BY Id ASC";
        //////        const string sqlQuery = "SELECT ID, Description_1, Description_2, Code, Commerce, Category, Subcategory, Price, DischargeDate  FROM C_PRODUCTS ORDER BY 1 ASC";
        //////        using (OleDbCommand cmd = new OleDbCommand(sqlQuery, cnx))
        //////        {
        //////            OleDbDataReader dataReader = cmd.ExecuteReader();
        //////            //
        //////            //Preguntamos si el DataReader fue devuelto con datos
        //////            while (dataReader.Read())
        //////            {
        //////                //if (dataReader["Category"].Equals(null)) { Convert.ToDateTime("12/05/2016"); } else { DateTime.Parse(Convert.ToString(dataReader["DischargeDate"])); }
        //////                //if (dataReader["Subcategory"].Equals(null)) { Convert.ToDateTime("12/05/2016"); }
        //////                //if (dataReader["Price"].Equals(null)) { Convert.ToDecimal("0"); }

        //////                //OleDbReaderHelper.GetValue<Int16>(dataReader,"Commerce");
        //////                //yourSqlReaderObject.GetValue<int?>("SOME_ID_COLUMN");
        //////                //yourSqlReaderObject.GetValue<string>("SOME_VALUE_COLUMN");

        //////                // http://stackoverflow.com/questions/18550769/sqldatareader-best-way-to-check-for-null-values-sqldatareader-isdbnull-vs-dbnul

        //////                //
        //////                //Instanciamos al objeto Eproducto para llenar sus propiedades
        //////                C_PRODUCTS producto = new C_PRODUCTS
        //////                {
        //////                    ID = Convert.ToInt32(dataReader["ID"]),
        //////                    Description_1 = Convert.ToString(dataReader["Description_1"]),
        //////                    Description_2 = Convert.ToString(dataReader["Description_2"]),
        //////                    Code = OleDbReaderHelper.GetValue<string>(dataReader, "Code"),
        //////                    Commerce = OleDbReaderHelper.GetValue<Int16>(dataReader, "Commerce"),
        //////                    Category = OleDbReaderHelper.GetValue<Int16>(dataReader, "Category"),
        //////                    Subcategory = OleDbReaderHelper.GetValue<Int16>(dataReader, "Subcategory"),
        //////                    Price = OleDbReaderHelper.GetValue<Decimal>(dataReader, "Price"),
        //////                    DischargeDate = OleDbReaderHelper.GetValue<DateTime>(dataReader, "DischargeDate")
        //////                };
        //////                //
        //////                //Insertamos el objeto Producto dentro de la lista Productos
        //////                productos.Add(producto);
        //////            }
        //////        }
        //////    }
        //////    return productos;
        //////}
        /// <summary>
        /// Devuelve un Objeto Producto
        /// </summary>
        /// <param name="idProducto">Id del producto a buscar</param>
        /// <returns>Un registro con los valores del Producto</returns>
        /// <autor>José Luis García Bautista</autor>
        public string GetByid(Int64 idProducto)
        {
            using (Oracl cnx = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fer\OneDrive\Proyectos\CARGAS.accdb"))
            {
                cnx.Open();

                const string sqlGetById = "SELECT * FROM C_PRODUCTS WHERE ID = @ID";
                using (OleDbCommand cmd = new OleDbCommand(sqlGetById, cnx))
                {
                    //
                    //Utilizamos el valor del parámetro idProducto para enviarlo al parámetro declarado en la consulta
                    //de selección SQL
                    cmd.Parameters.AddWithValue("@ID", idProducto);
                    OleDbDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        C_PRODUCTS producto = new C_PRODUCTS
                        {
                            ID = Convert.ToInt32(dataReader["ID"]),
                            Description_1 = Convert.ToString(dataReader["Description_1"]),
                            Description_2 = Convert.ToString(dataReader["Description_2"]),
                            Code = Convert.ToString(dataReader["Code"])
                        };

                        return producto;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Actualiza el Producto correspondiente al Id proporcionado
        /// </summary>
        /// <param name="producto">Valores utilizados para hacer el Update al registro</param>
        /// <autor>José Luis García Bautista</autor>
        //////public void Update(C_PRODUCTS producto)
        //////{
        //////    using (OleDbConnection cnx = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fer\OneDrive\Proyectos\CARGAS.accdb"))
        //////    {
        //////        cnx.Open();
        //////        const string sqlQuery =
        //////            //"UPDATE Producto SET Descripcion = @descripcion, Marca = @marca, Precio = @precio WHERE Id = @id";
        //////            "UPDATE C_PRODUCTS SET [Description_1] = @Description_1, " +
        //////            "[Description_2] = @Description_2, [Code] = @Code," +
        //////            "[Commerce] = @Commerce, [Category] = @Category, " +
        //////            "[Subcategory] = @Subcategory, [Price] = @Price," +
        //////            "[DischargeDate] = @DischargeDate WHERE [Id] = @ID";

        //////        using (OleDbCommand cmd = new OleDbCommand(sqlQuery, cnx))
        //////        {
        //////            //cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
        //////            //cmd.Parameters.AddWithValue("@marca", producto.Marca);
        //////            //cmd.Parameters.AddWithValue("@precio", producto.Precio);
        //////            //cmd.Parameters.AddWithValue("@id", producto.Id);
        //////            cmd.Parameters.AddWithValue("@Description_1", producto.Description_1);
        //////            cmd.Parameters.AddWithValue("@Description_2", producto.Description_2);
        //////            cmd.Parameters.AddWithValue("@Code", producto.Code);
        //////            cmd.Parameters.AddWithValue("@Commerce", producto.Commerce);
        //////            cmd.Parameters.AddWithValue("@Category", producto.Category);
        //////            cmd.Parameters.AddWithValue("@Subcategory", producto.Subcategory);
        //////            cmd.Parameters.AddWithValue("@Price", producto.Price);
        //////            cmd.Parameters.AddWithValue("@DischargeDate", DateTime.Today.ToString());
        //////            cmd.Parameters.AddWithValue("@ID", producto.ID);

        //////            cmd.ExecuteNonQuery();
        //////        }
        //////    }
        //////}
        /// <summary>
        /// Elimina un registro coincidente con el Id Proporcionado
        /// </summary>
        /// <param name="idproducto">Id del registro a Eliminar</param>
        /// <autor>José Luis García Bautista</autor>
        //////public void Delete(int idproducto)
        //////{
        //////    using (OleDbConnection cnx = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fer\OneDrive\Proyectos\CARGAS.accdb"))
        //////    {
        //////        cnx.Open();
        //////        const string sqlQuery = "DELETE FROM C_PRODUCTS WHERE [Id] = @ID";
        //////        using (OleDbCommand cmd = new OleDbCommand(sqlQuery, cnx))
        //////        {
        //////            cmd.Parameters.AddWithValue("@ID", idproducto);

        //////            cmd.ExecuteNonQuery();
        //////        }
        //////    }
        //////}
    }
}
