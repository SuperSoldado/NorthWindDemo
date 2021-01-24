using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

/* Usefull links: 
http://stackoverflow.com/questions/8331807/adding-additional-attributes-to-each-property-of-a-class
http://www.codeproject.com/Articles/15946/Custom-Attributes-in-NET
http://msdn.microsoft.com/en-us/library/z919e8tw.aspx

CustomPropertyAttribute signature:
1)CustomPropertyAttribute("string")
2)CustomPropertyAttribute("string","string")
3)CustomPropertyAttribute("string","string","string")
Example:		
1)("dataBaseType") : When only one parameter is supplied, it is considered the database type (int, varchar, etc.)
  Example: [CustomPropertyAttribute("nvarchar")]
2)("dataBaseType","properties") : Introuduce the properties data separeted with ",".
  Example: [CustomPropertyAttribute("int","isIdentity,isPrimaryKey")]
3)("dataBaseType","properties","fkInfo") : Introuduce the foreing key info.
  Example: [CustomPropertyAttribute("int","ForeingKeyTable=Tipo;ForeingKeyColumn=ID;ForeingKeyDescriptionColumnName=Descricao;ForeingKeyDataBaseDescriptionType=nvarchar","isNullable")]
  ForeingKeyTable=Tipo;                       Indicates the table where the column in database references to.
  ForeingKeyColumn=ID;                        Indicates the column where the column in database references to.
  ForeingKeyDescriptionColumnName=Descricao;  Indicates the column used to display data to user (instead of show ID, the reflection choose the description).
  ForeingKeyprefix=FK0_;                      Indicates the column prefix used internally to generate queries without same column name.
  ForeingKeyDataBaseDescriptionType=nvarchar  Indicates the datata type in database of the "ForeingKeyDescriptionColumnName" column.
 */
namespace LibShared.Reflection//LibShared.Reflection//Bludgeon.Library.Reflection
{


    /// <summary>
    /// This class encapsulates custom information for properties, like "isPrimaryKey" or "IsNullable"
    /// </summary>
    // Multiuse attribute.
    [System.AttributeUsage(System.AttributeTargets.All | System.AttributeTargets.Struct, AllowMultiple = true)]
    public class CustomPropertyAttribute : System.Attribute
    {
        /* Put this shit in interface, use with ClassProperty */
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeingKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPersisted { get; set; }
        public string FKTable { get; set; }
        public string FKColumn { get; set; }
        public string FKColumnDescription { get; set; }
        public string FKColumnDescriptionType { get; set; }
        public string FKColumnPrefix { get; set; }
        public string DatabaseType { get; set; }

        /// <summary>
        /// Custom attributs used on "info" classes to define some properties mapped from database.
        /// </summary>
        /// <param name="databaseType">Type as is in database, such as "varchar(100)" or "float"</param>
        /// <param name="properties">Adicional info. Accepted values: [isNullable,isPrimaryKey]</param>
        public CustomPropertyAttribute(string databaseType)
        {
            this.DatabaseType = databaseType;
            this.IsNullable = false;
            this.IsPrimaryKey = false;
            this.IsForeingKey = false;
            this.IsPersisted = false;
            this.IsIdentity = false;

            if (IsNullable && IsPrimaryKey)
            {
                throw new Exception("CustomPropertyAttribute (Constructor): Cannot be Primary key and isNullable at same time.");
            }
        }

        public CustomPropertyAttribute(string databaseType, string properties)
        {
            this.DatabaseType = databaseType;
            this.IsNullable = false;
            this.IsPrimaryKey = false;
            this.IsForeingKey = false;
            this.IsPersisted = false;

            string[] allProperties = properties.Split(',');

            for (int i = 0; i < allProperties.Length; i++)
            {
                switch (allProperties[i])
                {
                    case "isNullable":
                        this.IsNullable = true;
                        break;
                    case "isPrimaryKey":
                        this.IsPrimaryKey = true;
                        break;
                    case "isIdentity":
                        this.IsIdentity = true;
                        break;
                    case "isPersisted":
                        this.IsPersisted = true;
                        break;
                    default: throw new Exception("CustomPropertyAttribute (Constructor) can not process the property " + properties[i]);
                }
            }
        }


        /// <summary>
        /// Custom attributos used on "info" classes to define some properties mapped from database.
        /// </summary>
        /// <param name="databaseType">Type as is in database, such as "varchar(100)" or "float"</param>
        /// <param name="foreingKeyInfo">FK related info. Ex.: "ForeingKeyTable=MyTable;ForeingKeyColumn=MyColumnID;ForeingKeyDescriptionColumnName=colName;ForeingKeyDataBaseDescriptionType=VarChar(50)"</param>
        /// <param name="foreingKeyColumnName">The column name mapped as foreing key.</param>
        /// <param name="foreingKeyColumnDescription">The foreing key column description.</param>
        /// <param name="properties">Adicional info. Accepted values: [isNullable,isPrimaryKey]</param>
        public CustomPropertyAttribute(string databaseType, string properties, string foreingKeyInfo)
        {
            /**********************************************************************/
            //resolver problema de assinatura:
            //[CustomPropertyAttribute("int","isIdentity","isPrimaryKey")]
            //Está caindo aqui.
            /**********************************************************************/

            /*
             * ForeingKeyTable=Tipo;
             * ForeingKeyColumn=ID;
             * ForeingKeyDescriptionColumnName=Descricao;
             * FKColumnPrefix=FK0_;
             * ForeingKeyDataBaseDescriptionType=nvarchar*/

            string[] fkData = foreingKeyInfo.Split(';');
            string[] fkvalue0 = fkData[0].Split('=');
            string[] fkvalue1 = fkData[1].Split('=');
            string[] fkvalue2 = fkData[2].Split('=');
            string[] fkvalue3 = fkData[3].Split('=');
            string[] fkvalue4 = fkData[4].Split('=');
            this.FKTable = fkvalue0[1];
            this.FKColumn = fkvalue1[1];
            this.FKColumnDescription = fkvalue2[1];
            this.FKColumnPrefix = fkvalue3[1];
            this.FKColumnDescriptionType = fkvalue4[1];

            this.DatabaseType = databaseType;
            this.IsNullable = false;
            this.IsPrimaryKey = false;
            this.IsForeingKey = true;
            this.IsPersisted = false;

            string[] allProperties = properties.Split(',');

            for (int i = 0; i < allProperties.Length; i++)
            {
                switch (allProperties[i])
                {
                    case "isNullable":
                        this.IsNullable = true;
                        break;
                    case "isPrimaryKey":
                        this.IsPrimaryKey = true;
                        break;
                    case "isIdentity":
                        this.IsIdentity = true;
                        break;
                    case "isPersisted":
                        this.IsPersisted = true;
                        break;
                    default: throw new Exception("CustomPropertyAttribute (Constructor) can not process the property " + properties[i]);
                }
            }
        }
    }
}
